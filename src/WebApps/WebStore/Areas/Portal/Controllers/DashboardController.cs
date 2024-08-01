using Microsoft.AspNetCore.Mvc;

namespace WebStore.Areas.Portal.Controllers
{
    [Route("portal-dashboard")]
    public class DashboardController : BaseController
    {
        public IActionResult Index()
        {
            ViewData["menu"] = "Dashboard";
            return View();
        }
    }
}
