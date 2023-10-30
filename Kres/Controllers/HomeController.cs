using Kres.Models.EntityLayer;
using Kres.Models.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kres.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public string SetCurrentLanguage(int id)
        {
            LanguageList.First(x => x.IsSelected).IsSelected = false;
            LanguageList.First(x => x.Id == id).IsSelected = true;
            CurrentLanguage = LanguageList.First(x => x.Id == id);
            GetLocaleStringResource();
            FooterInformationItem = null;
            return "true";
        }
        [HttpPost]
        public string GetLocaleString(string resourceName)
        {
            return resourceName.toLanguage();
        }
        
        [HttpPost]
        public JsonResult GetLanguageList()
        {
            var language = Language.GetList();
            LanguageList = language;
            return Json(language);
        }
        [HttpPost]
        public ActionResult GetCurrentLanguageInfo()
        {
            return Json(new { CurrentLanguage });
        }
        [AllowAnonymous]
        public ActionResult MyAction()
        {

            var banks = Banks.GetListByEpaymentById();

            return View();
        }

    }
}