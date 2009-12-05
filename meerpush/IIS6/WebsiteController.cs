using System;
using System.Data;
using System.DirectoryServices;

namespace MeerPush.IIS6
{
    public class WebsiteController : IIS6Manager, IWebsiteController
    {
        string CREATE_SITE_METHOD_NAME = "CreateNewSite";

        public WebsiteController()
        {}

        public WebsiteController(Website website) : base(website)
        {}

        public int Create()
        {
            try
            {
                int websiteId = CreateWebsite(GetIISSiteEntryName());

                DirectoryEntry website = GetWebsite();
                SetSecurity(website);
                SetFriendlyName(website);
                SetASPnetVersion(website);

                Site.WebsiteId = websiteId;

                return websiteId;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        private int CreateWebsite(object[] newsite)
        {
            DirectoryEntry admin = GetIISAdmin();
            return (int)admin.Invoke(CREATE_SITE_METHOD_NAME, newsite);
        }

        private void SetSecurity(DirectoryEntry website)
        {
            website.Properties["AppPoolId"][0] = Site.AppPool;
            website.Properties["AccessFlags"][0] = 512;
            website.Properties["AccessRead"][0] = true;
            website.Properties["AuthFlags"][0] = 5;
            website.CommitChanges();
        }

        private void SetFriendlyName(DirectoryEntry website)
        {
            website.Properties["AppFriendlyName"][0] = website.Name;
            website.CommitChanges();
        }

        private void SetASPnetVersion(DirectoryEntry website)
        {
            // upgrade Scriptmap to 2.0.50272 (ASP.NET version)
            for (int prop = 0; prop < website.Properties["ScriptMaps"].Count; prop++)
            {
                website.Properties["ScriptMaps"][prop] = website.Properties["ScriptMaps"][prop].ToString().Replace("v1.1.4322", "v2.0.50727");
            }

            website.CommitChanges();
        }

        public void Start()
        {
            DirectoryEntry website = GetWebsite();
            website.Invoke("Start", null);
        }

        public bool Exists()
        {
            bool result = false;

            DirectoryEntry admin = GetIISAdmin();

            foreach (DirectoryEntry site in admin.Children)
            {
                PropertyValueCollection serverComment = site.Properties["ServerComment"];
                if (serverComment == null) 
                    continue;

                if (serverComment.Value == null || string.IsNullOrEmpty(serverComment.Value.ToString()))
                    continue;

                if (string.Compare(serverComment.Value.ToString(), Site.Name, false) == 0)
                {
                    result = true;
                    break;
                }
            }

            return result;
        }

        public void Delete()
        {
            if (Exists())
            {
                DirectoryEntry root = new DirectoryEntry("IIS://localhost/w3svc/" + GetWebsite().Name);
                root.DeleteTree();
            }
        }

        public void DumpIISInfo(DirectoryEntry entry)
        {
            if(entry == null) entry = new DirectoryEntry("IIS://localhost/w3svc");

            foreach (DirectoryEntry childEntry in entry.Children)
            {
                using (childEntry)
                {
                    Console.WriteLine(string.Format("Child name [{0}]", childEntry.Name));
                    foreach (PropertyValueCollection property in childEntry.Properties)
                    {
                        Console.WriteLine(string.Format("[{0}] [{1}] [{2}]", childEntry.Name, property.PropertyName, property.Value));
                    }
                    if (childEntry.Children != null)
                    {
                        this.DumpIISInfo(childEntry);
                    }
                }
            }
        }
    }
}