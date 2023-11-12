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
        public string PicturePath { get; set; }
        public int TeacherType { get; set; }
        public string Message { get; set; }
        public int NumberOfUser { get; set; }
        public int PermissionModerator { get; set; }
        public int PermisionB2B { get; set; }
        public int Status { get; set; }
        public int Deleted { get; set; }
        public int Type { get; set; }
        public bool IsSystemUser { get; set; }
        public bool Locked { get; set; }
        #endregion

        #region Method
        public static Teacher GetById(int pId)
        {
            Teacher item = new Teacher();
            DataTable dt = DAL.GetTeacherById(pId);

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                item.Id = row.Field<int>("Id");
                item.Code = row.Field<string>("Code");
                item.Name = row.Field<string>("Name");
                item.Password = row.Field<string>("Password");
                item.Tel1 = row.Field<string>("Tel1");
                item.Tel2 = row.Field<string>("Tel2");
                item.Email = row.Field<string>("Email");
                item.PicturePath = row.Field<string>("Picture");
               // item.PicturePath = row.Field<string>("PicturePath") == string.Empty || row.Field<string>("PicturePath") == null ? "../Content/Admin/images/noavatar.png" : GlobalSettings.FtpServerAddressFull + row.Field<string>("PicturePath");
                item.TeacherType = row.Field<int>("TeacherType");
                item.Message = row.Field<string>("Message");
                item.NumberOfUser = row.Field<int>("NumberOfUser");
                item.PermisionB2B = row.Field<int>("PermisionB2B");
                item.Status = row.Field<int>("Status");
                item.Deleted = row.Field<int>("Deleted");
                item.Type = row.Field<int>("Type");
               
                
                //item.AuthoritySalesman = new AuthoritySalesman()
                //{
                //    SalesmanId = row.Field<int>("Id"),
                //    CampaignStatu = row.Field<bool>("CampaignStatu"),
                //    ProductRestoration = row.Field<bool>("ProductRestoration"),
                //    LockSalesman = row.Field<bool>("LockSalesman"),
                //    Collecting = row.Field<bool>("Collecting"),
                //    EnteringInformation = row.Field<bool>("EnteringInformation"),
                //    CheckBasket = row.Field<bool>("CheckBasket"),
                //    CustomerType = row.Field<bool>("CustomerType"),
                //    HidePassword = row.Field<bool>("HidePassword"),
                //    WebLogin = row.Field<bool>("WebLogin"),
                //    IsSpecDiscount = row.Field<bool>("IsSpecDiscount"),
                //};
            }

            return item;
        }
        #endregion
    }

    public partial class DataAccessLayer
    {
        public DataTable GetTeacherById(int pId)
        {
            return DatabaseContext.ExecuteReader(CommandType.StoredProcedure, "_GetItem_TeacherById", MethodBase.GetCurrentMethod().GetParameters(), new object[] { pId });
        }
    }
}