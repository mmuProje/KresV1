using Kres.Models.DataLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Kres.Models.EntityLayer
{
    [Serializable]
    public class PosInstallment : DataAccess
    {
        public PosInstallment()
        {
            DueDay = "0";
        }

        #region Properties
        public int Id { get; set; }
        public int PosBankId { get; set; }
        public int BankId { get; set; }
        public int Installment { get; set; }
        public int ExtraInstallment { get; set; }
        public int DeferalInstallment { get; set; }
        public double CommissionRate { get; set; }
        public string InstallmentText { get; set; }
        public string InstallmentValue { get; set; }
        public string Type { get; set; }
        public string Note { get; set; }
        public string AutoNote { get; set; }
        public double TotalPrice { get; set; }
        public double MonthPrice { get; set; }
        public string CampaignCode { get; set; }
        public double MainInstallmentAmount { get; set; }
        public double ExtraInstallmentAmount { get; set; }
        public bool Checked { get; set; }
        public string AliasName { get; set; }
        public string DueDay { get; set; }
        public List<PosInstallment> PosInstallments { get; set; }

        #endregion

        #region Methods

        public static List<PosInstallment> GetInstallmentList(int pBankId, double pTotal, int pStudentId, int pSpecialInstallment, int pPosBankId)
        {
            using (DataTable dt = DAL.GetInstallmentList(pBankId, pTotal, pStudentId, pSpecialInstallment, pPosBankId))
            {
                List<PosInstallment> list = new List<PosInstallment>();
                foreach (DataRow row in dt.Rows)
                {
                    PosInstallment item = new PosInstallment()
                    {
                        Id = row.Field<int>("Id"),
                        BankId = row.Field<int>("BankId"),
                        Installment = row.Field<int>("Installment"),
                        ExtraInstallment = row.Field<int>("ExtraInstallment"),
                        DeferalInstallment = row.Field<int>("DeferralInstallment"),
                        CommissionRate = row.Field<double>("CommissionRate"),
                        InstallmentValue = row.Field<int>("Installment") + "-" + row.Field<double>("CommissionRate"),
                        Note = row.Field<string>("Note"),
                        AutoNote = row.Field<string>("AutoNote"),

                    };
                    list.Add(CalculateTotal(item, pTotal));
                }
                return list;
            }
        }

        public static List<PosInstallment> GetInstallmentByPosBankId(int pPosBankId)
        {
            using (DataTable dt = DAL.GetInstallmentByPosBankId(pPosBankId))
            {
                List<PosInstallment> list = new List<PosInstallment>();
                foreach (DataRow row in dt.Rows)
                {
                    PosInstallment item = new PosInstallment()
                    {
                        Id = row.Field<int>("Id"),
                        PosBankId = row.Field<int>("PosBankId"),
                        BankId = row.Field<int>("BankId"),
                        Installment = row.Field<int>("Installment"),
                        ExtraInstallment = row.Field<int>("ExtraInstallment"),
                        DeferalInstallment = row.Field<int>("DeferralInstallment"),
                        CommissionRate = row.Field<double>("CommissionRate"),
                        Note = row.Field<string>("Note"),
                        MainInstallmentAmount = row.Field<double>("MainInstallmentAmount"),
                        ExtraInstallmentAmount = row.Field<double>("ExtraInstallmentAmount"),
                        DueDay = row.Field<string>("DueDay"),
                        Checked = true

                    };
                    list.Add(item);
                }

                for (int i = 1; i <= 12; i++)
                {
                    if (list.Where(x => x.Installment == i).Count() == 0)
                        list.Add(new PosInstallment { Installment = i });
                }

                return list.OrderBy(x => x.Installment).ToList();
            }
        }

        public static PosInstallment CalculateTotal(PosInstallment item, double pTotal)
        {
            if (item.CommissionRate != 0)
            {
                item.TotalPrice = pTotal + (pTotal * (item.CommissionRate / 100));
                item.MonthPrice = item.TotalPrice / (item.Installment + item.ExtraInstallment);
            }
            else
            {
                item.TotalPrice = pTotal;
                item.MonthPrice = item.TotalPrice / (item.Installment + item.ExtraInstallment);

            }

            return item;
        }

        public bool Add()
        {
            return DAL.InsertPosInstallment(PosBankId, BankId, Installment, ExtraInstallment, DeferalInstallment, CommissionRate, Note, MainInstallmentAmount, ExtraInstallmentAmount, CreateId, DueDay);
        }
        public static List<PosInstallment> GetListByCardType()
        {
            using (DataTable dt = DAL.GetListBankCardType())
            {
                List<PosInstallment> list = new List<PosInstallment>();
                foreach (DataRow row in dt.Rows)
                {
                    PosInstallment item = new PosInstallment()
                    {
                        BankId = row.Field<int>("BankId"),
                        Type = row.Field<string>("Type"),
                        PosBankId = row.Field<int>("PosBankId"),
                        AliasName = row.Field<string>("AliasName")
                    };
                    list.Add(item);
                }
                return list;
            }
        }
        public static List<PosInstallment> GetListInstalmentByCardType()
        {
            using (DataTable dt = DAL.GetListInstalmentByCardType())
            {
                List<PosInstallment> list = new List<PosInstallment>();
                foreach (DataRow row in dt.Rows)
                {
                    PosInstallment item = new PosInstallment()
                    {
                        PosBankId = row.Field<int>("PosBankId"),
                        BankId = row.Field<int>("BankId"),
                        Installment = row.Field<int>("Installment"),
                        ExtraInstallment = row.Field<int>("ExtraInstallment"),
                        DeferalInstallment = row.Field<int>("DeferralInstallment"),
                        CommissionRate = row.Field<double>("CommissionRate"),
                        Type = row.Field<string>("Type"),
                        Note = (row.Field<string>("Note") == "" || row.Field<string>("Note") == null) ? "" : row.Field<string>("Note"),
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

        public DataTable GetListBankCardType()
        {
            return DatabaseContext.ExecuteReader(CommandType.StoredProcedure, "_GetList_BankCardType", MethodBase.GetCurrentMethod().GetParameters(), new object[] { });
        }
        public DataTable GetListInstalmentByCardType()
        {
            return DatabaseContext.ExecuteReader(CommandType.StoredProcedure, "_GetList_InstallmentCount_ByCardType_1", MethodBase.GetCurrentMethod().GetParameters(), new object[] { });
        }


        public DataTable GetInstallmentList(int pBankId, double pTotal, int pStudentId, int pSpecialInstallment, int pPosBankId)
        {
            return DatabaseContext.ExecuteReader(CommandType.StoredProcedure, "_Pos_Payment_GetList_InstallmentByBankId", MethodBase.GetCurrentMethod().GetParameters(), new object[] { pBankId, pTotal, pStudentId, pSpecialInstallment, pPosBankId });
        }


        public DataTable GetInstallmentByPosBankId(int pPosBankId)
        {
            return DatabaseContext.ExecuteReader(CommandType.StoredProcedure, "_Pos_GetList_InstallmentByPosBankId", MethodBase.GetCurrentMethod().GetParameters(), new object[] { pPosBankId });
        }


        public bool InsertPosInstallment(int pPosBankId, int pBankId, int pInstallment, int pExtraInstallment, int pDeferalInstallment, double pCommissionRate, string pNote, double pMainInstallmentAmount, double pExtraInstallmentAmount, int pCreateId, string pDueDay)
        {
            return DatabaseContext.ExecuteNonQuery(CommandType.StoredProcedure, "_Admin_Insert_PosInstallment", MethodBase.GetCurrentMethod().GetParameters(), new object[] { pPosBankId, pBankId, pInstallment, pExtraInstallment, pDeferalInstallment, pCommissionRate, pNote, pMainInstallmentAmount, pExtraInstallmentAmount, pCreateId, pDueDay });
        }



    }
}