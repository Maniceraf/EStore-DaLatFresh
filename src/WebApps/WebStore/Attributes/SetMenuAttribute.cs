using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace WebStore.Attributes
{
    public class SetMenuAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var controller = filterContext.Controller as Controller;
            if (controller != null)
            {
                controller.ViewData["menu"] = controller.GetType().Name.Replace("Controller", "");
            }
            base.OnActionExecuting(filterContext);
        }
    }
}
