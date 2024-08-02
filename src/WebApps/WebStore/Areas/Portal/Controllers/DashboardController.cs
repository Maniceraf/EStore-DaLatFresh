using Microsoft.AspNetCore.Mvc;

namespace WebStore.Areas.Portal.Controllers
{
    [Route("portal-dashboard")]
    public class DashboardController : BaseController
    {
        public DashboardController()
        {
            ViewData["menu"] = "Dashboard";
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
