using System;
using System.Data;
using System.Reflection;
using System.Web.Helpers;
using Kres.Models.DataLayer;

namespace Kres.Models.EntityLayer
{
    public class LogPayment : DataAccess
    {
        #region Constructors
        public LogPayment()
        {
            Client = ClientType.B2BWeb;
            StudentId = -1;
            TeacherId = -1;
        }
        #endregion

        #region Properties
        public int Id { get; set; }
        public ClientType Client { get; set; }
        public int StudentId { get; set; }
        public int TeacherId { get; set; }
        public DateTime ServerDate { get; set; }
        public LogPaymentType LogType { get; set; }
        public string Source { get; set; }
        public string Explanation { get; set; }
        public string CurrentPaymentId { get; set; }
        public string BankName { get; set; }
        #endregion

        #region Methods
        public bool Save()
        {
            return DAL.InsertLogPayment(Client.ToString(), StudentId, TeacherId, LogType.ToString(), Source, Explanation, CurrentPaymentId, BankName);
        }
        #endregion
    }
    public partial class DataAccessLayer
    {
        public bool InsertLogPayment(string pClient, int pStudentId, int pTeacherId, string pLogType, string pSource, string pExplanation, string pCurrentPaymentId, string pBankName)
        {
            return DatabaseContext.ExecuteNonQuery(CommandType.StoredProcedure, "Insert_LogPayment", MethodBase.GetCurrentMethod().GetParameters(), new object[] { pClient, pStudentId, pTeacherId, pLogType, pSource, pExplanation, pCurrentPaymentId, pBankName });
        }

    }
    public enum LogPaymentType
    {
        Error,
        Information,
        Request,
        Response,
        PointQuery

    }
}