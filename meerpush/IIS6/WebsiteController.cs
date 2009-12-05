using System.DirectoryServices;

namespace MeerPush.IIS6
{
    public class WebsiteControllerController : IIS6Manager, IWebsiteController
    {
        string CREATE_SITE_METHOD_NAME = "CreateNewSite";

        public WebsiteControllerController(Website website) : base(website)
        {}

        public int Create()
        {
            int websiteId = CreateWebsite(GetIISSiteEntryName());

            DirectoryEntry website = GetWebsite(Site.Server, websiteId);
            SetSecurity(website);
            SetFriendlyName(website);
            SetASPnetVersion(website);
            StartWebsite(website);

            return websiteId;
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

        private void StartWebsite(DirectoryEntry website)
        {
            website.Invoke("Start", null);
        }

        public bool Exist()
        {
            bool result = false;

            DirectoryEntry admin = GetIISAdmin();

            foreach (DirectoryEntry site in admin.Children)
            {
                if (string.Compare(site.Properties["ServerComment"].Value.ToString(), Site.Name, false) == 0)
                {
                    result = true;
                }
            }

            return result;
        }

        public void Delete()
        {
            DirectoryEntry admin = GetIISAdmin();

            foreach (System.DirectoryServices.DirectoryEntry vd in admin.Children)
            {
                if(vd.Name == Site.Name)
                {
                    admin.Invoke("Delete", new string[] { vd.SchemaClassName, Site.Name });
                    admin.CommitChanges();
                 break;
                }
            }
        }

    }
}