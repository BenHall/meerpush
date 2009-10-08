using System;
using System.IO;
using System.Net;
using MeerIIS.IIS6;
using Xunit;

namespace MeerIIS.tests.IIS6
{
    public class CreateWebsiteTests
    {
        string test_site;

        public CreateWebsiteTests()
        {
            //TODO
            string codeBase = @"C:\sourcecode\meeriis\meeriis.tests";
            test_site = Path.Combine(codeBase, "test_site");
        }

        [Fact]
        public void Can_create_iis_website_given_name()
        {
            Website website = new Website("localhost");
            int websiteId = website.Create("test_site", test_site, 8888);
            Assert.True(websiteId > 0);

            string html = GetSite("http://localhost:8888/");

            Assert.Contains("Hello World", html);
        }

        [Fact]
        public void Can_get_if_website_exists()
        {
            Website website = new Website("localhost");
            bool exist = website.Exist("test_site");
            Assert.False(exist);
        }

        private string GetSite(string uri)
        {
            var request = HttpWebRequest.Create(new Uri(uri));
            var response = request.GetResponse() as HttpWebResponse;
            StreamReader reader = new StreamReader(response.GetResponseStream());

            return reader.ReadToEnd();
        }
    }
}