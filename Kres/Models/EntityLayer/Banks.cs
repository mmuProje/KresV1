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
    public class Banks : DataAccess
    {

        #region Properties

        public int Id { get; set; }
        public string BankName { get; set; }
        public string Type { get; set; }


        #endregion


        public static Banks GetListByEpaymentById()
        {
            using (DataTable dt = DAL.GetListByEpaymentById())
            {
                Banks epayment = new Banks();
                foreach (DataRow row in dt.Rows)
                {
                    epayment = new Banks()
                    {
                        Id = row.Field<int>("Id")
                      
                    };
                }
                return epayment;
            }
        }



    }
    public partial class DataAccessLayer
    {
        public DataTable GetListByEpaymentById()
        {
            return DatabaseContext.ExecuteReader(CommandType.StoredProcedure, "get_banks", MethodBase.GetCurrentMethod().GetParameters(), new object[] { });
        }

    }
}