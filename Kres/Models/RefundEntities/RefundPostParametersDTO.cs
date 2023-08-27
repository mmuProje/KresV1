using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kres.Models.EntityLayer
{
    public class RefundPostParametersDTO
    {
        public string ApiName { get; set; }
        public string Password { get; set; }
        public string ClientId { get; set; }
        public string OrderId { get; set; }
        public double Amount { get; set; }
        public int VirtualPosId { get; set; }
        public string TerminalId { get; set; }
        public string PasswordOfProvrfn { get; set; }
        public string InstallmentCount { get; set; }
        public string HostLogKey { get; set; }
        public string OriginalRetrefNumber { get; set; }
        public string AuthCode { get; set; }
        public string PostAddress { get; set; }
    }
}