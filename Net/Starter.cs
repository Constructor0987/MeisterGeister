#if NET
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace Nancy
{
    public sealed class Response
    {
        internal Stream Body { get; set; }
        internal string ContentType { get; set; }
    }

    public sealed class ResponseFactory
    {
        public Response FromStream(Stream stream, string contentType)
        {
            return new Response { Body = stream, ContentType = contentType };
        }

        public Response AsText(string text, string contentType)
        {
            return FromStream(new MemoryStream(Encoding.UTF8.GetBytes(text)), contentType);
        }

        public Response AsJson(object value)
        {
            return AsText(JsonConvert.SerializeObject(value), "application/json");
        }
    }

    public sealed class Request
    {
        internal Request(NameValueCollection query) { Query = query; }
        public NameValueCollection Query { get; }
    }

    public abstract class NancyModule
    {
        protected IDictionary<string, Func<dynamic, Response>> Get { get; } =
            new Dictionary<string, Func<dynamic, Response>>(StringComparer.OrdinalIgnoreCase);
        protected ResponseFactory Response { get; } = new ResponseFactory();
        protected Request Request { get; private set; }

        internal Response Dispatch(string path, NameValueCollection query)
        {
            if (!Get.TryGetValue(path, out var handler))
                return null;
            Request = new Request(query);
            return handler(null);
        }
    }
}
#else
using Owin;

namespace MeisterGeister.Net.Web
{
    class Starter
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseNancy();
        }
    }
}
#endif
