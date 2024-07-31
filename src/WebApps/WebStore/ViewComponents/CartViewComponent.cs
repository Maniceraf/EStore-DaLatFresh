using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebStore.Helpers;
using WebStore.Models;
using WebStore.ValueObjects;
using WebStore.ViewModels;

namespace WebStore.ViewComponents
{
    public class CartViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
			var cart = HttpContext.Session.Get<List<CartItemViewModel>>(Globals.CART_KEY) ?? new List<CartItemViewModel>();

			return View("CartPanel", new CartViewModel
            {
                Quantity = cart.Sum(p => p.Count),
                Total = cart.Sum(p => p.Total)
            });
        }
    }
}
