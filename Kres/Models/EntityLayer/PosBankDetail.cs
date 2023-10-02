using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using Kres.Models.DataLayer;
using Kres.Models.Helper;

namespace Kres.Models.EntityLayer
{
    public class PosBankDetail : DataAccess
    {

        public PosBankDetail()
        {
            StoreNumber = string.Empty;
        }

        #region Properties

        public int Id { get; set; }
        public int PosBankId { get; set; }
        public PosBanks PosBanks { get; set; }
        public PosBank PosBank { get; set; }
        public string PosBankName { get; set; }
        public int Type { get; set; }
        public DateTime RecordDate { get; set; }
        public bool UseBonus { get; set; }
        public string StoreNumber { get; set; }
        public string TerminalNo { get; set; }
        public string ApiUser { get; set; }
        public string Password { get; set; }
        public string KeyWord { get; set; }
        public string _3dSecureKey { get; set; }
        public string ProvautPassword { get; set; }
        public _3DSecureType _3dSecureSelection { get; set; }
        public int _3dSecureSelectionInt { get; set; }
        public string PosnetId { get; set; }
        public bool NewPos { get; set; }
        public double MinimumInstallmentPrice { get; set; }
        public int AuthorizationType { get; set; }
        public string MenuStr { get; set; }
        #endregion

        #region Methods

        public static PosBankDetail GetPosPaymentChoicePayBank(int pOneShot, int pBankId)
        {
            using (DataTable dt = DAL.GetPosPaymentChoicePayBank(pOneShot, pBankId))
            {
                PosBankDetail obj = new PosBankDetail();
                foreach (DataRow row in dt.Rows)
                {
                    obj = new PosBankDetail()
                    {
                        Id = row.Field<int>("Id"),
                        PosBankName = row.Field<string>("PosBankName"),
                        PosBankId = row.Field<int>("PosBankId"),
                        PosBanks = row.Field<PosBanks>("PosBankId"),
                        Type = row.Field<int>("Type"),
                        UseBonus = row.Field<bool>("UseBonus"),
                        StoreNumber = row.Field<string>("StoreNumber"),
                        TerminalNo = row.Field<string>("TerminalNo"),
                        ApiUser = row.Field<string>("ApiUser"),
                        Password = row.Field<string>("Password"),
                        KeyWord = row.Field<string>("KeyWord"),
                        _3dSecureKey = row.Field<string>("3dSecureKey"),
                        ProvautPassword = row.Field<string>("ProvautPassword"),
                        _3dSecureSelection = (_3DSecureType)row.Field<int>("3dSecureSelection"),
                        PosnetId = row.Field<string>("PosnetId"),
                        NewPos = row.Field<bool>("NewPos"),
                        MinimumInstallmentPrice = row.Field<double>("MinimumInstallmentPrice"),
                        AuthorizationType = row.Field<int>("AuthorizationType"),

                        PosBank = new PosBank()
                        {
                            CardType = row.Field<string>("CardType"),
                            Active = row.Field<bool>("Active"),
                            MainPos = row.Field<bool>("MainPos"),
                            OneShotBank = row.Field<bool>("OneShotBank"),
                            OneShotBankCancel = row.Field<bool>("OneShotBankCancel"),
                            Priority = row.Field<int>("Priority"),
                            XmlUrl = row.Field<string>("XmlUrl"),
                            PostUrl = row.Field<string>("PostUrl")
                        }
                    };
                }
                return obj;
            }
        }


        public static PosBankDetail GetPosBankdEtailByPosBankId(int pPosBankId)
        {
            using (DataTable dt = DAL.GetPosBankdEtailByPosBankId(pPosBankId))
            {
                PosBankDetail obj = new PosBankDetail();
                foreach (DataRow row in dt.Rows)
                {
                    obj = new PosBankDetail()
                    {
                        Id = Convert.ToInt32(row["Id"]),
                        PosBankName = row.Field<string>("PosBankName"),
                        PosBankId = Convert.ToInt32(row["PosBankId"]),
                        PosBanks = (PosBanks)row.Field<int>("PosBankId"),
                        Type = Convert.ToInt32(row["Type"]),
                        UseBonus = Convert.ToBoolean(row["UseBonus"]),
                        StoreNumber = row.Field<string>("StoreNumber"),
                        TerminalNo = row.Field<string>("TerminalNo"),
                        ApiUser = row.Field<string>("ApiUser"),
                        Password = row.Field<string>("Password"),
                        KeyWord = row.Field<string>("KeyWord"),
                        _3dSecureKey = row.Field<string>("3dSecureKey"),
                        ProvautPassword = row.Field<string>("ProvautPassword"),
                        _3dSecureSelection = (_3DSecureType)Convert.ToInt32(row["3dSecureSelection"]),
                        _3dSecureSelectionInt = Convert.ToInt32(row["3dSecureSelection"]),
                        PosnetId = row.Field<string>("PosnetId"),
                        MinimumInstallmentPrice = Convert.ToDouble(row["MinimumInstallmentPrice"]),
                        AuthorizationType = Convert.ToInt32(row["AuthorizationType"]),
                        MenuStr = row.Field<string>("MenuStr"),
                        PosBank = new PosBank()
                        {

                            CardType = row.Field<string>("CardType"),
                            Active = row.Field<bool>("Active"),
                            MainPos = row.Field<bool>("MainPos"),
                            OneShotBank = row.Field<bool>("OneShotBank"),
                            OneShotBankCancel = row.Field<bool>("OneShotBankCancel"),
                            Priority = row.Field<int>("Priority"),
                            XmlUrl = row.Field<string>("XmlUrl"),
                            PostUrl = row.Field<string>("PostUrl")
                        }
                    };
                }
                return obj;
            }
        }

        public bool Update()
        {
            return DAL.UpdatePosBankDetail(Id, PosBankName, UseBonus, StoreNumber, TerminalNo, KeyWord, ApiUser, Password, _3dSecureKey, ProvautPassword, _3dSecureSelectionInt, PosnetId, MinimumInstallmentPrice, EditId);
        }

        public int Add()
        {
            DataTable dt = DAL.InsertPosBankDetail(PosBankId, PosBankName, UseBonus, StoreNumber, TerminalNo, KeyWord, ApiUser, Password, _3dSecureKey, ProvautPassword, _3dSecureSelectionInt, PosnetId, MinimumInstallmentPrice, CreateId);

            return Convert.ToInt32(dt.Rows[0][0]);

        }

        #endregion

    }

    public partial class DataAccessLayer
    {
        public bool UpdatePosBankDetail(int pId, string pPosBankName, bool pUseBonus, string pStoreNumber, string pTerminalNo, string pKeyWord, string pApiUser, string pPassword, string p3dSecureKey, string pProvautPassword, int p3dSecureSelection, string pPosnetId, double pMinimumInstallmentPrice, int pEditId)
        {
            return DatabaseContext.ExecuteNonQuery(CommandType.StoredProcedure, "_Admin_Update_PosBankDetail", MethodBase.GetCurrentMethod().GetParameters(), new object[] { pId, pPosBankName, pUseBonus, pStoreNumber, pTerminalNo, pKeyWord, pApiUser, pPassword, p3dSecureKey, pProvautPassword, p3dSecureSelection, pPosnetId, pMinimumInstallmentPrice, pEditId });
        }

        public DataTable InsertPosBankDetail(int pPosBankId, string pPosBankName, bool pUseBonus, string pStoreNumber, string pTerminalNo, string pKeyWord, string pApiUser, string pPassword, string p3dSecureKey, string pProvautPassword, int p3dSecureSelection, string pPosnetId, double pMinimumInstallmentPrice, int pCreateId)
        {
            return DatabaseContext.ExecuteReader(CommandType.StoredProcedure, "_Admin_Insert_PosBankDetail", MethodBase.GetCurrentMethod().GetParameters(), new object[] { pPosBankId, pPosBankName, pUseBonus, pStoreNumber, pTerminalNo, pKeyWord, pApiUser, pPassword, p3dSecureKey, pProvautPassword, p3dSecureSelection, pPosnetId, pMinimumInstallmentPrice, pCreateId });
        }

        public DataTable GetPosPaymentChoicePayBank(int pOneShot, int pBankId)
        {
            return DatabaseContext.ExecuteReader(CommandType.StoredProcedure, "_Pos_Payment_ChoicePayBank", MethodBase.GetCurrentMethod().GetParameters(), new object[] { pOneShot, pBankId });
        }

        public DataTable GetPosBankdEtailByPosBankId(int pPosBankId)
        {
            return DatabaseContext.ExecuteReader(CommandType.StoredProcedure, "_Pos_GetList_PosBankDetailByPosBankId", MethodBase.GetCurrentMethod().GetParameters(), new object[] { pPosBankId });
        }

    }
}