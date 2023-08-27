using System;

namespace Kres.Models.EntityLayer
{
    [Serializable]
    public class PaymentValue
    {
        #region Properties
        public PosCardType CardType { get; set; }
        public string Bank { get; set; }
        public string NameSurname { get; set; }
        public string CardNumber { get; set; }
        public string Installment { get; set; }
        public string ExpMounth { get; set; }
        public string ExpYear { get; set; }
        public string Amount { get; set; }
        public double Total { get; set; }
        public string Cvc { get; set; }
        public int BankId { get; set; }
        public int InsertId { get; set; }
        public string PaymentId { get; set; }
        public SystemType SystemType { get; set; }
        public string StudentCode { get; set; }
        public int StudentId { get; set; }
        public string IpAddress { get; set; }
        public bool _3DSecure { get; set; }
        public string KOICode { get; set; }
        public string Description { get; set; }
        public string PhoneNumber { get; set; }
        public string CampaingUrl { get; set; }



        #endregion
    }
    [Serializable]
    public class BankInformation
    {
        public int BankId { get; set; }
        public int Explanation { get; set; }
        public string BankName { get; set; }
        public string CardCode { get; set; }
    }
    
}