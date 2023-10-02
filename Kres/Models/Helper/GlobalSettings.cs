using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Kres.Models.Helper
{
    public class GlobalSettings
    {
        public static string Upload = "Upload/";
        public static string Contact = "Contact/";


        public static string Ip => ConfigurationManager.AppSettings["ip"];

        public static string UserName => ConfigurationManager.AppSettings["username"];

        public static string Password => ConfigurationManager.AppSettings["password"];

        public static uint Port => Convert.ToUInt32(ConfigurationManager.AppSettings["port"]);

        public static string Database => ConfigurationManager.AppSettings["database"];

        public static string DatabaseEncoding => ConfigurationManager.AppSettings["dbEncoding"];

        private static string eryazPortalWsAddress;
        public static string EryazPortalWsAddress
        {
            get
            {
                if (string.IsNullOrEmpty(eryazPortalWsAddress))
                {
                    eryazPortalWsAddress = ConfigurationManager.AppSettings["EryazPortalWsAddress"];
                }
                return eryazPortalWsAddress;
            }
        }

        public static string CookieName => ConfigurationManager.AppSettings["cookieName"];

        public static string CookieNameUserName => ConfigurationManager.AppSettings["cookieNameForUserName"];

        public static string B2bAddress => ConfigurationManager.AppSettings["b2bAddress"];
        public static string B2bAddressLocal => ConfigurationManager.AppSettings["b2bAddressLocal"];
        public static string FtpServerAddress;//=> "https://" + ConfigurationManager.AppSettings["ftpServerAddress"];
        public static string FtpServerAddressFull => "https://" + FtpServerAddress + FtpCompanyName+"/";
        public static string FtpServerUploadAddress => "ftp://" + FtpServerAddress;
        public static string FtpCompanyName; //=> ConfigurationManager.AppSettings["ftpCompanyName"];
        public static string FtpUserName;// => ConfigurationManager.AppSettings["ftpUserName"];
        public static string FtpPassword;//=> ConfigurationManager.AppSettings["ftpPassword"];
        public static string CompanyName => ConfigurationManager.AppSettings["companyName"];
        public static string CompanyPath => ConfigurationManager.AppSettings["companyPath"];
        public static string GeneralPath = "General/";
        public static string TeacherPath = "Teacher/";
        public static string RegisterStudentPath = "RegisterStudent/";
        public static string EncryptPassword => ConfigurationManager.AppSettings["encryptPassword"];

        public static string PaymentLogPassword => ConfigurationManager.AppSettings["paymentLogPassword"];

        public static string PaymentLogAdress => ConfigurationManager.AppSettings["paymentLogAdress"];


        public static string FileUploadBasePath => ConfigurationManager.AppSettings["fileUploadBasePath"];

        public static string EncryptKey { get {return "cNqXTA87wed24nFmRq"; } }

        public static string ConnectionString
        {
            get
            {
                string cs = string.Empty;
                SqlConnectionStringBuilder msbuilder = new SqlConnectionStringBuilder();

                msbuilder.DataSource = GlobalSettings.Port == 1433 ? GlobalSettings.Ip : GlobalSettings.Ip+","+ GlobalSettings.Port;
                msbuilder.InitialCatalog = GlobalSettings.Database;
                msbuilder.UserID = GlobalSettings.UserName; ;
                msbuilder.Password = GlobalSettings.Password; ;
                msbuilder.ConnectTimeout = 120;

                cs = msbuilder.ToString();

                return cs;
            }
        }

    }
}