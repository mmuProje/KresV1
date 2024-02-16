using Kres.Models.EntityLayer;
using Kres.Models.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
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

        public ActionResult StudentAdd()
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

        [HttpPost]
        public JsonResult AddStudent(string Name = "", string GuardianName = "", string Tel1 = "", string Mail = "", string Age = "", string Gender = "", string Address = "", string City = "", string Town = "")
        {
            MessageBox message = new MessageBox(MessageBoxType.Error, ("CommonController.Unsuccess").toLanguage());
            string fileName = "";

            Student student = new Student()
            {
                Name = Name,
                GuardianName = GuardianName,
                Tel1 = Tel1,
                Mail = Mail,
                Age = Convert.ToDateTime(Age),
                Gender = Gender,
                Address = Address,
                City = City,
                Town = Town
            };
            
            if ((Request == null || Request.Files.Count == 0))
            {
                var result = student.Add();
                message = result ? new MessageBox(MessageBoxType.Warning, "Kayıt Başarıyla Oluşturuldu.") : new MessageBox(MessageBoxType.Error, "Hata");
                return Json(message);
            }

            if (Request.Files.Count == 1)
            {
                HttpPostedFileBase file = Request.Files[0];
                if ((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName))
                {
                    var arr = Path.GetFileName(file.FileName).Split('.');

                    var fileExtension = arr[arr.Length - 1];
                    var fileNameWithoutExtension = string.Join(".", arr.Take(arr.Length - 1));
                    fileName = fileNameWithoutExtension + "_" + Guid.NewGuid() + "." + fileExtension;

                    string path = Path.Combine(Server.MapPath("~/Files/StudentImages/"), fileName);
                    file.SaveAs(path);

                    string picturePath = "/Files/manufacturerImages/" + fileName;

                    student.PicturePath = picturePath;
                    var result = student.Add();

                    if (result)
                        message = new MessageBox(MessageBoxType.Success, "İşlem Başarılı");
                    else
                        message = new MessageBox(MessageBoxType.Error, "Hata");

                }
            }
            return Json(message);
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