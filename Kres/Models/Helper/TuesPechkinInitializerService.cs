using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Web;
using TuesPechkin;

namespace Kres.Models.Helper
{
    public static class TuesPechkinInitializerService
    {
        private static string staticDeploymentPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ExternalDll");

        public static void CreateWkhtmltopdfPath()
        {
            if (Directory.Exists(staticDeploymentPath) == false)
            {
                Directory.CreateDirectory(staticDeploymentPath);
            }
        }

        public static IConverter converter =
            new ThreadSafeConverter(
                new RemotingToolset<PdfToolset>(
                    new WinAnyCPUEmbeddedDeployment(
                        new StaticDeployment(staticDeploymentPath)
                    )
                )
            );


        public static string StartConvertToHtml(string html, string path, HttpRequestBase Request, string headerHtml = "", string footerHtml = "", TuesPechkin.GlobalSettings.PaperOrientation paperOrientation = TuesPechkin.GlobalSettings.PaperOrientation.Portrait)
        {
            bool result = true;
            string url = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);
            byte[] buf = null;
            string headerPath = Path.Combine(Path.GetTempPath(), String.Format("{0}.html", Guid.NewGuid()));
            System.IO.File.WriteAllText(headerPath, headerHtml);
            string footerPath = Path.Combine(Path.GetTempPath(), String.Format("{0}.html", Guid.NewGuid()));
            System.IO.File.WriteAllText(footerPath, footerHtml);
            var pdfDocument = new HtmlToPdfDocument
            {
                GlobalSettings =
                                {
                                 ProduceOutline = true,
                                 PageOffset = 0,
                                 Orientation = paperOrientation,
                                 PaperSize = PaperKind.A4,
                                 OutputFormat = TuesPechkin.GlobalSettings.DocumentOutputFormat.PDF,
                                 Margins =
                                          {
                                             Bottom=1,
                                             Left=1,
                                             Right =1,
                                             Top=2,
                                             Unit = Unit.Centimeters
                                          }
                                },
                Objects =       {
                                 new ObjectSettings
                                         {
                                             HtmlText = html,
                                             HeaderSettings = string.IsNullOrEmpty(headerPath) ? null : new HeaderSettings{ HtmlUrl = headerPath },
                                             FooterSettings = new FooterSettings{ HtmlUrl = string.IsNullOrEmpty(footerPath) ? null : footerPath, RightText = "[page]"},
                                             CountPages = true,
                                             ProduceExternalLinks = true
                                         }
                                }
            };
            try
            {
                buf = TuesPechkinInitializerService.converter.Convert(pdfDocument);
                using (var fs = new FileStream(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path), FileMode.Create, FileAccess.Write))
                {
                    fs.Write(buf, 0, buf.Length);
                }
            }
            catch (Exception ex)
            {
                result = false; ;
            }
            buf = null;
            pdfDocument = null;
            return result ? $"{Request.Url.Scheme}://{Request.Url.Authority}/{path}" : string.Empty;
        }
    }
}