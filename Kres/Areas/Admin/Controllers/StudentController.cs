using Kres.Models.EntityLayer;
using Kres.Models.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Kres.Areas.Admin.Controllers.AdminPaymentController;

namespace Kres.Areas.Admin.Controllers
{
    public class StudentController : BaseController
    {
        // GET: Admin/Student
        public ActionResult Index()
        {
            return View();
        }



        #region Methods
        [HttpPost]
        public string GetListStudent(StudentSearchCriteria studentSearchCriteria)
        {
            //studentSearchCriteria.EndDate = studentSearchCriteria.EndDate.Date.Add(new TimeSpan(23, 59, 59));
            // return JsonConvert.SerializeObject(Student.GetListStudent(studentSearchCriteria.StartDate, studentSearchCriteria.EndDate, studentSearchCriteria.T9Text, studentSearchCriteria.StudentStatu));
            studentSearchCriteria.T9Text = studentSearchCriteria.T9Text == null ? "*" : studentSearchCriteria.T9Text;
            return JsonConvert.SerializeObject(Student.GetListStudent(studentSearchCriteria.T9Text));
        }

       
        public class StudentSearchCriteria
        {
            public string T9Text { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public int StudentStatu { get; set; }
        }
        #endregion
    }
}