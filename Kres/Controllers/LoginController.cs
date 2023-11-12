using Kres.Models.EntityLayer;
using Kres.Models.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Kres.Controllers
{
    public class LoginController : Controller
    {
        private string ip
        {
            get
            {
                string ipValue = HttpContext.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (string.IsNullOrEmpty(ipValue))
                    ipValue = HttpContext.Request.ServerVariables["REMOTE_ADDR"];
                return ipValue;
            }
        }

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public string sLogin(string Code, string Password, string loginType)
        {
            MessageBox message = null;
            LoginType CurrentLoginType = new LoginType();

            CurrentLoginType = String.IsNullOrEmpty(loginType)
                ? LoginType.Teacher
                : LoginType.Student;

            if (CurrentLoginType == LoginType.Teacher)
            {
                Logon logon = new Logon();
                logon.UserCode = Code;
                logon.Password = Password;

                Teacher checkTeacher = logon.TeacherAuthenticationCheck();

                if (checkTeacher != null && checkTeacher.Id > 0)
                {
                    Teacher teacher = logon.TeacherLogin();
                    if (teacher != null && teacher.Id != 0)
                    {
                        //if (!(Session["B2BRuleItem"] as B2bRule).CustomerWebLogin || !teacher.._WebLogin)
                        //{
                        //    message = new MessageBox(MessageBoxType.Error, "Web Giriş Yetkilerinizi Kontrol Ediniz");
                        //}
                        //else
                        //{
                        //Logger.LogTransaction(ClientType.B2BWeb, LogTransactionSource.Login, ProcessLogin.Success.ToString(), string.Empty, ip, -1, customer.Id, customer.Users.Id);

                        message = new MessageBox(MessageBoxType.Success, "Giriş Başarılı");
                        Teacher CurrentTeacher = Teacher.GetById(teacher.Id);
                        HttpContext.Session["Teacher"] = CurrentTeacher;
                        HttpContext.Session["LoginType"] = CurrentLoginType;
                        FormsAuthentication.SetAuthCookie(CurrentTeacher.Name, false);

                        return JsonConvert.SerializeObject(message);
                       // }
                    }
                    else
                    {
                        //string failMessage = "Kullanıcı Kodu :" + logon.UserCode + "    Şifre:" + logon.Password;
                        //Logger.LogTransaction(ClientType.Admin, LogTransactionSource.Login, ProcessLogin.Fail.ToString(), failMessage, ip, -1, -1, -1, checkTeacher == null ? -1 : checkTeacher.Id, -1);

                        message = new MessageBox(MessageBoxType.Error, "Kullanıcı Adı Veya Şifre Hatalı!");
                    }
                }
                else
                {
                    //string failMessage = "Kullanıcı Kodu :" + logon.UserCode + "    Şifre:" + logon.Password;
                    //Logger.LogTransaction(ClientType.Admin, LogTransactionSource.Login, ProcessLogin.Fail.ToString(), failMessage, ip, -1, -1, -1, checkTeacher == null ? -1 : checkTeacher.Id, -1);

                    message = new MessageBox(MessageBoxType.Error, "Girdiğiniz bilgilere ait bir hesap bulunamadı!");
                }
            }
            else
            {
                Logon logon = new Logon();
                logon.UserCode = Code;
                logon.Password = Password;

                Student checkStudent = logon.StudentAuthenticationCheck();

                if (checkStudent != null && checkStudent.Id > 0)
                {
                    Student student = logon.StudentLogin();
                    if (student != null && student.Id != 0)
                    {
                        //if (!(Session["B2BRuleItem"] as B2bRule).CustomerWebLogin || !teacher.._WebLogin)
                        //{
                        //    message = new MessageBox(MessageBoxType.Error, "Web Giriş Yetkilerinizi Kontrol Ediniz");
                        //}
                        //else
                        //{
                        //Logger.LogTransaction(ClientType.B2BWeb, LogTransactionSource.Login, ProcessLogin.Success.ToString(), string.Empty, ip, -1, customer.Id, customer.Users.Id);

                        message = new MessageBox(MessageBoxType.Success, "Giriş Başarılı");
                        Student CurrentStudent = Student.GetById(student.Id);
                        HttpContext.Session["Student"] = CurrentStudent;
                        HttpContext.Session["LoginType"] = CurrentLoginType;
                        FormsAuthentication.SetAuthCookie(CurrentStudent.Name, false);

                        return JsonConvert.SerializeObject(message);
                        // }
                    }
                    else
                    {
                        //string failMessage = "Kullanıcı Kodu :" + logon.UserCode + "    Şifre:" + logon.Password;
                        //Logger.LogTransaction(ClientType.Admin, LogTransactionSource.Login, ProcessLogin.Fail.ToString(), failMessage, ip, -1, -1, -1, checkTeacher == null ? -1 : checkTeacher.Id, -1);

                        message = new MessageBox(MessageBoxType.Error, "Kullanıcı Adı Veya Şifre Hatalı!");
                    }
                }
                else
                {
                    //string failMessage = "Kullanıcı Kodu :" + logon.UserCode + "    Şifre:" + logon.Password;
                    //Logger.LogTransaction(ClientType.Admin, LogTransactionSource.Login, ProcessLogin.Fail.ToString(), failMessage, ip, -1, -1, -1, checkTeacher == null ? -1 : checkTeacher.Id, -1);

                    message = new MessageBox(MessageBoxType.Error, "Girdiğiniz bilgilere ait bir hesap bulunamadı!");
                }
            }



            return JsonConvert.SerializeObject(message);
        }


        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            if (Request.Cookies[GlobalSettings.CookieName] != null)
            {
                HttpCookie myCookie = new HttpCookie(GlobalSettings.CookieName);
                myCookie.Expires = DateTime.Now.AddDays(-20);
                Response.Cookies.Add(myCookie);
            }


            return RedirectToAction("Index", "Login");

        }

        public ActionResult Unauthorized()
        {

            return View();
        }

    }
}