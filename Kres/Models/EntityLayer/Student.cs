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
    public class Student : DataAccess
    {

        #region Constructors
        public Student()
        {


        }
        #endregion

        #region Properties
        public int Id { get; set; }
        public string Code { get; set; }
        public string LoginCode { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string GuardianName { get; set; }
        public string Address { get; set; }
        public string Town { get; set; }
        public string City { get; set; }
        public string Mail { get; set; }
        public bool Web { get; set; }
        public bool Android { get; set; }
        public bool Ios { get; set; }
        public int Status { get; set; }
        public int PaymentType { get; set; }
        public string Tel1 { get; set; }
        public string Tel2 { get; set; }
        public string Fax1 { get; set; }
        public string Gsm1 { get; set; }
        public string Gsm2 { get; set; }
        public string Notes { get; set; }
        public DateTime RecordDate { get; set; }
        public DateTime EditDate { get; set; }
        public string TradingGroup { get; set; }
        public string PaymentCode { get; set; }
        public bool PaymentOnOrder { get; set; }
        public bool StatuOriginalPrice { get; set; }
        #endregion

        #region Method

        #endregion

    }

    public partial class DataAccessLayer
    {

    }
}