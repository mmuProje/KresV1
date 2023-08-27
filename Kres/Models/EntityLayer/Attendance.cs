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
    public class Attendance : DataAccess
    {
        #region Constructors
        public Attendance()
        {


        }
        #endregion

        #region Properties
        public int Id { get; set; }
        public int Attendance_Detail { get; set; }
        public int TeacherId { get; set; }
        public int ClassId { get; set; }
        public DateTime RecordDate { get; set; }
        public DateTime ConfirmDate { get; set; }
        #endregion

        #region Method

        #endregion
    }

    public partial class DataAccessLayer
    {

    }
}