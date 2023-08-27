using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kres.Models.EntityLayer
{
    public class BaseController : Controller
    {
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