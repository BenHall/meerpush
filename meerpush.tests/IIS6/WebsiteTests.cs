using System.IO;
using MeerPush.IIS6;
using Xunit;

namespace MeerPush.tests.IIS6
{
    public class WebsiteTests
    {
        string test_site;

        public WebsiteTests()
        {
            //TODO
            string codeBase = @"C:\sourcecode\MeerPush\MeerPush.tests";
            test_site = Path.Combine(codeBase, "test_site");
        }

        [Fact]
        public void Can_create_iis_website_given_name()
        {
            Website website = new Website();
            website.Server = "localhost";
            website.Name = "test_site";
            website.Home = test_site;
            website.Port = 8888;

            int websiteId = website.Create();
            Assert.True(websiteId > 0);

            string html = Helper.GetSite("http://localhost:8888/");

            Assert.Contains("Hello World", html);
        }

        [Fact]
        public void Can_get_if_website_exists()
        {
            Website website = new Website();
            website.Server = "localhost";
            website.Name = "test_site";
            website.Home = test_site;
            website.Port = 8888;

            bool exist = website.Exist();
            Assert.False(exist);
        }

        [Fact]
        public void Can_delete_website()
        {
            Website website = new Website();
            website.Server = "localhost";
            website.Name = "test_site";
            website.Home = test_site;
            website.Port = 8888;

            website.Delete();
            Assert.False(website.Exist());
        }
    }
}