using System;
using System.IO;
using MeerPush.IIS7;
using Xunit;

namespace MeerPush.tests.IIS7
{
    public class WebsiteTests : IDisposable 
    {
        string test_site;

        public WebsiteTests()
        {
            //TODO
            string codeBase = @"D:\SourceControl\MeerPush\MeerPush.tests\";
            test_site = Path.Combine(codeBase, "test_site");
        }

        [Fact]
        public void Can_create_iis_website_given_name()
        {
            WebsiteController websiteController = new WebsiteController(new Website{Server = "localhost", Name = "test_site", Home = test_site, Port = 8888});

            int websiteId = websiteController.Create();
            Assert.True(websiteId > 0);

            string html = Helper.GetSite("http://localhost:8888/");

            Assert.Contains("Hello World", html);
        }

        [Fact]
        public void Can_tell_if_website_exists()
        {
            WebsiteController websiteController = new WebsiteController(new Website{Name = "test_site", Server = "localhost"});

            bool exist = websiteController.Exists();
            Assert.False(exist);
        }

        [Fact]
        public void Website_exists_after_creating()
        {
            WebsiteController websiteController = new WebsiteController(new Website { Name = "test_site", Server = "localhost" });

            websiteController.Create();
            bool exist = websiteController.Exists();
            Assert.True(exist);
        }

        [Fact]
        public void Can_delete_website()
        {
            WebsiteController websiteController = new WebsiteController(new Website { Name = "test_site", Server = "localhost" });

            websiteController.Delete();
            Assert.False(websiteController.Exists());
        }

        public void Dispose()
        {
            WebsiteController websiteController = new WebsiteController(new Website { Name = "test_site"});

            websiteController.Delete();

            Assert.False(websiteController.Exists());
        }
    }
}
