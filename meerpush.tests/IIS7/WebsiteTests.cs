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
            Website website = new Website {Server = "localhost", Name = "test_site", Home = test_site, Port = 8888};

            int websiteId = website.Create();
            Assert.True(websiteId > 0);

            string html = Helper.GetSite("http://localhost:8888/");

            Assert.Contains("Hello World", html);
        }

        [Fact]
        public void Can_tell_if_website_exists()
        {
            Website website = new Website {Name = "test_site", Server = "localhost"};

            bool exist = website.Exist();
            Assert.False(exist);
        }

        [Fact]
        public void Website_exists_after_creating()
        {
            Website website = new Website {Server = "localhost", Name = "test_site"};

            website.Create();
            bool exist = website.Exist();
            Assert.True(exist);
        }

        [Fact]
        public void Can_delete_website()
        {
            Website website = new Website {Server = "localhost", Name = "test_site"};

            website.Delete();
            Assert.False(website.Exist());
        }

        public void Dispose()
        {
            Website website = new Website {Name = "test_site"};

            website.Delete();

            Assert.False(website.Exist());
        }
    }
}
