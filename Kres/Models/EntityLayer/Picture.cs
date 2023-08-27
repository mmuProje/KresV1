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
    public class Picture : DataAccess
    {
        #region Constructors
        public Picture()
        {


        }
        #endregion

        #region Properties
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductCode { get; set; }
        public string Path { get; set; }
        public int IsDefault { get; set; }
        #endregion

        #region Method

        #endregion
    }

    public partial class DataAccessLayer
    {

    }
}