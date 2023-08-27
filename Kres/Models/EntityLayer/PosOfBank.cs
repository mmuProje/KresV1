using Kres.Models.DataLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Kres.Models.EntityLayer
{
    public class PosOfBank : DataAccess
    {
        #region Properties

        public int Id { get; set; }
        public int PosBankId { get; set; }
        public PosBank PosBank { get; set; }
        public int BankId { get; set; }
        public Banks Banks { get; set; }
        public bool InstallmentUse { get; set; }

        #endregion

        #region Methods


        public bool Update()
        {
            return DAL.UpdatePosOfBank(Id, PosBankId, BankId, InstallmentUse, EditId);
        }
        public static List<PosOfBank> GetPosOfBankList(int pPosBankId)
        {
            using (DataTable dt = DAL.GetPosOfBankList(pPosBankId))
            {
                List<PosOfBank> list = new List<PosOfBank>();
                foreach (DataRow row in dt.Rows)
                {
                    PosOfBank item = new PosOfBank()
                    {
                        Id = row.Field<int>("Id"),
                        PosBankId = row.Field<int>("PosBankId"),
                        BankId = row.Field<int>("BankId"),
                        InstallmentUse = row.Field<bool>("InstallmentUse"),
                        Banks = new Banks()
                        {
                            Id = row.Field<int>("BankId"),
                            BankName = row.Field<string>("BankName"),
                            Type = row.Field<string>("Type")
                        }
                    };
                    list.Add(item);
                }
                return list;
            }
        }
        #endregion
    }

    public partial class DataAccessLayer
    {
        public bool UpdatePosOfBank(int pId, int pPosBankId, int pBankId,bool pInstallmentUse, int pEditId)
        {
            return DatabaseContext.ExecuteNonQuery(CommandType.StoredProcedure, "_Admin_Update_PosOfBank", MethodBase.GetCurrentMethod().GetParameters(), new object[] { pId, pPosBankId, pBankId, pInstallmentUse, pEditId });
        }
        public DataTable GetPosOfBankList(int pPosBankId)
        {
            return DatabaseContext.ExecuteReader(CommandType.StoredProcedure, "_Pos_GetList_PosOfBank", MethodBase.GetCurrentMethod().GetParameters(), new object[] { pPosBankId });
        }
    }
}