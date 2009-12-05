using System;
using Microsoft.Web.Administration;

namespace MeerPush.IIS7
{
    public class AppPoolController : IAppPool
    {
        public Website Site { get; set; }

        public AppPoolController()
        {
            
        }

        public AppPoolController(Website site)
        {
            Site = site;
        }

        public void Create()
        {
            ServerManager serverManager = ServerManager.OpenRemote(Site.Server);
            serverManager.ApplicationPools.Add(Site.AppPool.Name);
            serverManager.CommitChanges();
        }

        public bool Exists()
        {
            ServerManager serverManager = ServerManager.OpenRemote(Site.Server);
            ApplicationPool applicationPool = serverManager.ApplicationPools[Site.AppPool.Name];
            return applicationPool != null;
        }

        public void Delete()
        {
            ServerManager serverManager = ServerManager.OpenRemote(Site.Server);
            ApplicationPool applicationPool = serverManager.ApplicationPools[Site.AppPool.Name];
            applicationPool.Delete();
            serverManager.CommitChanges();
        }
    }
}
