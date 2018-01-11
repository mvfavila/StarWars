using KS.StarWars.Data.Interfaces;
using System;
using System.Net;
using System.Text;

namespace KS.StarWars.Data.HttpRest
{
    public class HttpRestClient : IHttpRestClient
    {
        private readonly string url;
        private readonly Encoding encoding;
        private const string slash = "/";

        public HttpRestClient(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentNullException(nameof(url));

            this.url = url;
            encoding = Encoding.UTF8;
        }

        public HttpWebResponse Get(string resource, params object[] parameters)
        {
            var fullUrl = GetFullUrl();
            AttachResourceToUrl(fullUrl, resource);
            AttachParametersToUrl(fullUrl, parameters);

            var request = CreateWebRequest(fullUrl.ToString(), RequestMethod.GET);

            return (HttpWebResponse)request.GetResponse();
        }

        private StringBuilder GetFullUrl()
        {
            return new StringBuilder(url);
        }

        private static HttpWebRequest CreateWebRequest(string url, RequestMethod requestMethod)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = requestMethod.ToCode();

            if (request.Proxy != null)
            {
                request.UseDefaultCredentials = true;
                request.Proxy = new WebProxy();
            }

            return request;
        }

        private void AttachResourceToUrl(StringBuilder fullUrl, string resource)
        {
            if (url.LastIndexOf(slash) != url.Length - 1)
                fullUrl.Append(slash);

            if (!string.IsNullOrWhiteSpace(resource))
            {
                fullUrl.Append(resource);
            }
        }

        private static void AttachParametersToUrl(StringBuilder fullUrl, object[] parameters)
        {
            if (parameters != null)
            {
                foreach (object param in parameters)
                {
                    fullUrl.Append(param).Append(slash);
                }

                if (parameters.Length > 0)
                    fullUrl.Remove(fullUrl.Length - 1, 1);
            }
        }
    }
}
