using Kres.Models.DataLayer;
using Kres.Models.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Xml.Linq;

namespace Kres.Models.EntityLayer
{
    public class Logon : DataAccess
    {
        #region Properties
        public string Password { get; set; }
        public string UserCode { get; set; }
        public string Captcha { get; set; }
        #endregion


        #region Methods

        public Teacher TeacherLogin()
        {
            DataTable dt = DAL.LoginTeacher(UserCode, Password, (int)SystemType.Web);
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                return Teacher.GetById(row.Field<int>("Id"));
            }
            return null;
        }

        public Student StudentLogin()
        {
            DataTable dt = DAL.LoginStudent(UserCode, Password, (int)SystemType.Web);
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                return Student.GetById(row.Field<int>("Id"));
            }
            return null;
        }

        public Teacher TeacherAuthenticationCheck()
        {
            DataTable dt;

            dt = DAL.TeacherAuthenticationCheck(UserCode);

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                Teacher item = new Teacher()
                {
                    Id = row.Field<int>("Id")
                    //IsAuthenticator = row.Field<bool>("IsAuthenticator"),
                    //AuthenticatorGuid = row.Field<string>("AuthenticatorGuid"),
                };
                return item;
            }
            return null;
        }

        public Student StudentAuthenticationCheck()
        {
            DataTable dt;

            dt = DAL.StudentAuthenticationCheck(UserCode);

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                Student item = new Student()
                {
                    Id = row.Field<int>("Id")
                    //IsAuthenticator = row.Field<bool>("IsAuthenticator"),
                    //AuthenticatorGuid = row.Field<string>("AuthenticatorGuid"),
                    //IsB2bAuthenticator = row.Field<bool>("IsB2bAuthenticator"),
                };
                return item;
            }
            return null;
        }

        #endregion

    }
    public partial class DataAccessLayer
    {
        public DataTable LoginTeacher(string pUserCode, string pPassword, int pSystemType)
        {
            return DatabaseContext.ExecuteReader(CommandType.StoredProcedure, "_GetItem_Teacher_Login", MethodBase.GetCurrentMethod().GetParameters(), new object[] { pUserCode, pPassword, pSystemType });
        }

        public DataTable LoginStudent(string pUserCode, string pPassword, int pSystemType)
        {
            return DatabaseContext.ExecuteReader(CommandType.StoredProcedure, "_GetItem_Student_Login", MethodBase.GetCurrentMethod().GetParameters(), new object[] { pUserCode, pPassword, pSystemType });
        }
        public DataTable StudentAuthenticationCheck(string pUserCode)
        {
            return DatabaseContext.ExecuteReader(CommandType.StoredProcedure, "_GetItem_Student_Authentication", MethodBase.GetCurrentMethod().GetParameters(), new object[] { pUserCode });
        }
        public DataTable TeacherAuthenticationCheck(string pUserCode)
        {
            return DatabaseContext.ExecuteReader(CommandType.StoredProcedure, "_GetItem_Teacher_Authentication", MethodBase.GetCurrentMethod().GetParameters(), new object[] { pUserCode });
        }
    }
}