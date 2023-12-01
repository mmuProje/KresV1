using Kres.Models.DataLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;


namespace Kres.Models.EntityLayer
{
    public class EPayment : DataAccess
    {
        #region Properties
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string CardNumber { get; set; }
        public string CardCvv { get; set; }
        public string ExpMonth { get; set; }
        public string ExpYear { get; set; }
        public string CardType { get; set; }
        public string Amount { get; set; }
        public string Installment { get; set; }
        public DateTime? ProcessingDate { get; set; }
        public int StudentId { get; set; }
        public string StudentCode { get; set; }
        public string StudentName { get; set; }
        public string NameSurname { get; set; }
        public string BankName { get; set; }
        public string ErrMsg { get; set; }
        public string IpAddress { get; set; }
        public string ProcReturnCode { get; set; }
        public string TransId { get; set; }
        public string Oid { get; set; }
        public string GroupId { get; set; }
        public string Appr { get; set; }
        public string Extra { get; set; }
        public string Result { get; set; }
        public string AuthCode { get; set; }
        public double Total { get; set; }
        public double TotalStr { get; set; }
        public string Discount { get; set; }
        public string BankId { get; set; }
        public string UseEPaymentType { get; set; }
        public string PaymentId { get; set; }
        public string HashParams { get; set; }
        public string HashParamsVal { get; set; }
        public string Hash { get; set; }
        public string HostRefNum { get; set; }
        public string ResponseFromServer { get; set; }
        public string OrderId { get; set; }
        public string Sipbil { get; set; }
        public string Sesbil { get; set; }
        public string UyeRef { get; set; }
        public string VbRef { get; set; }
        public string CampaignUrl { get; set; }
        public Student Student { get; set; }
        public DateTime RecordDate { get; set; }
        public string ExpendableBonus { get; set; }
        public int Count { get; set; }
        public string Note { get; set; }
        public string PhoneNumber { get; set; }
        public bool _3DSecure { get; set; }
        public bool UseBonus { get; set; }
        public string StudentTotalBonus { get; set; }
        public int EpaymentStatus { get; set; }
        #endregion

        #region Methods
        public static DataTable Insert(string pCardNumber, string pNameSurname, string pExpMonth, string pExpYear, string pCvc, string pAmount, string pInstallment, int pStudentId, string pStudentCode, string pStudentName, string pBankName, double pTotal, string pRate, int pBankId, string pPaymentId, int pSystemType, string pPhoneNumber, string pNote, string pCardType, int pTeacherId, string pExtraInstallment, string pStudentTotalBonus, string pExpendableBonus, bool pUseBonus = false, bool p3DSecure = true, int pGroupId = -1, int pPosBankId = -1)
        {
            return DAL.InsertPayment(pCardNumber, pNameSurname, pExpMonth, pExpYear, pCvc, pAmount, pInstallment, pStudentId, pStudentCode, pStudentName, pBankName, pTotal, pRate, pBankId, pPaymentId, pSystemType, pPhoneNumber, pNote, pCardType, pTeacherId, pExtraInstallment, pStudentTotalBonus, pExpendableBonus, pUseBonus, p3DSecure, pGroupId, pPosBankId);
        }
        public static void Update(int pId, string pAuthCode, string pProcReturnCode, string pErrMsg, string pIpAddress, string pOrderId, string pOid, string pGroupId, string pTransId, string pExtra, string pUseEPaymentType, DateTime pProcessingDate, int pUseEpaymentId, string pCampaignUrl = "")
        {
            DAL.UpdatePayment(pId, pAuthCode, pProcReturnCode, pErrMsg, pIpAddress, pOrderId, pOid, pGroupId, pTransId, pExtra, pUseEPaymentType, pProcessingDate, pUseEpaymentId, pCampaignUrl);
        }

        public static EPayment GetListByEpaymentById(int pId)
        {
            using (DataTable dt = DAL.GetListByEpaymentById(pId))
            {
                EPayment epayment = new EPayment();
                foreach (DataRow row in dt.Rows)
                {
                    epayment = new EPayment()
                    {
                        Id = row.Field<int>("Id"),
                        CardNumber = row.Field<string>("CardNumber"),
                        NameSurname = row.Field<string>("NameSurname"),
                        BankName = row.Field<string>("BankName"),
                        RecordDate = row.Field<DateTime>("RecordDate"),
                        Total = Convert.ToDouble(row["Total"]),
                        Amount = row.Field<string>("Amount"),
                        Installment = row.Field<string>("Installment"),
                        Result = row.Field<string>("Result"),
                        AuthCode = row.Field<string>("AuthCode"),
                        UseEPaymentType = row.Field<string>("UseEPaymentType"),
                        PaymentId = row.Field<string>("PaymentId"),
                        PhoneNumber= row.Field<string>("Tel1"),
                        ProcReturnCode = row.Field<string>("ProcReturnCode"),
                        ExpendableBonus = row.Field<string>("ExpendableBonus"),
                        Student = new Student()
                        {
                            Code = row.Field<string>("Code"),
                            Name = row.Field<string>("Name"),
                            Tel1 = row.Field<string>("Tel1"),
                            Mail = row.Field<string>("Mail")
                        }
                    };
                }
                return epayment;
            }
        }
        public static List<EPayment> GetListByStudentId(int pStudentId)
        {
            using (DataTable dt = DAL.GetEPaymentListByStudentId(pStudentId))
            {
                List<EPayment> epaymentList = new List<EPayment>();
                foreach (DataRow row in dt.Rows)
                {
                    EPayment ePayments = new EPayment()
                    {
                        Id = row.Field<int>("Id"),
                        CardNumber = row.Field<string>("CardNumber"),
                        NameSurname = row.Field<string>("NameSurname"),
                        BankName = row.Field<string>("BankName"),
                        ProcessingDate = row.Field<DateTime?>("ProcessingDate"),
                        Total = Convert.ToDouble(row["Total"]),
                        Amount = row.Field<string>("Amount"),
                        Installment = row.Field<string>("Installment"),
                        Result = row.Field<string>("Result"),
                        AuthCode = row.Field<string>("AuthCode"),
                        UseEPaymentType = row.Field<string>("UseEPaymentType"),
                        PaymentId = row.Field<string>("PaymentId"),
                        ProcReturnCode = row.Field<string>("ProcReturnCode"),
                        ExpendableBonus = row.Field<string>("ExpendableBonus"),
                        Student = new Student()
                        {
                            Code = row.Field<string>("Code"),
                            Name = row.Field<string>("Name"),
                            Tel1 = row.Field<string>("Tel1")
                        }
                    };
                    epaymentList.Add(ePayments);
                }
                return epaymentList;
            }
        }
        public static List<EPayment> GetListEpayment(DateTime startDate, DateTime endDate, string t9Text, int paymentStatu)
        {
            using (DataTable dt = DAL.GetListEpayment(startDate, endDate, t9Text, paymentStatu))
            {
                List<EPayment> epaymentList = new List<EPayment>();
                foreach (DataRow row in dt.Rows)
                {
                    EPayment ePayments = new EPayment()
                    {
                        Id = row.Field<int>("Id"),
                        CardNumber = row.Field<string>("CardNumber"),
                        NameSurname = row.Field<string>("NameSurname"),
                        BankName = row.Field<string>("BankName"),
                        ProcessingDate = row.Field<DateTime?>("ProcessingDate"),
                        ProcReturnCode = row.Field<string>("ProcReturnCode"),
                        Amount = row.Field<string>("Amount"),
                        Total = Convert.ToDouble(row["Total"]),
                        Installment = row.Field<string>("Installment"),
                        Result = row.Field<string>("Result"),
                        AuthCode = row.Field<string>("AuthCode"),
                        ErrMsg = row.Field<string>("ErrMsg"),
                        IpAddress = row.Field<string>("IpAddress"),
                        TransId = row.Field<string>("TransId"),
                        //Oid = row.Field<string>("Oid"),
                        Appr = row.Field<string>("Appr"),
                        Extra = row.Field<string>("Extra"),
                        //OrderId = row.Field<string>("OrderId"),
                        PaymentId = row.Field<string>("PaymentId"),
                        UseEPaymentType = row.Field<string>("UseEPaymentType"),
                        Note = row.Field<string>("Note"),
                        PhoneNumber = row.Field<string>("PhoneNumber"),
                        _3DSecure = row.Field<bool>("_3DSecure"),
                        UseBonus = row.Field<bool>("UseBonus"),
                        //StudentTotalBonus = row.Field<string>("StudentTotalBonus"),
                        ExpendableBonus = row.Field<string>("ExpendableBonus"),
                        EpaymentStatus = row.Field<int>("EpaymentStatus"),
                        Student = new Student()
                        {
                            Code = row.Field<string>("Code"),
                            Name = row.Field<string>("Name"),
                            Tel1 = row.Field<string>("Tel1")
                        }
                    };
                    epaymentList.Add(ePayments);
                }
                return epaymentList;
            }
        }
        public static bool UpdatePaymentStatus(int pId, int pStatus)
        {
            return DAL.UpdatePaymentStatus(pId, pStatus);
        }
        public static EPayment GetItemPayment(int pInsertId)
        {
            using (DataTable dt = DAL.GetItemEPaymentByInsertId(pInsertId))
            {
                EPayment epayment = new EPayment();
                foreach (DataRow row in dt.Rows)
                {
                    epayment = new EPayment
                    {
                        Id = row.Field<int>("Id"),
                        CardNumber = row.Field<string>("CardNumber"),
                        NameSurname = row.Field<string>("NameSurname"),
                        BankName = row.Field<string>("BankName"),
                        ProcessingDate = row.Field<DateTime?>("ProcessingDate"),
                        Amount = row.Field<string>("Amount"),
                        Installment = row.Field<string>("Installment"),
                        Result = row.Field<string>("Result"),
                        AuthCode = row.Field<string>("AuthCode"),
                        UseEPaymentType = row.Field<string>("UseEPaymentType"),
                        PaymentId = row.Field<string>("PaymentId"),
                        ProcReturnCode = row.Field<string>("ProcReturnCode"),
                        ExpendableBonus = row.Field<string>("ExpendableBonus"),
                        PhoneNumber = row.Field<string>("PhoneNUmber"),
                        CampaignUrl = row.Field<string>("CampaignUrl"),
                        Student = new Student
                        {
                            Code = row.Field<string>("Code"),
                            Name = row.Field<string>("Name"),
                        }

                    };
                }
                return epayment;
            }
        }
        public static double GetSuccessPaymentTotal(Student Student)
        {
            using (DataTable dt = DAL.GetSuccessPaymentTotal(Student.Id))
            {
                return Convert.ToDouble(dt.Rows[0][0]);
            }
        }

        #endregion
    }
    public partial class DataAccessLayer
    {
        public DataTable GetSuccessPaymentTotal(int pStudentId)
        {
            return DatabaseContext.ExecuteReader(CommandType.StoredProcedure, "_Payment_GetSuccessPaymentTotal", MethodBase.GetCurrentMethod().GetParameters(), new object[] { pStudentId });
        }

        public DataTable GetListByEpaymentById(int pId)
        {
            return DatabaseContext.ExecuteReader(CommandType.StoredProcedure, "_Payment_GetListEPaymentListById", MethodBase.GetCurrentMethod().GetParameters(), new object[] { pId });
        }

        public bool UpdatePaymentStatus(int pId, int pStatus)
        {
            return DatabaseContext.ExecuteNonQuery(CommandType.StoredProcedure, "_Admin_Update_PaymentStatus", MethodBase.GetCurrentMethod().GetParameters(), new object[] { pId, pStatus });
        }

        public DataTable InsertPayment(string pCardNumber, string pNameSurname, string pExpMonth, string pExpYear, string pCvc, string pAmount, string pInstallment, int pStudentId, string pStudentCode, string pStudentName, string pBankName, double pTotal, string pRate, int pBankId, string pPaymentId, int pSystemType, string pPhoneNumber, string pNote, string pCardType, int pTeacherId, string pExtraInstallment, string pStudentTotalBonus, string pExpendableBonus, bool pUseBonus, bool p3DSecure, int pGroupId, int pPosBankId)
        {
            return DatabaseContext.ExecuteReader(CommandType.StoredProcedure, "_Payment_Insert_Payment_2", MethodBase.GetCurrentMethod().GetParameters(), new object[] { pCardNumber, pNameSurname, pExpMonth, pExpYear, pCvc, pAmount, pInstallment, pStudentId, pStudentCode, pStudentName, pBankName, pTotal, pRate, pBankId, pPaymentId, pSystemType, pPhoneNumber, pNote, pCardType, pTeacherId, pExtraInstallment, pStudentTotalBonus, pExpendableBonus, pUseBonus, p3DSecure, pGroupId, pPosBankId });
        }
        public bool UpdatePayment(int pId, string pAuthCode, string pProcReturnCode, string pErrMsg, string pIpAddress, string pOrderId, string pOid, string pGroupId, string pTransId, string pExtra, string pUseEPaymentType, DateTime pProcessingDate, int pUseEpaymentId, string pCampaignUrl)
        {
            return DatabaseContext.ExecuteNonQuery(CommandType.StoredProcedure, "_Payment_Update_Payment", MethodBase.GetCurrentMethod().GetParameters(), new object[] { pId, pAuthCode, pProcReturnCode, pErrMsg, pIpAddress, pOrderId, pOid, pGroupId, pTransId, pExtra, pUseEPaymentType, pProcessingDate, pUseEpaymentId, pCampaignUrl });
        }
        public DataTable GetEPaymentListByStudentId(int pStudentId)
        {
            return DatabaseContext.ExecuteReader(CommandType.StoredProcedure, "_Payment_GetListEPaymentListByStudentId", MethodBase.GetCurrentMethod().GetParameters(), new object[] { pStudentId });
        }
        public DataTable GetListEpayment(DateTime pStartDate, DateTime pEndDate, string pT9Text, int pPaymentStatu)
        {
            return DatabaseContext.ExecuteReader(CommandType.StoredProcedure, "_Admin_GetListEPayment", MethodBase.GetCurrentMethod().GetParameters(), new object[] { pStartDate, pEndDate, pT9Text, pPaymentStatu });
        }
        public DataTable GetItemEPaymentByInsertId(int pInsertId)
        {
            return DatabaseContext.ExecuteReader(CommandType.StoredProcedure, "_GetItem_PaymentByInsertId", MethodBase.GetCurrentMethod().GetParameters(), new object[] { pInsertId });
        }
    }
}