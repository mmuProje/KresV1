using Kres.Models.DataLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Security;

namespace Kres.Models.EntityLayer
{
    public class Student : DataAccess
    {

        #region Constructors
        public Student()
        {


        }
        #endregion

        #region Properties
        public int Id { get; set; }
        public string Code { get; set; }
        public string LoginCode { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string GuardianName { get; set; }
        public string Address { get; set; }
        public string Town { get; set; }
        public string City { get; set; }
        public string Mail { get; set; }
        public bool Web { get; set; }
        public bool Android { get; set; }
        public bool Ios { get; set; }
        public int Status { get; set; }
        public int PaymentType { get; set; }
        public string Tel1 { get; set; }
        public string Tel2 { get; set; }
        public string Fax1 { get; set; }
        public string Gsm1 { get; set; }
        public string Gsm2 { get; set; }
        public string Notes { get; set; }
        public DateTime RecordDate { get; set; }
        public DateTime EditDate { get; set; }
        public string TradingGroup { get; set; }
        public string PaymentCode { get; set; }
        public bool PaymentOnOrder { get; set; }
        public bool StatuOriginalPrice { get; set; }
        public int LanguageId { get; set; }
        #endregion

        #region Method
        public static Student GetById(int id, int userId)
        {

            #region Yeni arama için eklenecek
            Student item = new Student();
            //DataSet ds = DAL.GetCustomerById(id, userId);
            //DataTable dtCustomer = ds.Tables.Count > 0 ? ds.Tables[0] : new DataTable();
            //DataTable dtCustomerRule = ds.Tables.Count > 1 ? ds.Tables[1] : new DataTable();
            //DataTable dtCustomerPassiveManufacturer = ds.Tables.Count > 2 ? ds.Tables[2] : new DataTable();
            //DataTable dtCustomerProductComparison = ds.Tables.Count > 3 ? ds.Tables[3] : new DataTable();
            //#endregion

            //if (dtCustomer.Rows.Count > 0)
            //{
            //    DataRow row = dtCustomer.Rows[0];

            //    item.CustomerRule = new List<string>();
            //    if (dtCustomerRule.Rows.Count > 0)
            //    {
            //        foreach (DataRow rowRule in dtCustomerRule.Rows)
            //        {
            //            string ruleProduct = rowRule.Field<string>("Product");
            //            item.CustomerRule.Add(ruleProduct);
            //        }
            //    }

            //    item.Id = row.Field<int>("Id");
            //    item.LanguageId = row.Field<int>("LanguageId");
            //    item.StartScreen = row.Field<string>("StartScreen");
            //    item.IsStartScreen = row.Field<bool>("IsStartScreen");
            //    item.EntegreId = row.Field<int>("EntegreId");
            //    item.B2bCode = row.Field<string>("B2bCode");
            //    item.Code = row.Field<string>("Code");
            //    item.Name = row.Field<string>("Name");
            //    item.DueDay = row.Field<int>("DueDay");
            //    item.Address = row.Field<string>("Address");
            //    item.Country = row.Field<string>("Country");
            //    item.City = row.Field<string>("City");
            //    item.Town = row.Field<string>("Town");
            //    item.Fax1 = row.Field<string>("Fax1");
            //    item.Tel1 = row.Field<string>("Tel1");
            //    item.Mail = row.Field<string>("Mail");
            //    item.RuleCode = row.Field<string>("RuleCode");
            //    item.RiskLimit = row.Field<double>("RiskLimit");
            //    item.PaymentType = (PaymentType)(row.Field<int>("PaymentType"));
            //    item.NumberOfUser = row.Field<int>("NumberOfUser");
            //    item.NumberOfUserIos = row.Field<int>("NumberOfUserIos");
            //    item.NumberOfUserAndroid = row.Field<int>("NumberOfUserAndroid");
            //    item.IsEnteringInformationPermitted = row.Field<bool>("EnteringInformation");
            //    item.SpecialInstallment = row.Field<bool>("SpecialInstallment");
            //    item.TaxOffice = row.Field<string>("TaxOffice");
            //    item.TaxNumber = row.Field<string>("TaxNumber");
            //    item.PaymentOnOrder = row.Field<bool>("PaymentOnOrder");
            //    item.ShipmentId = row.Field<int>("ShipmentId");
            //    item.VatRate = row.Field<double>("VatRate");
            //    item.IsVatRateActive = row.Field<bool>("IsVatRateActive");
            //    item.IsCurrentAccountStatu = row.Field<bool>("IsCurrentAccountStatu");
            //    item.Password = row.Field<string>("CurrentAccountPassword");
            //    item.CurrentAccountPassword = MD5Sifreleme(row.Field<string>("CurrentAccountPassword"));
            //    item.CurrencyType = row.Field<string>("CurrencyType");
            //    item.CampaignStatu = row.Field<bool>("CampaignStatu");
            //    item.IsCampaignCodeActive = row.Field<bool>("IsCampaignCodeActive");
            //    item.CampaignCode = row.Field<string>("CampaignCode");
            //    item.IsConfirmKvkk = row.Field<int>("IsConfirmKvkk");
            //    item.CustomerCargo = row.Field<string>("CustomerCargo");
            //    item.CartDueDay = row.Field<int>("CartDueDay");
            //    item.AuthorityCustomer = new AuthorityCustomer()
            //    {
            //        CustomerId = row.Field<int>("Id"),
            //        _EnteringInformation = row.Field<bool>("EnteringInformation"),
            //        _CheckBasket = row.Field<bool>("CheckBasket"),
            //        _ProductRestoration = row.Field<bool>("ProductRestoration"),
            //        _ShowQuantity = row.Field<bool>("ShowQuantity"),
            //        _ShowDiscount = row.Field<bool>("ShowDiscount"),
            //        _WebLogin = row.Field<bool>("WebLogin"),
            //        _IsTecdoc = row.Field<bool>("IsTecdoc"),
            //        _B2bBasketTransferStatu = row.Field<bool>("B2bBasketTransferStatu"),
            //        _IsBalanceControlRemove = row.Field<bool>("_IsBalanceControlRemove"),
            //        _IsPaymentBalanceControlRemove = row.Field<bool>("_IsPaymentBalanceControlRemove"),
            //    };
            //}

            return item;
        }
        #endregion
    }
    #endregion
}

public partial class DataAccessLayer
{

}
