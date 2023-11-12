using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kres.Models.Helper
{
    public class CustomAuthorizeAttribute : ActionFilterAttribute
    {
        public string Name { get; set; }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            string name = Name;
            if (string.IsNullOrEmpty(name))
                name = filterContext.Controller.GetType().Name;

            string allRoles = HttpContext.Current.Session["SystemRoles"].ToString();
            if (name == string.Empty || allRoles.ToString().Contains(name))
            {
                filterContext.Controller.ViewData["ControllerDisplayName"] = Name;
                base.OnActionExecuting(filterContext);
            }
            else
            {
                filterContext.Result = new RedirectResult("~/Login/Unauthorized");
            }
        }
    }
}