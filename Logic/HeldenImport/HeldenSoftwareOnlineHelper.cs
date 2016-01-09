using MeisterGeister.Logic.Einstellung;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Cache;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MeisterGeister.Logic.HeldenImport
{
    class HeldenSoftwareOnlineHelper
    {
        public static string Post(string token, params KeyValuePair<string, string>[] bodies)
        {
            var builder = new StringBuilder();
            builder.Append("token=");
            builder.Append(WebUtility.HtmlEncode(token));

            foreach (var body in bodies)
            {
                builder.Append("&");
                builder.Append(WebUtility.HtmlEncode(body.Key));
                builder.Append("=");
                builder.Append(WebUtility.HtmlEncode(body.Value));
            }

            var request = CreatePostRequest(builder.ToString());

            WebResponse response = request.GetResponse();
            string result = null;
            using (var stream = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
            {
                result = stream.ReadToEnd();
            }
            return result;
        }

        private static HttpWebRequest CreatePostRequest(string body)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Einstellungen.HeldenSoftwareOnlineURL);
            request.ContentType = "application/x-www-form-urlencoded; charset=utf-8";
            request.ContentLength = body.Length;
            request.Accept = "text/html,application/xml,application/xhtml+xml,text/html;q=0.9,text/plain;q=0.8,image/png,*/*;q=0.5";
            request.Headers.Add(HttpRequestHeader.AcceptCharset, "utf-8");
            request.Method = "POST";

            using (var newStream = request.GetRequestStream())
            {
                var encoding = new UTF8Encoding();
                byte[] content = encoding.GetBytes(body);
                newStream.Write(content, 0, content.Length);
            }

            return request;
        }
    }
}
