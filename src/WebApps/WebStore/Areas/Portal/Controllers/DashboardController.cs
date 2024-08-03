using Microsoft.AspNetCore.Mvc;

namespace WebStore.Areas.Portal.Controllers
{
    [Route("portal/dashboard")]
    public class DashboardController : BaseController
    {
        public DashboardController()
        {
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
