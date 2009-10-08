using System;
using System.IO;
using MeerIIS.IIS7;
using Xunit;

namespace MeerIIS.tests.IIS7
{
    public class WebsiteTests : IDisposable 
    {
        string test_site;

        public WebsiteTests()
        {
            //TODO
            string codeBase = @"D:\SourceControl\meeriis\meeriis.tests\";
            test_site = Path.Combine(codeBase, "test_site");
        }

        [Fact]
        public void Can_create_iis_website_given_name()
        {
            Website website = new Website("localhost");
            int websiteId = website.Create("test_site", test_site, 8888);
            Assert.True(websiteId > 0);

            string html = Helper.GetSite("http://localhost:8888/");

            Assert.Contains("Hello World", html);
        }

        [Fact]
        public void Can_tell_if_website_exists()
        {
            Website website = new Website("localhost");
            bool exist = website.Exist("test_site");
            Assert.False(exist);
        }

        [Fact]
        public void Website_exists_after_creating()
        {
            Website website = new Website("localhost");
            website.Create("test_site", test_site, 8888);
            bool exist = website.Exist("test_site");
            Assert.True(exist);
        }

        [Fact]
        public void Can_delete_website()
        {
            Website website = new Website("localhost");
            website.Delete("test_site");
            Assert.False(website.Exist("test_site"));
        }

        public void Dispose()
        {
            Website website = new Website("localhost");
            website.Delete("test_site");
        }
    }
}
