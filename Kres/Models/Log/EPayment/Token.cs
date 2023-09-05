using System;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using Kres.Models.EntityLayer;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace Kres.Models.EntityLayer
{
    public class Token
    {
        public static string Decrypt(string encryptedText, string key)
        {
            string initVector = "tu89geji340taliu2";
            try
            {
                int keysize = 256;
                int modAl = encryptedText.Length % 4;
                if (modAl > 0)
                {
                    encryptedText += new string('=', 4 - modAl);
                }

                byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
                byte[] DeEncryptedText = Convert.FromBase64String(encryptedText);
                PasswordDeriveBytes password = new PasswordDeriveBytes(key, null);
                byte[] keyBytes = password.GetBytes(keysize / 8);
                RijndaelManaged symmetricKey = new RijndaelManaged();
                symmetricKey.Mode = CipherMode.CBC;
                ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
                MemoryStream memoryStream = new MemoryStream(DeEncryptedText);
                CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
                byte[] plainTextBytes = new byte[DeEncryptedText.Length];
                int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                memoryStream.Close();
                cryptoStream.Close();
                return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
            }
            catch
            {
                return string.Empty;
            }
        }

        public static string Encrypt(string text, string key)
        {
            string initVector = "tu89geji340taliu2";
            try
            {
                int keysize = 256;

                byte[] initVectorBytes = Encoding.UTF8.GetBytes(initVector);
                byte[] plainTextBytes = Encoding.UTF8.GetBytes(text);
                PasswordDeriveBytes password = new PasswordDeriveBytes(key, null);
                byte[] keyBytes = password.GetBytes(keysize / 8);
                RijndaelManaged symmetricKey = new RijndaelManaged();
                symmetricKey.Mode = CipherMode.CBC;
                ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
                MemoryStream memoryStream = new MemoryStream();
                CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                cryptoStream.FlushFinalBlock();
                byte[] Encrypted = memoryStream.ToArray();
                memoryStream.Close();
                cryptoStream.Close();
                string result = Convert.ToBase64String(Encrypted);
                if (result.Contains(" "))
                    return Encrypt(text, key);
                else
                    return result;
            }
            catch
            {
                return string.Empty;
            }
        }

        public static string ChangeCharacter(string pName)
        {
            return pName.Replace("Ü", "").Replace("Ğ", "").Replace("İ", "").Replace("Ş", "").Replace("Ç", "").Replace("Ö", "").Replace("ı", "").Replace(" ", "");
        }
        public static string GetUserToken()
        {
            string key = "cNqXTA" + ChangeCharacter(GlobalSettings.CompanyName) + "nFmRq";
            string password = Encrypt(GlobalSettings.PaymentLogPassword, key);
            try
            {
                if (string.IsNullOrEmpty(GlobalSettings.PaymentLogPassword))
                {
                    return "ERROR=Api şifresi yok";
                }

                string url = GlobalSettings.PaymentLogAdress + "/token";

                var client = new RestClient(url);
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/x-www-form-urlencoded");
                request.AddHeader("cache-control", "no-cache");
                request.AddParameter("application/x-www-form-urlencoded", "grant_type=password&username=" + "EP_" + ChangeCharacter(GlobalSettings.CompanyName) + "&password=" + password, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    JToken token = JObject.Parse(response.Content);
                    return token.SelectToken("access_token").ToString();
                }
                else
                {
                    dynamic resp = JObject.Parse(response.Content);
                    return "ERROR=" + resp.error_description;
                }
            }
            catch (Exception ex)
            {
                return "ERROR=" + ex.Message;
            }
        }

        public static bool GetTableControl()
        {
            try
            {
                string token = GetUserToken();
                if (token.Contains("ERROR="))
                {
                    Logger.LogGeneral(LogGeneralErrorType.Error,ClientType.B2BWeb, "Api Table Control Error", new Exception(token),"");
                    return false;

                }
                string url = GlobalSettings.PaymentLogAdress + "/api/payment/Create/" + ChangeCharacter(GlobalSettings.CompanyName).ToLower();
                var client = new RestClient(url);
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("authorization", "Bearer " + token);
                request.AddHeader("content-type", "application/json");
                IRestResponse response = client.Execute(request);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return true;
                }
                else
                {
                    dynamic resp = JObject.Parse(response.Content);
                    Logger.LogGeneral(LogGeneralErrorType.Error, ClientType.B2BWeb, "Api Table Control Error 1", new Exception(response.Content),"");
                    return false;

                }

            }
            catch (Exception ex)
            {
                Logger.LogGeneral(LogGeneralErrorType.Error,ClientType.B2BWeb, "Api Table Control Error 2", ex,"");
                return false;
            }
        }
    }
}