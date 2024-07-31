using Microsoft.AspNetCore.Mvc;
using WebStore.Helpers;
using WebStore.Interfaces;
using WebStore.ValueObjects;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public List<CartItemViewModel> Cart => HttpContext.Session.Get<List<CartItemViewModel>>(Globals.CART_KEY) ?? new List<CartItemViewModel>();

        public CartController(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(Cart);
        }

        [HttpGet]
        public IActionResult AddToCart(int id, int quantity = 1)
        {
            var cart = Cart;
            var item = cart.SingleOrDefault(p => p.ProductId == id);
            if (item == null)
            {
                var product = _unitOfWork.ProductRepository.GetById(id);
                if (product == null)
                {
                    TempData["Message"] = $"Không tìm thấy hàng hóa có mã {id}";
                    return Redirect("/404");
                }

                item = new CartItemViewModel
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    Price = product.Price ?? 0,
                    PreviewImage = product.PreviewImage ?? string.Empty,
                    Count = quantity
                };

                cart.Add(item);
            }
            else
            {
                item.Count += quantity;
            }

            HttpContext.Session.Set(Globals.CART_KEY, cart);

            return RedirectToAction("Index");
        }

        [HttpDelete]
        public IActionResult RemoveCart(int id)
        {
            var cart = Cart;
            var item = cart.SingleOrDefault(p => p.ProductId == id);
            if (item != null)
            {
                cart.Remove(item);
                HttpContext.Session.Set(Globals.CART_KEY, cart);
            }
            return RedirectToAction("Index");
        }
    }
}
