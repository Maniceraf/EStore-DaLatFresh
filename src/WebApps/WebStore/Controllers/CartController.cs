using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using WebStore.Helpers;
using WebStore.Interfaces;
using WebStore.Interfaces.Services;
using WebStore.ValueObjects;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFirebaseStorageService _firebaseStorageService;
        public List<CartItemViewModel> Cart => HttpContext.Session.Get<List<CartItemViewModel>>(Globals.CART_KEY) ?? new List<CartItemViewModel>();

        public CartController(IUnitOfWork unitOfWork, IFirebaseStorageService firebaseStorageService) 
        {
            _unitOfWork = unitOfWork;
            _firebaseStorageService = firebaseStorageService;
        }

		[HttpGet]
		public IActionResult Index()
        {
			var result = Cart.Select(x => 
			{
				var a = _firebaseStorageService.GetSignedUrlAsync(x.PreviewImage ?? string.Empty).Result;
                return new CartItemViewModel
                {
                    ProductId = x.ProductId,
                    ProductName = x.ProductName,
                    PreviewImage = a,
                    Count = x.Count,
                    Price = x.Price,
				};
			}).ToList();
            return View(result);
        }

        [HttpPost]
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
