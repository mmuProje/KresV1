using Kres.Models.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Kres.Models.EntityLayer
{
    [Serializable]
    public class PosPaymentValue
    {
        #region Properties
        public PosCardType CardType { get; set; }
        public string Bank { get; set; }
        public string NameSurname { get; set; }
        public string CardNumber { get; set; }
        public int Installment { get; set; }
        public int ExtraInstallment { get; set; }
        public int DeferalInstallment { get; set; }
        public double CommissionRate { get; set; }
        public string InstallmentNote { get; set; }
        public string InstallmentAutoNote { get; set; }
        public string ExpMounth { get; set; }
        public string ExpYear { get; set; }
        public int Price { get; set; }
        public int DecimalPrice { get; set; }
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
        public string Explanation { get; set; }
        public double TotalPrice { get; set; }
        public double MonthPrice { get; set; }
        public int TeacherId { get; set; }
        public string CampaignCode { get; set; }
        public string TotalBonusPrice { get; set; }
        public string TotalBonusDecimal { get; set; }
        public bool UseBonus { get; set; }
        public string ExpendableBonus { get; set; }
        public string TotalBonus { get; set; }
        public string CampaingUrl { get; set; }
        public int InsertPointId { get; set; }
        public int InsertCancelId { get; set; }

        #endregion
    }
    [Serializable]
    [DataContract]
    public class PosBankInformation
    {
        [DataMember(Name = "BankId")]
        public int BankId { get; set; }
        [DataMember(Name = "Explanation")]
        public string Explanation { get; set; }
        [DataMember(Name = "BankName")]
        public string BankName { get; set; }
        [DataMember(Name = "CardCode")]
        public int CardCode { get; set; }
        [DataMember(Name = "CardType")]
        public string CardType { get; set; }
        [DataMember(Name = "Bin")]
        public string Bin { get; set; }

    }


    public enum PosSystemType
    {
        Android = 3,
        Web = 1,
        WinForm = 2
    }

    public enum PosCardType
    {
        Visa,
        Master,
        Maestro,
        Amex,
        Troy,
        None
    }
    public enum PosBankName
    {
        ISBANK,
        AKBANK,
        YAPIKREDI,
        TEB,
        FINANSBANK,
        HALKBANK,
        GARANTIBANK,
        ZIRAATBANK,
        DENIZBANK,
        VAKIFBANK,
        BANKASYA,
        BELIRSIZ
    }

}