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
    public class Classes : DataAccess
    {
        #region Constructors
        public Classes()
        {


        }
        #endregion

        #region Properties
        public int Id { get; set; }
        public int Type { get; set; }
        public int StudentId { get; set; }
        public string StudentCode { get; set; }
        public string Class { get; set; }
        public string Branch { get; set; }
        public int Status { get; set; }
        #endregion

        #region Method

        #endregion
    }

    public partial class DataAccessLayer
    {

    }
}