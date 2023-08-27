using System;

namespace Kres.Models.EntityLayer
{
    public class PaymentControlDenizBankTemp
    {
        public PaymentControlDenizBankTemp()
        {
        }
        #region Properties
        public string OrderId { get; set; }
        public string ProcReturnCode { get; set; }
        public string HostRefNum { get; set; }
        public string AuthCode { get; set; }
        public string TxnResult { get; set; }
        public string ErrorMessage { get; set; }
        public string CampanyId { get; set; }
        public string CampanyInstallCount { get; set; }
        public string CampanyShiftDateCount { get; set; }
        public string CampanyTxnId { get; set; }
        public string CampanyType { get; set; }
        public string CampanyInstallment { get; set; }
        public string CampanyDate { get; set; }
        public string CampanyAmnt { get; set; }
        public DateTime TRXDATE { get; set; }
        public string TransId { get; set; }
        public string ErrorCode { get; set; }
        public string EarnedBonus { get; set; }
        public string UsedBonus { get; set; }
        public string AvailableBonus { get; set; }
        public string BonusToBonus { get; set; }
        public string CampaignBonus { get; set; }
        public string FoldedBonus { get; set; }
        public string SurchargeAmount { get; set; }
        public string Amount { get; set; }
        #endregion
        #region Methods
        public PaymentControlDenizBankTemp SetValue(PaymentControlDenizBankTemp pDenizbank, string pProperty, string pValue)
        {
            switch (pProperty)
            {
                case "OrderId": pDenizbank.OrderId = pValue; break;
                case "ProcReturnCode": pDenizbank.ProcReturnCode = pValue; break;
                case "HostRefNum": pDenizbank.HostRefNum = pValue; break;
                case "AuthCode": pDenizbank.AuthCode = pValue; break;
                case "TxnResult": pDenizbank.TxnResult = pValue; break;
                case "ErrorMessage": pDenizbank.ErrorMessage = pValue; break;
                case "CampanyId": pDenizbank.CampanyId = pValue; break;
                case "CampanyInstallCount": pDenizbank.CampanyInstallCount = pValue; break;
                case "CampanyShiftDateCount": pDenizbank.CampanyShiftDateCount = pValue; break;
                case "CampanyTxnId": pDenizbank.CampanyTxnId = pValue; break;
                case "CampanyType": pDenizbank.CampanyType = pValue; break;
                case "CampanyInstallment": pDenizbank.CampanyInstallment = pValue; break;
                case "CampanyDate": pDenizbank.CampanyDate = pValue; break;
                case "CampanyAmnt": pDenizbank.CampanyAmnt = pValue; break;
                case "TRXDATE":
                    try
                    {
                        pDenizbank.TRXDATE = Convert.ToDateTime(pValue);
                    }
                    catch (Exception)
                    {
                        pDenizbank.TRXDATE = DateTime.MinValue;
                    }
                    break;
                case "TransId": pDenizbank.TransId = pValue; break;
                case "ErrorCode": pDenizbank.ErrorCode = pValue; break;
                case "EarnedBonus": pDenizbank.EarnedBonus = pValue; break;
                case "UsedBonus": pDenizbank.UsedBonus = pValue; break;
                case "AvailableBonus": pDenizbank.AvailableBonus = pValue; break;
                case "BonusToBonus": pDenizbank.BonusToBonus = pValue; break;
                case "CampaignBonus": pDenizbank.CampaignBonus = pValue; break;
                case "FoldedBonus": pDenizbank.FoldedBonus = pValue; break;
                case "SurchargeAmount": pDenizbank.SurchargeAmount = pValue; break;
                case "Amount": pDenizbank.Amount = pValue; break;
            }
            return pDenizbank;
        }
        #endregion
    }
}