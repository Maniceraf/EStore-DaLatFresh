using Microsoft.AspNetCore.Mvc;
using WebStore.Attributes.ActionFilterAttributes;

namespace WebStore.Areas.Portal.Controllers
{
    [Area("Portal")]
    [SetMenu]
    public class BaseController : Controller
    {

    }
}
