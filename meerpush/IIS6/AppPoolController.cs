using System;
using System.DirectoryServices;

namespace MeerPush.IIS6
{
    public class AppPoolController : IIS6, IAppPool
    {
        public AppPoolController(Website website) : base(website)
        {}

        public void Create()
        {
            DirectoryEntry appPools = new DirectoryEntry(string.Format("IIS://{0}/w3svc/AppPools", Site.Server));
            DirectoryEntry appPool = appPools.Children.Add(Site.Name, "IISApplicationPool");

            appPool.InvokeSet("AppPoolId", new Object[] { Site.Name });
            appPool.InvokeSet("AppPoolIdentityType", new Object[] { 0 });

            appPool.Properties["AppPoolQueueLength"].Value = 4000;
            appPool.Invoke("SetInfo", null);
            appPool.CommitChanges();

        }

        public bool Exists()
        {
            DirectoryEntry appPools = new DirectoryEntry(string.Format("IIS://{0}/w3svc/AppPools", Site.Server));
            bool found = false;
            foreach (DirectoryEntry appPool in appPools.Children)
            {
                if (Site.Name.Equals(appPool.Name, StringComparison.OrdinalIgnoreCase))
                {
                    appPool.DeleteTree();
                    found = true;
                    break;
                }
            }

            return found;
        }

        public bool Delete()
        {
            DirectoryEntry appPools = new DirectoryEntry(string.Format("IIS://{0}/w3svc/AppPools", Site.Server));
            bool status = false;
            foreach (DirectoryEntry appPool in appPools.Children)
            {
                if (Site.Name.Equals(appPool.Name, StringComparison.OrdinalIgnoreCase))
                {
                    appPool.DeleteTree();
                    status = true;
                    break;
                }
            }

            return status;
        }
    }
}