using Microsoft.AspNetCore.Mvc;
using WebStore.Areas.Portal.ValueObjects;

namespace WebStore.Areas.Portal.ViewComponents
{
    [ViewComponent(Name = "PortalMenu"), ]
    public class MenuViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(string menuItem)
        {
            var result = Routes.GetAll(menuItem);
            return View("~/Areas/Portal/Views/Shared/Components/PortalMenu/Default.cshtml", result);
        }
    }
}
