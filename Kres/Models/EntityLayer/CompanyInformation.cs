using Kres.Models.DataLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Security;

namespace Kres.Models.EntityLayer
{
    public class CompanyInformation : DataAccess
    {
        #region Constructors
        public CompanyInformation()
        {


        }
        #endregion

        #region Properties
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Title { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Gsm { get; set; }
        public string Fax { get; set; }
        public string Email1 { get; set; }
        public string Email2 { get; set; }
        public string Picture { get; set; }
        public string RegistrationNo { get; set; }
        public string WebSite { get; set; }
        public string Address { get; set; }
        public int Status { get; set; }
        public int CustomerInstallment { get; set; }
        public string MapPath { get; set; }
        #endregion

        #region Method

        #endregion
    }

    public partial class DataAccessLayer
    {

    }
}