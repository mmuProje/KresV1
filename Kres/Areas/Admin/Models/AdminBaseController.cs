using System.Web.Mvc;
using Kres.Models.EntityLayer;
using System.Net;
using Kres.Models.Helper;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using RestSharp;
using System.Net.Mail;
using System.Web;
using static Kres.Models.Helper.BaseController;

namespace Kres.Areas.Admin.Models
{
    public class AdminBaseController:Controller
    {
        #region Properties
        protected Teacher AdminCurrentTeacher
        {
            get { return (Teacher)Session["AdminTeacher"]; }
            set { Session["AdminTeacher"] = value; }
        }

        protected Language CurrentLanguage
        {
            get { return (Language)Session["CurrentLanguage"]; }
            set { Session["CurrentLanguage"] = value; }
        }

        protected List<Language> LanguageList
        {
            get { return (List<Language>)Session["LanguageList"]; }
            set { Session["LanguageList"] = value; }
        }

        protected Dictionary<string, string> CurrentLocaleStringResource
        {
            get { return (Dictionary<string, string>)Session["LocaleStringResource"]; }
            set { Session["LocaleStringResource"] = value; }
        }

        protected AdmimCurrentSessions AdminCurrentSessions
        {
            get
            {
                Session["AdminCurrentSessions"] = new AdmimCurrentSessions
                {
                    Language = MappingList.MapNew<Language, CurrentSessionLanguage>(CurrentLanguage),
                    Teacher = MappingList.MapNew<Teacher, CurrentSessionTeacher>(AdminCurrentTeacher),
                    LanguageList = MappingList.MapList<Language, CurrentSessionLanguage>(LanguageList),
                };
                return Session["AdminCurrentSessions"] as AdmimCurrentSessions;
            }
        }

        #endregion

        #region Methods
        public string GetUserIpAddress()
        {
            string ip = HttpContext.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(ip))
                ip = HttpContext.Request.ServerVariables["REMOTE_ADDR"];
            return ip;
        }
        public string GetControllerName()
        {
            return this.ControllerContext.RouteData.Values["controller"] + "=>" + this.ControllerContext.RouteData.Values["action"];
        }

        

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            //if (!filterContext.Controller.ControllerContext.HttpContext.Request.CurrentExecutionFilePath.Contains("FireSyncDesign"))
            //{
            //    if (AdminCurrentTeacher == null || AdminCurrentTeacher.Id == 0)
            //    {
            //        filterContext.Result = new RedirectResult("~/Admin/Login/Logout");
            //    }
            //    else if (AdminCurrentTeacher.Locked)
            //    {
            //        filterContext.Result = new RedirectResult("~/Admin/Login/Locked");
            //    }
            //    else
            //    {
            //        if (GetControllerName() != "Home=>GetLocaleString")
            //            Logger.LogNavigation(-1, -1, AdminCurrentTeacher.Id, GetControllerName(), ClientType.Admin, GetUserIpAddress());

            //        ViewBag.AdminCurrentTeacher = AdminCurrentTeacher;
            //    }

            //}

            
            ViewBag.SystemPaymentControlInformation = Session["SystemPaymentControlInformation"];
            ViewBag.SystemPaymentControlLink = Session["SystemPaymentControlLink"];


            if (LanguageList == null || LanguageList.Count == 0)
            {
                LanguageList = Language.GetList();

                LanguageList.First().IsSelected = true;
                CurrentLanguage = LanguageList.Where(x => x.IsSelected).First();
            }


        }
        //protected override void OnAuthorization(AuthorizationContext filterContext)
        //{
        //    if (filterContext.HttpContext.Session["AdminTeacher"] == null)
        //    {
        //        filterContext.Result = new RedirectResult("~/Admin/Login/Logout");
        //    }

        //}

        

        public Tuple<bool, string> SendB4BEmail(EmailSender senderItem, string contentHtml, List<TeacherOfStudent> currentTeacherOfStudent, Student CurrentStudent)
        {

            string toEmailList = !String.IsNullOrEmpty(senderItem.EMailGroup.ToEmailList) ? senderItem.EMailGroup.ToEmailList : "";

            toEmailList += (senderItem.IsSendStudent && !String.IsNullOrEmpty(CurrentStudent.Mail)) ? ("," + CurrentStudent.Mail) : "";

            if (senderItem.IsSendTeacher)
            {
                string sEmailAdres = "";

                foreach (TeacherOfStudent item in currentTeacherOfStudent)
                {
                    sEmailAdres += item.Teacher.Email + ",";
                }

                toEmailList += (!String.IsNullOrEmpty(sEmailAdres)) ? ("," + sEmailAdres) : "";
            }
            if (!String.IsNullOrEmpty(senderItem.EMailGroup.ToOtherEmailList))
            {
                senderItem.EMailGroup.ToOtherEmailList = senderItem.EMailGroup.ToOtherEmailList.Replace(";", ",");
                toEmailList = toEmailList + "," + senderItem.EMailGroup.ToOtherEmailList + ",";
            }
            toEmailList = toEmailList.Substring(toEmailList.Length - 1, 1) == "," ? toEmailList.Substring(0, toEmailList.Length - 1) : toEmailList;
            string[] toEmailListData = toEmailList.Split(',');
            toEmailList = string.Empty;
            foreach (string item in toEmailListData)
            {
                if (IsValidEmail(item))
                    toEmailList = toEmailList + item + ",";
            }
            if (toEmailListData.Length > 0)
                toEmailList = toEmailList.Substring(0, toEmailList.Length - 1);

            MailMessage mail = new MailMessage();
            mail.To.Add(toEmailList);
            if (!String.IsNullOrEmpty(senderItem.EMailGroup.CCEmailList))
            {
                senderItem.EMailGroup.CCEmailList = senderItem.EMailGroup.CCEmailList.Replace(";", ",");
                string[] cCEmailListtData = senderItem.EMailGroup.CCEmailList.Split(',');
                senderItem.EMailGroup.CCEmailList = string.Empty;
                foreach (string item in cCEmailListtData)
                {
                    if (IsValidEmail(item))
                        senderItem.EMailGroup.CCEmailList = senderItem.EMailGroup.CCEmailList + item + ",";
                }
                if (cCEmailListtData.Length > 0)
                    senderItem.EMailGroup.CCEmailList = senderItem.EMailGroup.CCEmailList.Substring(0, senderItem.EMailGroup.CCEmailList.Length - 1);

                mail.CC.Add(senderItem.EMailGroup.CCEmailList);
            }

            if (!String.IsNullOrEmpty(senderItem.EMailGroup.BCCEmailList))
            {
                senderItem.EMailGroup.BCCEmailList = senderItem.EMailGroup.BCCEmailList.Replace(";", ",");

                string[] bCCEmailListData = senderItem.EMailGroup.BCCEmailList.Split(',');
                senderItem.EMailGroup.BCCEmailList = string.Empty;
                foreach (string item in bCCEmailListData)
                {
                    if (IsValidEmail(item))
                        senderItem.EMailGroup.BCCEmailList = senderItem.EMailGroup.BCCEmailList + item + ",";
                }
                if (bCCEmailListData.Length > 0)
                    senderItem.EMailGroup.BCCEmailList = senderItem.EMailGroup.BCCEmailList.Substring(0, senderItem.EMailGroup.BCCEmailList.Length - 1);



                mail.Bcc.Add(senderItem.EMailGroup.BCCEmailList);
            }

            mail.Body = contentHtml;
            mail.IsBodyHtml = true;
            mail.Subject = senderItem.Name;



            return EmailHelper.Send(mail);

        }

        public bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        protected byte[] Parse(string base64Content)
        {
            if (string.IsNullOrEmpty(base64Content))
            {
                throw new ArgumentNullException(nameof(base64Content));
            }

            int indexOfSemiColon = base64Content.IndexOf(";", StringComparison.OrdinalIgnoreCase);

            string dataLabel = base64Content.Substring(0, indexOfSemiColon);

            string contentType = dataLabel.Split(':').Last();

            var startIndex = base64Content.IndexOf("base64,", StringComparison.OrdinalIgnoreCase) + 7;

            var fileContents = base64Content.Substring(startIndex);

            byte[] bytes = Convert.FromBase64String(fileContents);

            return bytes;
        }

        protected string GetFileType(string imageBase)
        {
            int indexOfSemiColon = imageBase.IndexOf(";", StringComparison.OrdinalIgnoreCase);
            string dataLabel = imageBase.Substring(0, indexOfSemiColon);
            string imgType = dataLabel.Split(':').Last().Split('/').Last();

            #region allType
            switch (imgType)
            {
                case "msword":
                    imgType = "doc";
                    break;
                case "vnd.openxmlformats-officedocument.wordprocessingml.document":
                    imgType = "docx";
                    break;
                case "vnd.openxmlformats-officedocument.wordprocessingml.template":
                    imgType = "dotx";
                    break;
                case "vnd.ms-word.document.macroEnabled.12":
                    imgType = "docm";
                    break;
                case "vnd.ms-word.template.macroEnabled.12":
                    imgType = "dotm";
                    break;
                case "vnd.ms-excel":
                    imgType = "xls";
                    break;
                case "vnd.openxmlformats-officedocument.spreadsheetml.sheet":
                    imgType = "xlsx";
                    break;
                case "vnd.openxmlformats-officedocument.spreadsheetml.template":
                    imgType = "xltx";
                    break;
                case "vnd.ms-excel.sheet.macroEnabled.12":
                    imgType = "xlsm";
                    break;
                case "vnd.ms-excel.template.macroEnabled.12":
                    imgType = "xltm";
                    break;
                case "vnd.ms-excel.addin.macroEnabled.12":
                    imgType = "xlam";
                    break;
                case "vnd.ms-excel.sheet.binary.macroEnabled.12":
                    imgType = "xlsb";
                    break;
                case "vnd.ms-powerpoint":
                    imgType = "ppt";
                    break;
                case "vnd.openxmlformats-officedocument.presentationml.presentation":
                    imgType = "pptx";
                    break;
                case "vnd.openxmlformats-officedocument.presentationml.template":
                    imgType = "potx";
                    break;
                case "vnd.openxmlformats-officedocument.presentationml.slideshow":
                    imgType = "ppsx";
                    break;
                case "vnd.ms-powerpoint.addin.macroEnabled.12":
                    imgType = "ppam";
                    break;
                case "vnd.ms-powerpoint.presentation.macroEnabled.12":
                    imgType = "pptm";
                    break;
                case "vnd.ms-powerpoint.template.macroEnabled.12":
                    imgType = "potm";
                    break;
                case "vnd.ms-powerpoint.slideshow.macroEnabled.12":
                    imgType = "ppsm";
                    break;
                case "vnd.ms-access":
                    imgType = "mdb";
                    break;

                case "video/3gpp":
                    imgType = "3gp";
                    break;
                case "":
                    imgType = "";
                    break;
                case "video/x-ms-asf":
                    imgType = "asf";
                    break;
                case "video/x-msvideo":
                    imgType = "avi";
                    break;
                case "application/octet-stream":
                    imgType = "bin";
                    break;
                case "image/bmp":
                    imgType = "bmp";
                    break;
                case "text/css":
                    imgType = "css";
                    break;
                case "text/csv":
                    imgType = "csv";
                    break;
                case "image/vnd.dwg":
                    imgType = "dwg";
                    break;
                case "image/vnd.dxf":
                    imgType = "dxf";
                    break;
                case "x-msdownload":
                    imgType = "exe";
                    break;
                case "video/x-flv":
                    imgType = "flv";
                    break;
                case "image/gif":
                    imgType = "gif";
                    break;
                case "text/html":
                    imgType = "html";
                    break;
                case "	image/x-icon":
                    imgType = "ico";
                    break;
                case "java-archive":
                    imgType = "";
                    break;
                case "javascript":
                    imgType = "js";
                    break;
                case "video/mp4":
                    imgType = "mp4";
                    break;
                case "video/mpeg":
                    imgType = "mpeg";
                    break;
                case "vnd.oasis.opendocument.database":
                    imgType = "odb";
                    break;
                case "vnd.oasis.opendocument.chart":
                    imgType = "odb";
                    break;
                case "vnd.oasis.opendocument.formula":
                    imgType = "odf";
                    break;

                case "vnd.oasis.opendocument.spreadsheet":
                    imgType = "ods";
                    break;

                case "vnd.oasis.opendocument.text":
                    imgType = "odt";
                    break;

                case "x-font-otf":
                    imgType = "otf";
                    break;

                case "vnd.oasis.opendocument.presentation-template":
                    imgType = "otp";
                    break;

                case "vnd.oasis.opendocument.text-template":
                    imgType = "ott";
                    break;

                case "vnd.palm":
                    imgType = "pdb";
                    break;
                case "pdf":
                    imgType = "pdf";
                    break;
                case "vnd.adobe.photoshop":
                    imgType = "psd";
                    break;
                case "x-mspublisher":
                    imgType = "pub";
                    break;
                case "x-rar-compressed":
                    imgType = "rar";
                    break;
                case "svg+xml":
                    imgType = "svg";
                    break;
                case "x-shockwave-flash":
                    imgType = "swf";
                    break;
                case "tiff":
                    imgType = "tiff";
                    break;
                case "plain":
                    imgType = "txt";
                    break;
                case "x-ms-wma":
                    imgType = "wma";
                    break;
                case "vnd.ms-works":
                    imgType = "wps";
                    break;
                case "vnd.xara":
                    imgType = "xar";
                    break;
                case "xml":
                    imgType = "xml";
                    break;
                case "zip":
                    imgType = "zip";
                    break;
                default:
                    break;
            }
            #endregion

            return imgType;
        }
        public void GetLocaleStringResource()
        {
            int langId = CurrentLanguage != null && CurrentLanguage.Id > 0 ? CurrentLanguage.Id : 1;
            CurrentLocaleStringResource = LocaleStringResource.GetListByLanguageId(langId);
        }



        #endregion
    }
}