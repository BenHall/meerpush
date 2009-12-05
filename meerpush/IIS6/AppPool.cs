using System;
using System.DirectoryServices;

namespace MeerIIS.IIS6
{
    public class AppPool : IAppPool
    {
        public string Server { get; set; }

        public AppPool(string server)
        {
            Server = server;
        }

        public void Create(string name)
        {
            DirectoryEntry appPools = new DirectoryEntry(string.Format("IIS://{0}/w3svc/AppPools", Server));
            DirectoryEntry appPool = appPools.Children.Add(name, "IISApplicationPool");

            appPool.InvokeSet("AppPoolId", new Object[] { name });
            appPool.InvokeSet("AppPoolIdentityType", new Object[] { 0 });

            appPool.Properties["AppPoolQueueLength"].Value = 4000;
            appPool.Invoke("SetInfo", null);
            appPool.CommitChanges();

        }

        public bool Exists(string name)
        {
            DirectoryEntry appPools = new DirectoryEntry(string.Format("IIS://{0}/w3svc/AppPools", Server));
            bool found = false;
            foreach (DirectoryEntry appPool in appPools.Children)
            {
                if (name.Equals(appPool.Name, StringComparison.OrdinalIgnoreCase))
                {
                    appPool.DeleteTree();
                    found = true;
                    break;
                }
            }

            return found;
        }

        public bool Delete(string name)
        {
            DirectoryEntry appPools = new DirectoryEntry(string.Format("IIS://{0}/w3svc/AppPools", Server));
            bool status = false;
            foreach (DirectoryEntry appPool in appPools.Children)
            {
                if (name.Equals(appPool.Name, StringComparison.OrdinalIgnoreCase))
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