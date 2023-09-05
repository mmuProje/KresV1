using System;
using System.Threading.Tasks;

namespace Kres.Models.EntityLayer
{
    public class Logger
    {

        public static Task<bool> LogGeneral(LogGeneralErrorType type, ClientType clientType, string source, Exception ex, string ipAddress, int StudentId = -1, int userId = -1, int TeacherId = -1, int licenceId = -1, double latitude = 0, double longitude = 0)
        {
            LogGeneralError log = new LogGeneralError()
            {
                LogType = type,
                Client = clientType,
                Explanation = (ex != null) ? ex.Message : string.Empty,
                Source = source,
                IpAddress = ipAddress,
                StudentId = StudentId,
                TeacherId = TeacherId,
                UserId = userId,
                LicenceId = licenceId,
                Latitude = latitude,
                Longitude = longitude,

            };
            return Task.Run(() => log.Save());
        }

        public static Task<bool> LogGeneral(LogGeneralErrorType type, ClientType clientType, string source, string ex, string ipAddress, int StudentId = -1, int userId = -1, int TeacherId = -1, int licenceId = -1, double latitude = 0, double longitude = 0)
        {
            LogGeneralError log = new LogGeneralError()
            {
                LogType = type,
                Client = clientType,
                Explanation = ex ?? string.Empty,
                Source = source,
                IpAddress = ipAddress,
                StudentId = StudentId,
                TeacherId = TeacherId,
                UserId = userId,
                LicenceId = licenceId,
                Latitude = latitude,
                Longitude = longitude

            };
            return Task.Run(() => log.Save());
        }

        public static Task<bool> LogTransaction(ClientType clientType, LogTransactionSource source, string process, string message, string ipAddress, int licenceId = -1, int StudentId = -1, int userId = -1, int TeacherId = -1, double latitude = 0.0, double longitude = 0.0)
        {
            LogTransaction log = new LogTransaction()
            {
                ClientType = clientType,
                StudentId = StudentId,
                TeacherId = TeacherId,
                UserId = userId,
                Source = source,
                Process = process,
                Explanation = message ?? string.Empty,
                Latitude = latitude,
                Longitude = longitude,
                IpAddress = ipAddress,

                LicenceId = licenceId
            };
            return Task.Run(() => log.Save());
        }
        public static Task<bool> LogNavigation(int StudentId, int userId, int TeacherId, string navigation, ClientType clientType, string ipAddress)
        {

            LogNavigation log = new LogNavigation()
            {
                StudentId = StudentId,
                UserId = userId,
                TeacherId = TeacherId,
                Navigation = navigation,
                ClientType = clientType,
                IpAddress = ipAddress
            };
            return Task.Run(() => log.Save());
        }



      

       

        

        #region PaymentLog

        public static int LogPayment(LogPaymentType type, string source, string ex, string currentpaymentid, string bankName, int StudentId = -1, int TeacherId = -1, bool redirect = true)
        {
            LogPayment log = new LogPayment()
            {
                LogType = type,
                Explanation = ex,
                Source = source,
                StudentId = StudentId,
                TeacherId = TeacherId,
                CurrentPaymentId = currentpaymentid,
                BankName = bankName
            };

            log.Save();
            return log.Id;
        }
        #endregion


    }

}