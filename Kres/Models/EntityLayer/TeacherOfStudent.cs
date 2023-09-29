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
    public class TeacherOfStudent : DataAccess
    {
        #region Constructors
        public TeacherOfStudent()
        {


        }
        #endregion

        #region Properties
        public int Id { get; set; }
        public int TeacherId { get; set; }
        public int StudentId { get; set; }
        public int Class { get; set; }
        public Teacher Teacher { get; set; }
        #endregion

        #region Method
        public static List<TeacherOfStudent> GetSalesmanOfCustomer(int pCustomerId)
        {
            //DataTable dt = DAL.GetSalesmanOfCustomer(pCustomerId);
            //List<TeacherOfStudent> list = new List<TeacherOfStudent>();

            //foreach (DataRow row in dt.Rows)
            //{
            //    TeacherOfStudent item = new TeacherOfStudent();
            //    {
            //        item.Id = row.Field<int>("Id");
            //        item.TeacherId = row.Field<int>("SalesmanId");
            //        item.Teacher = new Teacher()
            //        {
            //            Id = row.Field<int>("SalesmanId"),
            //            Code = row.Field<string>("Code"),
            //            Name = row.Field<string>("Name"),
            //            Tel1 = row.Field<string>("Tel1"),
            //            Tel2 = row.Field<string>("Tel2"),
            //            Email = row.Field<string>("Email"),
            //            PicturePath = row.Field<string>("PicturePath") == string.Empty || row.Field<string>("PicturePath") == null ? "../Content/Admin/images/noavatar.png" : GlobalSettings.FtpServerAddressFull + row.Field<string>("PicturePath"),

            //        };
            //    };
            //    list.Add(item);
            //}

            //return list;
            List<TeacherOfStudent> list = new List<TeacherOfStudent>();
            return list;
        }
        #endregion
    }

    public partial class DataAccessLayer
    {

    }
}