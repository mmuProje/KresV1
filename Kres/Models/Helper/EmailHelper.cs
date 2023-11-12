using Kres.Models.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace Kres.Models.Helper
{
    public class EmailHelper:DataAccessLayer
    {
        public static Tuple<bool, string> Send(MailMessage mail, int type = -1)
        {
           return new Tuple<bool, string>(false, "Hata oluştu");
        }
    }
}