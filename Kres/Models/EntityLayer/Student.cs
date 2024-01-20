using Kres.Models.DataLayer;
using Kres.Models.Helper;
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
        public DateTime Age { get; set; }
        public string Gender { get; set; }
        public string TradingGroup { get; set; }
        public string PaymentCode { get; set; }
        public string PicturePath { get; set; }
        public bool PaymentOnOrder { get; set; }
        public bool StatuOriginalPrice { get; set; }
        public int LanguageId { get; set; }
        #endregion

        #region Method
        public static Student GetById(int pId)
        {
            Student item = new Student();
            DataTable dt = DAL.GetStudentById(pId);

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                item.Id = row.Field<int>("Id");
                item.Code = row.Field<string>("Code");
                item.LoginCode = row.Field<string>("LoginCode");
                item.Password = row.Field<string>("Password");
                item.Name = row.Field<string>("Name");
                item.GuardianName = row.Field<string>("GuardianName");
                item.Address = row.Field<string>("Address");
                item.Town = row.Field<string>("Town");
                item.City = row.Field<string>("City");
                item.Mail = row.Field<string>("Mail");
                //item.Web = row.Field<bool>("Web");
                //item.Android = row.Field<bool>("Android");
                //item.Ios = row.Field<bool>("Ios");
                item.Status = row.Field<int>("Status");
                item.PaymentType = row.Field<int>("PaymentType");
                item.Tel1 = row.Field<string>("Tel1");
                item.Tel2 = row.Field<string>("Tel2");
                item.Fax1 = row.Field<string>("Fax1");
                item.Gsm1 = row.Field<string>("Gsm1");
                item.Gsm2 = row.Field<string>("Gsm2");
                item.Notes = row.Field<string>("Notes");
                item.RecordDate = row.Field<DateTime>("RecordDate");
                item.EditDate = row.Field<DateTime>("EditDate");
                item.TradingGroup = row.Field<string>("TradingGroup");
                item.PaymentCode = row.Field<string>("PaymentCode");
                //item.PaymentOnOrder = row.Field<bool>("PaymentOnOrder");
                //item.StatuOriginalPrice = row.Field<bool>("StatuOriginalPrice");
                // item.PicturePath = row.Field<string>("PicturePath") == string.Empty || row.Field<string>("PicturePath") == null ? "../Content/Admin/images/noavatar.png" : GlobalSettings.FtpServerAddressFull + row.Field<string>("PicturePath"); 
                // item.Deleted = row.Field<int>("Deleted")
            }

            return item;
        }


       // public static List<Student> GetListStudent(DateTime startDate, DateTime endDate, string t9Text, int studentStatu)
        public static List<Student> GetListStudent(string t9Text)
        {
            using (DataTable dt = DAL.GetStudentList(t9Text))
            {
                List<Student> list = new List<Student>();
                foreach (DataRow row in dt.Rows)
                {
                    Student students = new Student()
                    {
                        Id = row.Field<int>("Id"),
                        Code = row.Field<string>("Code"),
                        Name = row.Field<string>("Name"),
                        GuardianName = row.Field<string>("GuardianName"),
                        Age = row.Field<DateTime>("Age"),
                        Gender = row.Field<string>("Gender"),
                        PicturePath = row.Field<string>("PicturePath") == string.Empty ? "https://" + HttpContext.Current.Request.Url.Authority + "/Content/images/nophoto.png" : row.Field<string>("PicturePath")
                };
                    list.Add(students);
                }
                return list;
            }
        }
        #endregion

    }

    public partial class DataAccessLayer
    {
        public DataTable GetStudentById(int pId)
        {
            return DatabaseContext.ExecuteReader(CommandType.StoredProcedure, "_GetItem_StudentById", MethodBase.GetCurrentMethod().GetParameters(), new object[] { pId });
        }
        public DataTable GetStudentList(string pT9Text)
        {
            return DatabaseContext.ExecuteReader(CommandType.StoredProcedure, "_GetList_Student", MethodBase.GetCurrentMethod().GetParameters(), new object[] { pT9Text });
        }
    }
}


