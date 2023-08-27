using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Kres.Models.EntityLayer
{
    public class PaymentContract : DataAccess
    {
        #region Properties
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int OrderId { get; set; }
        public string PaymentId { get; set; }
        public string Contract { get; set; }
        #endregion
    }
}