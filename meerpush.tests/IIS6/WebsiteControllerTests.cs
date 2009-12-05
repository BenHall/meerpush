using System.IO;
using MeerPush.IIS6;
using Xunit;

namespace MeerPush.tests.IIS6
{
    public class WebsiteControllerTests
    {
        string test_site;

        [Fact]
        public void Can_get_if_website_exists()
        {
            WebsiteController websiteController = new WebsiteController(CreateSiteObject());

            bool exist = websiteController.Exist();
            Assert.False(exist);
        }

        public WebsiteControllerTests()
        {
            //TODO
            string codeBase = @"C:\sourcecode\MeerPush\MeerPush.tests";
            test_site = Path.Combine(codeBase, "test_site");
        }

        [Fact]
        public void Can_create_iis_website_given_name()
        {
            WebsiteController websiteController = new WebsiteController(CreateSiteObject());

            int websiteId = websiteController.Create();
            Assert.True(websiteId > 0);

            string html = Helper.GetSite("http://localhost:8888/");

            Assert.Contains("Hello World", html);
        }

        [Fact]
        public void Can_delete_website()
        {
            WebsiteController websiteController = new WebsiteController(CreateSiteObject());

            websiteController.Delete();
            Assert.False(websiteController.Exist());
        }

        private Website CreateSiteObject()
        {
            Website site = new Website();
            site.Server = "localhost";
            site.Name = "test_site";
            site.Home = test_site;
            site.Port = 8888;
            return site;
        }
    }
}