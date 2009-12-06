using System;
using System.DirectoryServices;

namespace MeerPush.IIS6
{
    public class AppPoolController : IIS6Manager, IAppPool
    {
        public AppPoolController(Website website) : base(website)
        {}

        public void Create()
        {
            DirectoryEntry appPools = GetAppPoolAdmin();
            DirectoryEntry appPool = appPools.Children.Add(Site.Name, "IISApplicationPool");

            appPool.InvokeSet("AppPoolId", new Object[] { Site.Name });
            appPool.InvokeSet("AppPoolIdentityType", new Object[] { 0 });

            appPool.Properties["AppPoolQueueLength"].Value = 4000;
            appPool.Invoke("SetInfo", null);
            appPool.CommitChanges();

        }

        public bool Exists()
        {
            DirectoryEntry appPools = GetAppPoolAdmin();
            bool found = false;
            foreach (DirectoryEntry appPool in appPools.Children)
            {
                if (Site.Name.Equals(appPool.Name, StringComparison.OrdinalIgnoreCase))
                {
                    found = true;
                    break;
                }
            }

            return found;
        }

        public void Delete()
        {
            DirectoryEntry appPools = GetAppPoolAdmin();
            foreach (DirectoryEntry appPool in appPools.Children)
            {
                if (Site.Name.Equals(appPool.Name, StringComparison.OrdinalIgnoreCase))
                {
                    appPool.DeleteTree();
                    break;
                }
            }
        }
    }
}