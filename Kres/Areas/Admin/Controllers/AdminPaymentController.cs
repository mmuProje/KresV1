using Kres.Areas.Admin.Models;
using Kres.Models.EntityLayer;
using Kres.Models.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SelectPdf;

namespace Kres.Areas.Admin.Controllers
{
    public class AdminPaymentController : AdminBaseController
    {
        // GET: Admin/AdminPayment
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult PaymentList()
        {
            return View();
        }

        public ActionResult Detail(int id = -1)
        {
            if (id < 1)
                return View();
            else
                return View();
        }
        #region   HttpPost Methods

        [HttpPost]
        public JsonResult SavePdf(EPayment eItem)//
        {
            //DocumentDraftcs documentDraft = DocumentDraftcs.GetDocumentDraft(0, false);

            EPayment item = EPayment.GetListByEpaymentById(eItem.Id);

            string contentHtml = "";
            //contentHtml = documentDraft.ContentBody != string.Empty ? documentDraft.ContentBody:documentDraft.EmailBody;
            contentHtml = System.IO.File.ReadAllText(Server.MapPath("/files/mailtemplate/Epayment-Success-Admin.html"));

            contentHtml = contentHtml.Replace("{PaymentId}", item.PaymentId).Replace("{PaymentNumber}", item.Id.ToString()).Replace("{CustomerCode}", item.Student.Code).Replace("{CustomerName}", item.Student.Name).Replace("{3DSecure}", item._3DSecure ? "3D Secure" : "Normal").Replace("{BankName}", item.BankName).Replace("{Amount}", item.Amount).Replace("{Installment}", item.Installment).Replace("{InstallmentAmount}", (item.Total / Convert.ToInt32(item.Installment)).ToString("N2")).Replace("{CreateDate}", item.RecordDate.ToString()).Replace("{ExpendableBonus}", item.ExpendableBonus).Replace("{AuthCode}", item.AuthCode).Replace("{CardNumber}", item.CardNumber).Replace("{NameSurname}", item.NameSurname).Replace("{PhoneNumber}", item.PhoneNumber).Replace("{SanalPosBankName}", item.UseEPaymentType);

            HtmlToPdf converter = new HtmlToPdf();
            converter.Options.MarginLeft = 40;
            converter.Options.MarginRight = 40;
            converter.Options.MarginTop = 10;
            converter.Options.MarginBottom = 10;
            // create a new pdf document converting an url
            PdfDocument doc = converter.ConvertHtmlString(contentHtml, string.Empty);



            // custom header on page 3
            if (doc.Pages.Count >= 3)
            {
                PdfPage page = doc.Pages[2];
            }



            string path = "Files/Payment/" + Guid.NewGuid() + "/" + AdminCurrentTeacher.Code + ".pdf";

            if (System.IO.File.Exists(Server.MapPath(path)))
            {
                System.IO.File.Delete(Server.MapPath(path));
            }

            doc.Save(Server.MapPath("~/" + path));
            doc.Close();

            return Json(GlobalSettings.B2bAddress + path);
            //return Json("http://localhost:9906/" + path);
            //return Json("https://localhost:44326/" + path);
        }


        [HttpPost]
        public string GetListPayment(PaymentSearchCriteria paymentSearchCriteria)
        {
            paymentSearchCriteria.EndDate = paymentSearchCriteria.EndDate.Date.Add(new TimeSpan(23, 59, 59));
            return JsonConvert.SerializeObject(EPayment.GetListEpayment(paymentSearchCriteria.StartDate, paymentSearchCriteria.EndDate, paymentSearchCriteria.T9Text, paymentSearchCriteria.PaymentStatu));
        }

        [HttpPost]
        public JsonResult GetBankList()
        {
            List<PosBank> list = PosBank.GetList();
            return Json(list);
        }


        [HttpPost]
        public JsonResult GetPosOfBankList(int posBankId)
        {
            List<PosOfBank> list = PosOfBank.GetPosOfBankList(posBankId);
            return Json(list);
        }

        [HttpPost]
        public JsonResult UpdatePosOfBank(PosOfBank posOfBankItem)
        {
            bool result = false;

            posOfBankItem.EditId = AdminCurrentTeacher.Id;
            result = posOfBankItem.Update();

            var message = result ? new MessageBox(MessageBoxType.Success, ("Admin.CommonController.Success").toLanguage()) : new MessageBox(MessageBoxType.Error, ("Admin.CommonController.Unsuccess").toLanguage());
            return Json(message);
        }

        [HttpPost]
        public JsonResult UpdatePaymentStatus(int id, int status)
        {
            bool result = false;

            result = EPayment.UpdatePaymentStatus(id, status);
            var message = result ? new MessageBox(MessageBoxType.Success, ("Admin.CommonController.Success").toLanguage()) : new MessageBox(MessageBoxType.Error, ("Admin.CommonController.Unsuccess").toLanguage());
            return Json(message);
        }

        [HttpPost]
        public JsonResult GetPosBankDetail(int posBankId)
        {
            PosBankDetail item = PosBankDetail.GetPosBankdEtailByPosBankId(posBankId);
            return Json(item);
        }


        [HttpPost]
        public JsonResult SaveDetailItem(PosBankDetail selectedItem, List<PosInstallment> installmentList)
        {
            bool result = false;
            int id = 0;

            selectedItem.CreateId = AdminCurrentTeacher.Id;
            selectedItem.EditId = AdminCurrentTeacher.Id;

            if (selectedItem.Id != 0)
            {
                PosBankCheck(selectedItem);
                result = selectedItem.Update();
            }
            else
            {
                PosBankCheck(selectedItem);
                id = selectedItem.Add();
            }
            selectedItem.Id = id;
            if (id > 0)
                result = true;

            if (installmentList.Where(x => x.Checked).ToList().Count > 0)
            {
                foreach (PosInstallment item in installmentList.Where(x => x.Checked).ToList())
                {
                    item.Note = string.IsNullOrEmpty(item.Note) ? string.Empty : item.Note;
                    item.PosBankId = selectedItem.PosBankId;
                    item.CreateId = AdminCurrentTeacher.Id;
                    item.Add();
                }
            }
            else
            {
                foreach (PosInstallment item in installmentList.Where(x => x.Installment == 1).ToList())
                {
                    item.Note = string.IsNullOrEmpty(item.Note) ? string.Empty : item.Note;
                    item.PosBankId = selectedItem.PosBankId;
                    item.CreateId = AdminCurrentTeacher.Id;
                    item.Installment = -1;
                    item.Add();
                }
            }

            var message = result ? new MessageBox(MessageBoxType.Success, ("Admin.CommonController.Success").toLanguage(), selectedItem.Id) : new MessageBox(MessageBoxType.Error, ("Admin.CommonController.Unsuccess").toLanguage());

            return Json(message);
        }

        private static void PosBankCheck(PosBankDetail selectedItem)
        {
            selectedItem.PosnetId = string.IsNullOrEmpty(selectedItem.PosnetId) ? string.Empty : selectedItem.PosnetId;
            selectedItem.TerminalNo = string.IsNullOrEmpty(selectedItem.TerminalNo) ? string.Empty : selectedItem.TerminalNo;
            selectedItem.KeyWord = string.IsNullOrEmpty(selectedItem.KeyWord) ? string.Empty : selectedItem.KeyWord;
        }

        [HttpPost]
        public JsonResult GetPosInstallment(int posBankId)
        {
            List<PosInstallment> list = PosInstallment.GetInstallmentByPosBankId(posBankId);
            return Json(list);
        }

        [HttpPost]
        public JsonResult UpdatePosBank(PosBank selectedItem)
        {
            bool result = false;

            selectedItem.EditId = AdminCurrentTeacher.Id;
            result = selectedItem.Update();

            var message = result ? new MessageBox(MessageBoxType.Success, ("Admin.CommonController.Success").toLanguage()) : new MessageBox(MessageBoxType.Error, ("Admin.CommonController.Unsuccess").toLanguage());
            return Json(message);
        }

        #endregion
        public class PaymentSearchCriteria
        {
            public string T9Text { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public int PaymentStatu { get; set; }
        }

        public ReturnParameters CancelProcessOnBank(int id)
        {
            ReturnParameters returnValues = new ReturnParameters();

            try
            {
                RefundPostParameters refundPostParameters = RefundPostParameters.GetRefundItemById(id);

                RefundPostParametersDTO refundItem = new RefundPostParametersDTO()
                {
                    ApiName = refundPostParameters.ApiName,
                    Password = refundPostParameters.Password,
                    TerminalId = refundPostParameters.TerminalId,
                    ClientId = refundPostParameters.ClientId,
                    OrderId = refundPostParameters.OrderId,
                    Amount = refundPostParameters.Amount,
                    AuthCode = refundPostParameters.AuthCode,
                    PostAddress = refundPostParameters.PostAddress,
                    VirtualPosId = refundPostParameters.VirtualPosId,
                    HostLogKey = refundPostParameters.HostLogKey,
                    InstallmentCount = refundPostParameters.InstallmentCount,
                    OriginalRetrefNumber = refundPostParameters.OriginalRetrefNumber,
                    PasswordOfProvrfn = refundPostParameters.PasswordOfProvrfn
                };

                var httpWebRequest = (HttpWebRequest)WebRequest.Create(refundPostParameters.PostAddress);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string refundJson = Kres.Models.Helper.JsonSerializer.Serialize(refundItem);
                    streamWriter.Write(refundJson);
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    string response = streamReader.ReadToEnd();
                    returnValues = Kres.Models.Helper.JsonSerializer.Deserialize<ReturnParameters>(response);
                }
            }
            catch (Exception ex)
            {
                returnValues.IsSuccess = false;
                returnValues.ResultMessage = "İşlem başarısız. " + ex.Message;
            }

            return returnValues;
        }
    }
}