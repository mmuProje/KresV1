using System;
using Kres.Models.EntityLayer;
using Kres.Models.Helper;
using Newtonsoft.Json;

namespace Kres.Models.EntityLayer
{
    [Serializable]
    public class EPaymentLog
    {
        public string CompanyName { get; set; }
        public string NameSurname { get; set; }
        public string CardNumber { get; set; }
        public string ExpMounth { get; set; }
        public string ExpYear { get; set; }
        public string Installment { get; set; }
        public string CardType { get; set; }
        public string Cvc { get; set; }
        public double Amount { get; set; }
        public double Total { get; set; }
        public string Bank { get; set; }
        public int BankId { get; set; }
        public string PaymentId { get; set; }
        public string StudentCode { get; set; }
        public int StudentId { get; set; }
        public bool _3DSecure { get; set; }
        public string Description { get; set; }
        public string PhoneNumber { get; set; }
        public int TeacherId { get; set; }
        public DateTime RecordDate { get; set; }
        public string XmlRequest { get; set; }
        public string XmlResponse { get; set; }
        public string AuthCode { get; set; }
        public string ProcReturnCode { get; set; }
        public string ErrorMessage { get; set; }
        public string OrderId { get; set; }
        public string PaymentBank { get; set; }
        public int Status { get; set; }
        public string IpAddress { get; set; }
        [JsonIgnore]
        public bool LogToDB { get; set; }

        public EPaymentLog(PosPaymentValue log, string xmlRequest, string xmlResponse, string authCode, string procReturnCode, string errMsg, string orderId, int status = 99)
        {
            string key = "cNqXTA" + Token.ChangeCharacter(GlobalSettings.CompanyName) + "nFmRq";
            CompanyName = Token.ChangeCharacter(GlobalSettings.CompanyName);
            NameSurname = Token.Encrypt(log.NameSurname, key);
            CardNumber = Token.Encrypt(log.CardNumber.Substring(0, 6) + "******" + log.CardNumber.Substring(log.CardNumber.Length - 4, 4), key);
            ExpMounth = Token.Encrypt(log.ExpMounth, key);
            ExpYear = Token.Encrypt(log.ExpYear, key);
            Installment = log.Installment.ToString();
            CardType = log.CardType.ToString();
            Cvc = Token.Encrypt(log.Cvc, key);
            Amount = Convert.ToDouble(log.TotalPrice);
            Total = log.TotalPrice;
            Bank = log.Bank;
            BankId = Convert.ToInt32(log.BankId);
            PaymentId = log.PaymentId;
            StudentCode = Token.Encrypt(log.StudentCode, key);
            StudentId = log.StudentId;
            _3DSecure = log._3DSecure;
            Description =log.Explanation;
            PhoneNumber = log.PhoneNumber;
            TeacherId = log.TeacherId;
            RecordDate = DateTime.Now;
            XmlRequest = Token.Encrypt(xmlRequest, key);
            XmlResponse = Token.Encrypt(xmlResponse, key);
            AuthCode = authCode;
            ProcReturnCode = procReturnCode;
            ErrorMessage = errMsg;
            OrderId = Token.Encrypt(orderId, key);
            Status = status;
            PaymentBank = log.Bank;
        }
    }
    public enum LogType
    {
        Start = 1,
        Continues = 2,
        Finish = 3,
        Stop = 4,
        NoData = 5,
        Error = 99
    }

}