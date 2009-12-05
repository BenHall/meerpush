using Microsoft.Web.Administration;

namespace MeerPush.IIS7
{
    public class WebsiteController : IWebsiteController
    {
        public Website Site { get; set; }

        public WebsiteController()
        {
            
        }

        public WebsiteController(Website site)
        {
            Site = site;
        }

        public int Create()
        {
            ServerManager serverManager = ServerManager.OpenRemote(Site.Server);
            Site iisSite = serverManager.Sites.Add(Site.Name, Site.Home, Site.Port);
            serverManager.CommitChanges();

            return (int)iisSite.Id;
        }

        public void Start()
        {
            ServerManager serverManager = ServerManager.OpenRemote(Site.Server);
            Site iisSite = serverManager.Sites[Site.Name];
            iisSite.Start();
        }

        public bool Exists()
        {
            ServerManager serverManager = ServerManager.OpenRemote(Site.Server);
            Site iisSite = serverManager.Sites[Site.Name];
            return iisSite != null;
        }

        public void Delete()
        {
            ServerManager serverManager = ServerManager.OpenRemote(Site.Server);
            serverManager.Sites[Site.Name].Delete();
            serverManager.CommitChanges();
        }
    }
}
