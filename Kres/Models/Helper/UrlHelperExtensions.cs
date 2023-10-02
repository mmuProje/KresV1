using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kres.Models.Helper
{
    public static class UrlHelperExtensions
    {
        public static string ContentVersioned(this UrlHelper self, string contentPath, HttpRequest request)
        {
            string url = request.Url.ToString().ToLower();
            if (url.Contains("localhost"))
            {
                return self.Content(contentPath);
            }
            else
            {
                DateTime dateDate = DateTime.Now;

                string versionedContentPath = contentPath + "?v=" + GetFileLastModifiedDate(contentPath, request);
                return self.Content(versionedContentPath);
            }

        }

        private static string GetFileLastModifiedDate(string fname, HttpRequest request)
        {
            var localPath = request.RequestContext.HttpContext.Server.MapPath(fname.Replace('/', '\\'));
            DateTime lastModified = File.GetLastWriteTime(localPath);

            return lastModified.ToString("yyyyMMddHHmm");
        }
    }
}