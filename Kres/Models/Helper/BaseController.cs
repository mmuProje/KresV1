﻿using Kres.Models.EntityLayer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Kres.Models.Helper
{
    public class BaseController : Controller
    {
        #region Sessions
        protected LoginType CurrentLoginType
        {
            get { return Session["LoginType"] == null ? LoginType.None : (LoginType)Session["LoginType"]; }
            set { Session["LoginType"] = value; }
        }

        protected Teacher CurrentTeacher
        {
            get { return (Teacher)Session["Teacher"]; }
            set { Session["Teacher"] = value; }
        }

        //protected CurrentSessionTeacher CustomerBase
        //{
        //    get { return (CurrentSessionCustomer)Session["CustomerBase"]; }
        //    set { Session["CustomerBase"] = value; }
        //}


        protected Student CurrentStudent
        {
            get { return (Student)Session["Student"]; }
            set { Session["Student"] = value; }
        }

        protected List<TeacherOfStudent> CurrentTeacherOfStudent
        {
            get { return (List<TeacherOfStudent>)Session["TeacherOfStudent"]; }
            set { Session["TeacherOfStudent"] = value; }
        }
        protected FooterInformation FooterInformationItem
        {
            get { return (FooterInformation)Session["FooterInformationItem"]; }
            set { Session["FooterInformationItem"] = value; }
        }


        //protected List<EmailSender> EmailSenderList
        //{
        //    get { return (List<EmailSender>)Session["EmailSenderList"]; }
        //    set { Session["EmailSenderList"] = value; }
        //}
        protected List<Language> LanguageList
        {
            get { return (List<Language>)Session["LanguageList"]; }
            set { Session["LanguageList"] = value; }
        }
        protected Language CurrentLanguage
        {
            get { return (Language)Session["CurrentLanguage"]; }
            set { Session["CurrentLanguage"] = value; }
        }
        protected Dictionary<string, string> CurrentLocaleStringResource
        {
            get { return (Dictionary<string, string>)Session["LocaleStringResource"]; }
            set { Session["LocaleStringResource"] = value; }
        }
        protected CurrentSessions CurrentSessions
        {
            get
            {
                Session["CurrentSessions"] = new CurrentSessions
                {
                    Student = MappingList.MapNew<Student, CurrentSessionStudent>(CurrentStudent),
                    LoginType = CurrentLoginType,
                    //CompanyInformation = MappingList.MapNew<CompanyInformation, CurrentSessionCompanyInformation>(CompanyInformationItem),
                    Language = MappingList.MapNew<Language, CurrentSessionLanguage>(CurrentLanguage),
                    //TeacherOfStudent = MappingList.MapList<Teacher, TeacherOfStudent>(CurrentTeacherOfStudent.Select(x => x.Teacher).ToList()),
                    Teacher = MappingList.MapNew<Teacher, CurrentSessionTeacher>(CurrentTeacher),
                    LanguageList = MappingList.MapList<Language, CurrentSessionLanguage>(LanguageList),
                };
                return Session["CurrentSessions"] as CurrentSessions;
            }
        }


        public int CurrentSalesmanId { get { return (CurrentLoginType == LoginType.Teacher ? CurrentTeacher.Id : -1); } }
        //public int CurrentEditId { get { return (CurrentLoginType == LoginType.Student ? CurrentStudent.Id ); } }
        #endregion
        #region Methods
        private bool ControlApp(string Name, string loginType)
        {

            switch (loginType)
            {
                case "Student":
                    {
                        object isChange = HttpRuntime.Cache.Get(Name) == null ? false : HttpRuntime.Cache.Get(Name);
                        object isChangeGeneral = HttpRuntime.Cache.Get("StudentGeneralChange") == null ? false : HttpRuntime.Cache.Get("StudentGeneralChange");
                        bool retVal = (bool)isChange || (bool)isChangeGeneral ? true : false;
                        return (bool)isChange;
                    }
                case "Teacher":
                    {
                        object isChange = HttpRuntime.Cache.Get(Name) == null ? false : HttpRuntime.Cache.Get(Name);
                        return (bool)isChange;
                    }
                default:
                    return false;
            }
        }
        public static class MappingList
        {
            public static List<T> MapList<T, T1>(List<T> list1, List<T1> list2) where T : class, new() where T1 : class, new()
            {
                PropertyInfo[] TProperties = typeof(T).GetProperties();
                PropertyInfo[] T1Properties = typeof(T1).GetProperties();

                for (int i = 0; i < list2.Count; i++)
                {
                    list1.Insert(i, new T());
                    foreach (PropertyInfo item in TProperties)
                    {
                        if (item.Name == "CreateDate")
                        {

                        }
                        if (item.CanWrite)
                        {

                            if (item.Name != "BsonId" && item.Name != "ObjectId")
                            {
                                PropertyInfo[] value;
                                if (T1Properties.Any(x => x.Name == item.Name))
                                {
                                    if (item.PropertyType.Name.Length > 3 && item.PropertyType.Name.Substring(item.PropertyType.Name.Length - 3, 3).ToLower() == "dto")
                                    {
                                        value = list2[i].GetType().GetProperties();

                                    }
                                    else
                                    {
                                        value = list2[i].GetType().GetProperties();
                                        item.SetValue(list1[i], value.FirstOrDefault(x => x.Name == item.Name).GetValue(list2[i], new object[] { }));
                                    }
                                }
                            }
                        }
                    }
                }
                return list1;
            }

            public static T MapItem<T, T1>(T item1, T1 item2) where T : class, new() where T1 : class, new()
            {
                PropertyInfo[] TProperties = typeof(T).GetProperties();
                PropertyInfo[] T1Properties = typeof(T1).GetProperties();

                foreach (PropertyInfo itemProp in TProperties)
                {
                    if (itemProp.Name == "CreateDate")
                    {

                    }
                    if (itemProp.CanWrite)
                    {

                        if (itemProp.Name != "BsonId" && itemProp.Name != "ObjectId")
                        {
                            PropertyInfo[] value;
                            if (T1Properties.Any(x => x.Name == itemProp.Name))
                            {
                                if (itemProp.PropertyType.Name.Length > 3 && itemProp.PropertyType.Name.Substring(itemProp.PropertyType.Name.Length - 3, 3) == "DTO")
                                {
                                    value = item2.GetType().GetProperties();

                                }
                                else
                                {
                                    value = item2.GetType().GetProperties();
                                    itemProp.SetValue(item1, value.FirstOrDefault(x => x.Name == itemProp.Name).GetValue(item2, new object[] { }));
                                }
                            }
                        }
                    }

                }
                return item1;
            }
            public static T MapNew<F, T>(F from) where T : class, new()
            {
                string json = JsonConvert.SerializeObject(from);
                if (from == null)
                    json = JsonConvert.SerializeObject(new T());
                return JsonConvert.DeserializeObject<T>(json);
            }
            public static List<T> MapList<F, T>(List<F> from) where T : class, new()
            {
                string json = JsonConvert.SerializeObject(from);
                if (from == null)
                    json = JsonConvert.SerializeObject(new T());
                return JsonConvert.DeserializeObject<List<T>>(json);
            }
        }
        protected void ContextRedirectToAciton(ActionExecutingContext filterContext, string path)
        {
            //filterContext.Result = new RedirectResult(path);
            //filterContext.Result = JavaScript($"window.location='{path}'");
        }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {


            base.OnActionExecuting(filterContext);
            string loginPagePath = "/Login/";
            if (CurrentLoginType != LoginType.None)
            {
                if (ControlApp(this.CurrentStudent.Id.ToString(), "Student"))
                {
                    this.CurrentStudent = Student.GetById(this.CurrentStudent.Id);
                    HttpRuntime.Cache.Remove(this.CurrentStudent.Id.ToString());
                    HttpRuntime.Cache.Remove("StudentGeneralChange");
                }

                if (CurrentTeacher != null && CurrentTeacher.Id > 0 && ControlApp("Teacher" + CurrentTeacher.Id.ToString(), "Teacher"))
                {
                    this.CurrentTeacher = Teacher.GetById(this.CurrentTeacher.Id);
                }

                Session["SystemRoles"] = CurrentLoginType.ToString();
                if (CurrentLoginType == LoginType.Student && this.CurrentStudent == null)
                    ContextRedirectToAciton(filterContext, loginPagePath);
                if (CurrentLoginType == LoginType.Teacher && this.CurrentTeacher == null)
                    ContextRedirectToAciton(filterContext, loginPagePath);


                if (Session["SalesmanOfCustomer"] == null)
                {
                    if (this.CurrentStudent != null)
                    {
                        CurrentTeacherOfStudent = TeacherOfStudent.GetSalesmanOfCustomer(CurrentStudent.Id);
                    }
                    else
                    {
                        ContextRedirectToAciton(filterContext, loginPagePath);
                    }

                }
                if (LanguageList == null || LanguageList.Count == 0)
                {
                    LanguageList = Language.GetList();
                }


                if (CurrentLanguage == null || CurrentLanguage.Id <= 0)
                {
                    if (CurrentStudent != null && LanguageList.Any(x => x.Id == CurrentStudent.LanguageId)) LanguageList.First(x => x.Id == CurrentStudent.LanguageId).IsSelected = true;
                    else
                        LanguageList.First().IsSelected = true;
                    CurrentLanguage = (CurrentStudent != null && LanguageList.Any(x => x.Id == CurrentStudent.LanguageId)) ? LanguageList.First(x => x.Id == CurrentStudent.LanguageId) : LanguageList.Where(x => x.IsSelected).First();

                }

                if (CurrentLocaleStringResource == null || CurrentLocaleStringResource.Count == 0)
                    GetLocaleStringResource();


                //if (CurrentLanguage != null && FooterInformationItem == null)
                //{
                //    FooterInformationItem = FooterInformation.GetFooterItem(CurrentLanguage.Id);
                //    ViewBag.FooterInformationItem = FooterInformationItem;
                //}
                //else if (FooterInformationItem != null)
                //    ViewBag.FooterInformationItem = FooterInformationItem;
            }
            else
            {
                ContextRedirectToAciton(filterContext, loginPagePath);

            }
        }
        public void GetLocaleStringResource()
        {
            int langId = CurrentLanguage != null && CurrentLanguage.Id > 0 ? CurrentLanguage.Id : 1;
            CurrentLocaleStringResource = LocaleStringResource.GetListByLanguageId(langId);
        }

        #endregion
    }
    public static class MyExtensions
    {


        public static string toLanguage(this string val)
        {
            string str = val;
            try
            {
            repeat:
                Dictionary<string, string> LanguageFile = HttpContext.Current.Session["LocaleStringResource"] != null ? (Dictionary<string, string>)HttpContext.Current.Session["LocaleStringResource"] : null;

                if (LanguageFile != null) str = LanguageFile[val];
                else
                {
                    Language lang = HttpContext.Current.Session["CurrentLanguage"] as Language;
                    int langId = lang != null && lang.Id > 0 ? lang.Id : 1;
                    HttpContext.Current.Session["LocaleStringResource"] = LocaleStringResource.GetListByLanguageId(langId);
                    goto repeat;
                }


            }
            catch
            //(Exception ex)
            {
                str = LocaleStringResource.GetItemByResourceNameDefault(val);
            }

            return str;
        }
    }
    public class MessageBox
    {
        public string Statu { get; set; }
        public string Message { get; set; }
        public string Color { get; set; }
        public string Icon { get; set; }
        public int ResultId { get; set; }
        public double cmpAvailableQuantity { get; set; }
        public MessageBox(MessageBoxType statu, string message, int resultId = -1)
        {
            switch (statu)
            {
                case MessageBoxType.Error:
                    Statu = "error";
                    Color = "error";
                    Icon = "fa fa-ban";
                    break;
                case MessageBoxType.Info:
                    Statu = "info";
                    Color = "info";
                    Icon = "fa fa-info-circle";
                    break;
                case MessageBoxType.Success:
                    Statu = "success";
                    Color = "success";
                    Icon = "fa fa-check";
                    break;
                case MessageBoxType.Warning:
                    Statu = "warning";
                    Color = "warning";
                    Icon = "fa fa-exclamation-triangle";
                    break;
                case MessageBoxType.Payment:
                    Statu = "payment";
                    Color = "warning";
                    Icon = "fa fa-exclamation-triangle";
                    break;

            }
            Message = message;
            ResultId = resultId;

        }


    }

    #region Enums

    public enum EmailSenderEnum
    {
        Collecting = 1,
        Order = 2,
        SuggestionRequest = 3,
        EpaymentSuccess = 4,
        OrderConfirm = 5,
        UnSuccessEpayment = 6,
        ReturnProduct = 7,
        TeacherVisit = 8,
        CreateRegister = 9,
        TeacherTask = 10

    }
    public enum MessageBoxType
    {
        Success,
        Error,
        Warning,
        Info,
        Payment

    }

    public enum LoginType
    {
        Student = 0,
        Teacher = 1,
        Admin = 2,
        None = 99,
    }

    public enum SystemType
    {
        Android = 3,
        Web = 1,
        WinForm = 2
    }

    public enum AnnouncementsType
    {
        Article = 0,
        Picture = 1,
        Campaign = 2,
        VisualOpeningMessage = 3,
        GeneralOpeningMessage = 4,
        TeacherOpeningMessage = 5,
        MenuFloatingWriting = 6,
        VirtualPosAnnouncement = 7,
        VirtualPosPicture = 8,
        Brands = 9,
        SearchPageBanner = 10,
        ShipmentType = 11,
        LoginPictures = 12
    }

    public enum PosBanks
    {
        Garanti = 1,
        Akbank = 2,
        IsBank = 3,
        Finansbank = 4,
        Halkbank = 5,
        Anadolubank = 6,
        Turkiyefinans = 7,
        Teb = 8,
        Yapikredi = 9,
        Kuveytturk = 10,
        Hsbc = 12,
        Vakifbank = 13,
        Ziraatbank = 14,
        Denizbank = 15,
        Ingbank = 16,
        Sekerbank = 17,
        Albaraka = 18,
        IsBankInnova = 19,
        ZiraatbankInnova = 20,
        QNBFinansbank = 21
    }
    public enum Bank
    {
        ISBANK = 30,
        AKBANK = 22,
        YAPIKREDI = 27,
        TEB = 32,
        FINANSBANK = 1,
        HALKBANK = 11,
        GARANTIBANK = 34,
        ZIRAATBANK = 31,
        DENIZBANK = 35,
        VAKIFBANK = 26,
        SEKERBANK = 35,
        ING = 9,
        HSBC = 8,
        TURKIYEFINANS = 23,
        KUVEYTTURK = 37,
        BELIRSIZ = 99
    }

    public enum _3DSecureType
    {
        OnlyNonSecure = 0,
        Only3D = 1,
        NonSecureOr3D = 2,
        Student3DandTeacherNonsecure = 3

    }

    public enum TransactionType
    {
        Sale = 1,
        PointSearch = 2,
        PointSale = 3,
        Cancel = 4,
        Refund = 5
    }
    #endregion
}