using Kres.Models.DataLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Kres.Models.EntityLayer
{
    public class EMailGroup : DataAccess
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ToEmailIdList { get; set; }
        public string ToEmailList { get; set; }
        public string[] ToEmailIdListArr { get; set; }
        public string CCEmailIdList { get; set; }
        public string CCEmailList { get; set; }
        public string[] CCEmailIdListArr { get; set; }
        public string ToOtherEmailList { get; set; }
        public string BCCEmailList { get; set; }


        public bool InsertOrUpdate()
        {
            return DAL.InsertOrUpdate(Id, Name, ToEmailIdList, ToOtherEmailList, CCEmailIdList, BCCEmailList, CreateId, EditId, Deleted);
        }

        public static List<EMailGroup> GetList()
        {
            using (DataTable dt = DAL.GetListMailGroup())
            {
                List<EMailGroup> list = new List<EMailGroup>();
                foreach (DataRow row in dt.Rows)
                {
                    EMailGroup item = new EMailGroup()
                    {
                        Id = row.Field<int>("Id"),
                        Name = row.Field<string>("Name"),
                        ToEmailIdList = row.Field<string>("ToEmailIdList"),
                        ToEmailIdListArr = row.Field<string>("ToEmailIdList").Split(','),
                        CCEmailIdList = row.Field<string>("CCEmailIdList"),
                        CCEmailIdListArr = row.Field<string>("CCEmailIdList").Split(','),
                        ToOtherEmailList = row.Field<string>("ToOtherEmailList"),
                        BCCEmailList = row.Field<string>("BCCEmailList")
                    };
                    list.Add(item);
                }
                return list;
            }
        }

    }

    public partial class DataAccessLayer
    {
        public bool InsertOrUpdate(int pId, string pName, string pToEmailIdList, string pToOtherEmailList, string pCCEmailIdList, string pBCCEmailList, int pCreateId, int pEditId, bool pDeleted)
        {
            return DatabaseContext.ExecuteNonQuery(CommandType.StoredProcedure, "_Admin_Insert_Or_Update_MailGroup", MethodBase.GetCurrentMethod().GetParameters(), new object[] { pId, pName, pToEmailIdList, pToOtherEmailList, pCCEmailIdList, pBCCEmailList, pCreateId, pEditId, pDeleted });
        }

        public DataTable GetListMailGroup()
        {
            return DatabaseContext.ExecuteReader(CommandType.StoredProcedure, "_Admin_GetList_MailGroup", MethodBase.GetCurrentMethod().GetParameters(), new object[] { });
        }



    }
}