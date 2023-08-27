using Kres.Models.DataLayer;
using Kres.Models.EntityLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Kres.Models.EntityLayer
{
    public class RefundPostParameters : DataAccess
    {
        public string ApiName { get; set; }
        public string Password { get; set; }
        public string ClientId { get; set; }
        public string OrderId { get; set; }
        public double Amount { get; set; }
        public int VirtualPosId { get; set; }
        public string TerminalId { get; set; }
        public string PasswordOfProvrfn { get; set; }
        public string InstallmentCount { get; set; }
        public string HostLogKey { get; set; }
        public string OriginalRetrefNumber { get; set; }
        public string AuthCode { get; set; }
        public string PostAddress { get; set; }


        public static RefundPostParameters GetRefundItemById(int pId)
        {
            DataTable dt = DAL.GetRefundItemById(pId);

            if (dt.Rows.Count > 0)
            {
                RefundPostParameters refundPostParameters = new RefundPostParameters()
                {
                    ApiName = dt.Rows[0].Field<string>("ApiName"),
                    Password = dt.Rows[0].Field<string>("Password"),
                    TerminalId = dt.Rows[0].Field<string>("TerminalId"),
                    ClientId = dt.Rows[0].Field<string>("ClientId"),
                    OrderId = dt.Rows[0].Field<string>("OrderId"),
                    Amount = dt.Rows[0].Field<double>("Amount"),
                    AuthCode = dt.Rows[0].Field<string>("AuthCode"),
                    PostAddress = dt.Rows[0].Field<string>("PostAddress"),
                    VirtualPosId = dt.Rows[0].Field<int>("VirtualPosId"),
                    HostLogKey = dt.Rows[0].Field<string>("HostLogKey"),
                    InstallmentCount = dt.Rows[0].Field<string>("InstallmentCount"),
                    OriginalRetrefNumber = dt.Rows[0].Field<string>("OriginalRetrefNumber"),
                    PasswordOfProvrfn = dt.Rows[0].Field<string>("PasswordOfProvrfn")
                };

                return refundPostParameters;
            }
            else
                return new RefundPostParameters();
        }

    }
    public partial class DataAccessLayer
    {
        public DataTable GetRefundItemById(int pId)
        {
            return DatabaseContext.ExecuteReader(CommandType.StoredProcedure, "_Admin_Get_RefundValues", MethodBase.GetCurrentMethod().GetParameters(), new object[] { pId });
        }
    }
}
