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
    public class PosBank : DataAccess
    {
        #region Properties

        public int Id { get; set; }
        public string BankName { get; set; }
        public string CardType { get; set; }
        public bool Active { get; set; }
        public bool MainPos { get; set; }
        public bool OneShotBank { get; set; }
        public bool OneShotBankCancel { get; set; }
        public int Priority { get; set; }
        public string XmlUrl { get; set; }
        public string PostUrl { get; set; }
        public int PosBankDetailId { get; set; }

        #endregion

        #region Methods

        public static List<PosBank> GetList()
        {
            using (DataTable dt = DAL.GetPosBankList())
            {
                List<PosBank> list = new List<PosBank>();
                foreach (DataRow row in dt.Rows)
                {
                    PosBank obj = new PosBank()
                    {
                        Id = row.Field<int>("Id"),
                        BankName = row.Field<string>("BankName"),
                        CardType = row.Field<string>("CardType"),
                        Active = row.Field<bool>("Active"),
                        MainPos = row.Field<bool>("MainPos"),
                        OneShotBank = row.Field<bool>("OneShotBank"),
                        OneShotBankCancel = row.Field<bool>("OneShotBankCancel"),
                        XmlUrl = row.Field<string>("XmlUrl"),
                        PostUrl = row.Field<string>("PostUrl"),
                        PosBankDetailId = Convert.ToInt32(row["PosBankDetailId"])
                    };
                    list.Add(obj);
                }
                return list;
            }
        }

        public bool Update()
        {
            return DAL.UpdatePosBank(Id, Active, MainPos, OneShotBank, OneShotBankCancel, XmlUrl, PostUrl, EditId);
        }

        #endregion
    }

    public partial class DataAccessLayer
    {
        public bool UpdatePosBank(int pId, bool pActive, bool pMainPos, bool pOneShotBank, bool pOneShotBankCancel, string pXmlUrl, string pPostUrl, int pEditId)
        {
            return DatabaseContext.ExecuteNonQuery(CommandType.StoredProcedure, "_Admin_Update_PosBank", MethodBase.GetCurrentMethod().GetParameters(), new object[] { pId, pActive, pMainPos, pOneShotBank, pOneShotBankCancel, pXmlUrl, pPostUrl, pEditId });
        }

        public DataTable GetPosBankList()
        {
            return DatabaseContext.ExecuteReader(CommandType.StoredProcedure, "_GetList_PosBank");
        }
    }
}