using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kres.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                // return View(model: "Admin area home controller");
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                   defaults: new
                   {
                       controller = "Homes",
                       action = "Index",
                       id = UrlParameter.Optional
                   }
            //  new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}