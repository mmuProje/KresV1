using Kres.Models.DataLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Kres.Models.EntityLayer
{
    public class DocumentDraftcs : DataAccess
    {
        #region Properties

        public int Id { get; set; }
        public string Header { get; set; }
        public int Type { get; set; }
        public string EmailBody { get; set; }
        public string HeaderBody { get; set; }
        public string ContentBody { get; set; }
        public string FooterBody { get; set; }
        public float HeaderHeight { get; set; }
        public float FooterHeight { get; set; }
        public string EmailAddress { get; set; }
        public bool IsSendEmail { get; set; }
        public bool IsSendTeacher { get; set; }
        public bool IsSendStudent { get; set; }
        public bool IsPageNumber { get; set; }
        public bool IsDisplayHeader { get; set; }
        public bool IsDisplayFooter { get; set; }
        public bool DefaultData { get; set; }

        #endregion

        #region Methods

        public static DocumentDraftcs GetDocumentDraft(int pType, bool pDefault)
        {
            using (DataTable dt = DAL.GetDocumentDraft(pType, pDefault))
            {
                DocumentDraftcs list = new DocumentDraftcs();
                foreach (DataRow row in dt.Rows)
                {
                    list = new DocumentDraftcs
                    {
                        Id = row.Field<int>("Id"),
                        Type = row.Field<int>("Type"),
                        Header = row.Field<string>("Header"),
                        EmailBody = row.Field<string>("EmailBody"),
                        HeaderBody = row.Field<string>("HeaderBody"),
                        ContentBody = row.Field<string>("ContentBody"),
                        FooterBody = row.Field<string>("FooterBody"),
                        HeaderHeight = float.Parse(row.Field<decimal>("HeaderHeight").ToString()),
                        FooterHeight = float.Parse(row.Field<decimal>("FooterHeight").ToString()),
                        EmailAddress = row.Field<string>("EmailAddress"),
                        IsSendEmail = row.Field<bool>("IsSendEmail"),
                        IsSendTeacher = row.Field<bool>("IsSendTeacher"),
                        IsSendStudent = row.Field<bool>("IsSendStudent"),
                        IsPageNumber = row.Field<bool>("IsPageNumber"),
                        IsDisplayHeader = row.Field<bool>("IsDisplayHeader"),
                        IsDisplayFooter = row.Field<bool>("IsDisplayFooter"),
                        DefaultData = row.Field<bool>("DefaultData"),
                    };
                }
                return list;
            }
        }

        public bool Add()
        {
            return DAL.InsertDocumentDraft(Header, Type, EmailBody, HeaderBody, ContentBody, FooterBody, HeaderHeight, FooterHeight, EmailAddress, IsSendEmail, IsSendTeacher, IsSendStudent, IsPageNumber, IsDisplayHeader, IsDisplayFooter, DefaultData, CreateId);
        }

        public bool Update()
        {
            return DAL.UpdateDocumentDraft(Id, Header, Type, EmailBody, HeaderBody, ContentBody, FooterBody, HeaderHeight, FooterHeight, EmailAddress, IsSendEmail, IsSendTeacher, IsSendStudent, IsPageNumber, IsDisplayHeader, IsDisplayFooter, DefaultData, EditId);
        }

        #endregion

    }

    public partial class DataAccessLayer
    {
        public DataTable GetDocumentDraft(int pType, bool pDefault)
        {
            return DatabaseContext.ExecuteReader(CommandType.StoredProcedure, "_GetItem_DocumentDraftByType", MethodBase.GetCurrentMethod().GetParameters(), new object[] { pType, pDefault });
        }

        public bool UpdateDocumentDraft(int pId, string pHeader, int pType, string pEmailBody, string pHeaderBody, string pContentBody, string pFooterBody, float pHeaderHeight, float pFooterHeight, string pEmailAddress, bool pIsSendEmail, bool pIsSendTeacher, bool pIsSendStudent, bool pIsPageNumber, bool pIsDisplayHeader, bool pIsDisplayFooter, bool pDefaultData, int pEditId)
        {
            return DatabaseContext.ExecuteNonQuery(CommandType.StoredProcedure, "_Admin_Update_DocumentDraft", MethodBase.GetCurrentMethod().GetParameters(), new object[] { pId, pHeader, pType, pEmailBody, pHeaderBody, pContentBody, pFooterBody, pHeaderHeight, pFooterHeight, pEmailAddress, pIsSendEmail, pIsSendTeacher, pIsSendStudent, pIsPageNumber, pIsDisplayHeader, pIsDisplayFooter, pDefaultData, pEditId });
        }

        public bool InsertDocumentDraft(string pHeader, int pType, string pEmailBody, string pHeaderBody, string pContentBody, string pFooterBody, float pHeaderHeight, float pFooterHeight, string pEmailAddress, bool pIsSendEmail, bool pIsSendTeacher, bool pIsSendStudent, bool pIsPageNumber, bool pIsDisplayHeader, bool pIsDisplayFooter, bool pDefaultData, int pCreateId)
        {
            return DatabaseContext.ExecuteNonQuery(CommandType.StoredProcedure, "_Admin_Insert_DocumentDraft", MethodBase.GetCurrentMethod().GetParameters(), new object[] { pHeader, pType, pEmailBody, pHeaderBody, pContentBody, pFooterBody, pHeaderHeight, pFooterHeight, pEmailAddress, pIsSendEmail, pIsSendTeacher, pIsSendStudent, pIsPageNumber, pIsDisplayHeader, pIsDisplayFooter, pDefaultData, pCreateId });
        }
    }
}