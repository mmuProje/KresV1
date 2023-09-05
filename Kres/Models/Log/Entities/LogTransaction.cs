
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using Kres.Models.DataLayer;

namespace Kres.Models.EntityLayer
{
    class LogTransaction : DataAccess
    {
        #region Constructors
        public LogTransaction()
        {
            ClientType = ClientType.B2BWeb;
            StudentId = -1;
            TeacherId = -1;
            UserId = -1; ;
            LicenceId = -1;

        }
        #endregion

        #region Parametres
        public int Id { get; set; }
        public ClientType ClientType { get; set; }
        public int StudentId { get; set; }
        public int TeacherId { get; set; }
        public int UserId { get; set; }
        public LogTransactionSource Source { get; set; }
        public string Process { get; set; }
        public string Explanation { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string IpAddress { get; set; }
        public int LicenceId { get; set; }

        #endregion

        #region Methods

        public bool Save()
        {
            return DAL.InsertLogTransaction(ClientType.ToString(), StudentId, TeacherId, UserId, Source.ToString(), Process, Explanation, Latitude, Longitude, IpAddress, LicenceId);
        }

        public static List<LogTransaction> GetListStudentLogin(int StudentId, int userId)
        {
            List<LogTransaction> list = new List<LogTransaction>();
            DataTable dt = DAL.GetListLogStudentLogin(StudentId, userId);
            foreach (DataRow row in dt.Rows)
            {
                LogTransaction obj = new LogTransaction()
                {
                    ClientType = (ClientType)Enum.Parse(typeof(ClientType), row.Field<string>("ClientType")),
                    StudentId = row.Field<int>("StudentId"),
                    TeacherId = row.Field<int>("TeacherId"),
                    Source = (LogTransactionSource)Enum.Parse(typeof(LogTransactionSource), row.Field<string>("Source")),
                    Process = row.Field<string>("Process"),
                    Explanation = row.Field<string>("Explanation"),
                    CreateDate = row.Field<DateTime>("CreateDate"),
                    IpAddress = row.Field<string>("IpAddress")

                };
                list.Add(obj);
            }

            return list;
        }

        #endregion

    }
    public partial class DataAccessLayer
    {
        public bool InsertLogTransaction(string pClientType, int pStudentId, int pTeacherId, int pUserId, string pSource, string pProcess, string pExplanation, double pLatitude, double pLongitude, string pIpAddress, int pLicenceId)
        {
            return DatabaseContext.ExecuteNonQuery(CommandType.StoredProcedure, "_Insert_LogTransaction", MethodBase.GetCurrentMethod().GetParameters(), new object[] { pClientType, pStudentId, pTeacherId, pUserId, pSource, pProcess, pExplanation, pLatitude, pLongitude, pIpAddress, pLicenceId });
        }
        public DataTable GetListLogStudentLogin(int pStudentId, int pUserId)
        {
            return DatabaseContext.ExecuteReader(CommandType.StoredProcedure, "_Admin_Report_Student_Login", MethodBase.GetCurrentMethod().GetParameters(), new object[] { pStudentId, pUserId });
        }

    }
}