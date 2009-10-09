using Meeriis;
using Microsoft.Web.Administration;

namespace MeerIIS.IIS7
{
    public class Website : IWebsite
    {
        public string Server { get; set; }
        public string Name { get; set; }
        public string Home { get; set; }

        private int _port = 80;
        public int Port { get { return _port; } set { _port = value; } }

        public int Create()
        {
            ServerManager serverManager = ServerManager.OpenRemote(Server);
            Site site = serverManager.Sites.Add(Name, Home, Port);
            serverManager.CommitChanges();

            return (int) site.Id;
        }

        public bool Exist()
        {
            ServerManager serverManager = ServerManager.OpenRemote(Server);
            Site site = serverManager.Sites[Name];
            return site != null;
        }

        public void Delete()
        {
            ServerManager serverManager = ServerManager.OpenRemote(Server);
            serverManager.Sites[Name].Delete();
            serverManager.CommitChanges();
        }
    }
}
