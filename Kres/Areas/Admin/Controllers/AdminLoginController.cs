﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kres.Areas.Admin.Controllers
{
    public class AdminLoginController : Controller
    {
        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }


        // Error Page
        public ActionResult Unauthorized()
        {

            return View();
        }
    }
}