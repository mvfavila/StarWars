using AutoMoq;
using Moq;
using System.IO;
using System.Net;
using System.Text;

namespace KS.StarWars.Data.Tests.Repositories
{
    internal class TestHelper
    {
        private readonly AutoMoqer mocker;

        public TestHelper()
        {
            mocker = new AutoMoqer();
        }

        internal static HttpWebResponse MockWebResponse(string content)
        {
            var responseData = Encoding.UTF8.GetBytes(content);
            Stream stream = new MemoryStream(responseData);

            var response = new Mock<HttpWebResponse>();
            response.Setup(r => r.GetResponseStream()).Returns(stream);
            response.Setup(r => r.CharacterSet).Returns("UTF-8");

            return response.Object;
        }
    }
}
