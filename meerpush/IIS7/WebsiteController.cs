using System;
using Microsoft.Web.Administration;

namespace MeerPush.IIS7
{
    public class WebsiteController : IWebsiteController
    {
        public Website Site { get; set; }

        public WebsiteController()
        {
            Site = new Website();
        }

        public WebsiteController(Website site)
        {
            Site = site;
        }

        public int Create()
        {
            try
            {
                Site iisSite;
                using (ServerManager serverManager = ServerManager.OpenRemote(Site.Server))
                {
                    iisSite = serverManager.Sites.Add(Site.Name, Site.Home, Site.Port);
                    serverManager.CommitChanges();
                }

                return (int)iisSite.Id;
            }
            catch (Exception) { return -1; }
        }

        public void Start()
        {
            try
            {
                using (ServerManager serverManager = ServerManager.OpenRemote(Site.Server))
                {
                    Site iisSite = serverManager.Sites[Site.Name];
                    if (iisSite != null)
                        iisSite.Start();
                }
            }
            catch (Exception) {}
        }

        public bool Exists()
        {
            Site iisSite;
            using (ServerManager serverManager = ServerManager.OpenRemote(Site.Server))
            {
                iisSite = serverManager.Sites[Site.Name];
            }
            return iisSite != null;
        }

        public void Delete()
        {
            if (Exists())
            {
                using (ServerManager serverManager = ServerManager.OpenRemote(Site.Server))
                {
                    serverManager.Sites[Site.Name].Delete();
                    serverManager.CommitChanges();
                }
            }
        }
    }
}
