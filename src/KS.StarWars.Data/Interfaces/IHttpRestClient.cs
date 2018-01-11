using System.Net;

namespace KS.StarWars.Data.Interfaces
{
    public interface IHttpRestClient
    {
        HttpWebResponse Get(string resource, params object[] parameters);
    }

    /// <summary>
    /// Method HTTP called on the request.
    /// </summary>
    public enum RequestMethod
    {
        /// <summary>
        /// GET
        /// </summary>
        GET,
    }

    /// <summary>
    /// Converts the enum to the proper string value
    /// </summary>
    public static class RequestMethodExtension
    {
        /// <summary>
        /// Returns the enum value as string.
        /// </summary>
        /// <param name="value">enum item</param>
        /// <returns>enum value as string.</returns>
        public static string ToCode(this RequestMethod value)
        {
            switch (value)
            {
                case RequestMethod.GET: return nameof(RequestMethod.GET);
                default: return string.Empty;
            }
        }
    }
}
