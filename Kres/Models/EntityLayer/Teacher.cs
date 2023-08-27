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
    public class Teacher : DataAccess
    {
        #region Constructors
        public Teacher()
        {


        }
        #endregion

        #region Properties
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Tel1 { get; set; }
        public string Tel2 { get; set; }
        public string Email { get; set; }
        public string Picture { get; set; }
        public int TeacherType { get; set; }
        public string Message { get; set; }
        public int NumberOfUser { get; set; }
        public int PermissionModerator { get; set; }
        public int PermisionB2B { get; set; }
        public int Status { get; set; }
        public int Deleted { get; set; }
        public int Type { get; set; }
        #endregion

        #region Method

        #endregion
    }

    public partial class DataAccessLayer
    {
       
    }
}