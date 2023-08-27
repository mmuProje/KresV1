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
    public class AttendanceDetail : DataAccess
    {
        #region Constructors
        public AttendanceDetail()
        {


        }
        #endregion

        #region Properties
        public int Id { get; set; }
        public int AttendanceId { get; set; }
        public int StudentId { get; set; }
        public int TeacherId { get; set; }
        public int ClassId { get; set; }
        public int Statu { get; set; }
        public DateTime RecordDate { get; set; }
        public int Deleted { get; set; }
        #endregion

        #region Method

        #endregion
    }

    public partial class DataAccessLayer
    {

    }
}