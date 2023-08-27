using Kres.Models.DataLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace Kres.Models.EntityLayer
{
    public class Currency : DataAccess
    {
        #region Constructors
        public Currency()
        {


        }
        #endregion

        #region Properties
        public int Id { get; set; }
        public string Type { get; set; }
        public double Rate { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime EditDate { get; set; }
        public int CreateId { get; set; }
        public int EditId { get; set; }
        public bool CheckBist { get; set; }
        #endregion

        #region Method

        #endregion
    }

    public partial class DataAccessLayer
    {

    }
}