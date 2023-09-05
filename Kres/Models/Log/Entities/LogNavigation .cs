
using System;
using System.Data;
using System.Reflection;
using Kres.Models.DataLayer;

namespace Kres.Models.EntityLayer
{
    class LogNavigation : DataAccess
    {
        #region Constructors
        public LogNavigation()
        {
            ClientType = ClientType.B2BWeb;
            StudentId = -1;
            TeacherId = -1;
            UserId = -1; ;

        }
        #endregion

        #region Parametres
        public int Id { get; set; }
        public ClientType ClientType { get; set; }
        public int StudentId { get; set; }
        public int TeacherId { get; set; }
        public int UserId { get; set; }
        public string Navigation { get; set; }
        public string IpAddress { get; set; }
        #endregion

        #region Methods

        public bool Save()
        {
            return DAL.InsertLogNavigation(StudentId,UserId,TeacherId,Navigation,ClientType.ToString(),IpAddress);
        }

        #endregion

    }
    public partial class DataAccessLayer
    {
        public bool InsertLogNavigation(int pStudentId, int pUserId, int pTeacherId, string pNavigation, string pClientType, string pIpAddress)
        {
            return DatabaseContext.ExecuteNonQuery(CommandType.StoredProcedure, "_Insert_Log_Navigation", MethodBase.GetCurrentMethod().GetParameters(), new object[] { pStudentId, pUserId, pTeacherId, pNavigation, pClientType, pIpAddress });
        }

    }
}