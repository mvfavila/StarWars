using System.IO;
using System.Net;
using System.Text;

namespace KS.StarWars.Data.HttpRest
{
    internal static class HttpRestClientHelper
    {
        internal static string GetResultStreamAsString(HttpWebResponse response)
        {
            var encoding = Encoding.UTF8;

            if (!string.IsNullOrEmpty(response.CharacterSet))
                encoding = Encoding.GetEncoding(response.CharacterSet);

            using (var reader = new StreamReader(response.GetResponseStream(), encoding))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
