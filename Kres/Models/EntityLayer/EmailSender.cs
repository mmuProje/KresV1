using Kres.Models.DataLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Kres.Models.EntityLayer
{
    public class EmailSender : DataAccess
    {
        public int Id { get; set; }
        public int MailGroupId { get; set; }
        public string MailGroupName { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public bool IsSendStudent { get; set; }
        public bool IsSendTeacher { get; set; }
        public EMailGroup EMailGroup { get; set; }

        public bool UpdateEMailSendSettings()
        {
            return DAL.UpdateEMailSendSettings(Id, MailGroupId, Name, IsSendStudent, IsSendTeacher, IsActive);
        }
        public static List<EmailSender> GetList()
        {
            using (DataTable dt = DAL.GetListMailSendSettings())
            {
                List<EmailSender> list = new List<EmailSender>();
                foreach (DataRow row in dt.Rows)
                {
                    EmailSender item = new EmailSender()
                    {
                        Id = row.Field<int>("Id"),
                        MailGroupId = row.Field<int>("MailGroupId"),
                        MailGroupName = row.Field<string>("MailGroupName"),
                        Name = row.Field<string>("Name"),
                        IsActive = row.Field<bool>("IsActive"),
                        IsSendStudent = row.Field<bool>("IsSendStudent"),
                        IsSendTeacher = row.Field<bool>("IsSendTeacher")
                    };
                    list.Add(item);
                }
                return list;
            }
        }
        public static List<EmailSender> GetActiveEmailList()
        {
            using (DataTable dt = DAL.GetActiveEmailList())
            {
                List<EmailSender> list = new List<EmailSender>();
                foreach (DataRow row in dt.Rows)
                {
                    EmailSender item = new EmailSender()
                    {
                        Id = row.Field<int>("Id"),
                        Name = row.Field<string>("Name"),
                        IsSendStudent = row.Field<bool>("IsSendStudent"),
                        IsSendTeacher = row.Field<bool>("IsSendTeacher"),
                        EMailGroup = new EMailGroup()
                        {
                            BCCEmailList = row.Field<string>("BCCEmailList"),
                            CCEmailList = row.Field<string>("CCEmailList"),
                            ToEmailList = row.Field<string>("ToEmailList"),
                            ToOtherEmailList = row.Field<string>("ToOtherEmailList"),
                            Id = Convert.ToInt32(row["GroupId"])
                        },
                    };
                    list.Add(item);
                }
                return list;
            }
        }
    }

    public partial class DataAccessLayer
    {
        public bool UpdateEMailSendSettings(int pId, int pMailGroupId, string pName, bool pIsSendStudent, bool pIsSendTeacher, bool pIsActive)
        {
            return DatabaseContext.ExecuteNonQuery(CommandType.StoredProcedure, "_Admin_Update_Mail_Sender", MethodBase.GetCurrentMethod().GetParameters(), new object[] { pId, pMailGroupId, pName, pIsSendStudent, pIsSendTeacher, pIsActive });
        }
        public DataTable GetListMailSendSettings()
        {
            return DatabaseContext.ExecuteReader(CommandType.StoredProcedure, "_Admin_GetList_Mail_Sender", MethodBase.GetCurrentMethod().GetParameters(), new object[] { });
        }
        public DataTable GetActiveEmailList()
        {
            return DatabaseContext.ExecuteReader(CommandType.StoredProcedure, "_Getlist_MailSender", MethodBase.GetCurrentMethod().GetParameters(), new object[] { });
        }
    }
}