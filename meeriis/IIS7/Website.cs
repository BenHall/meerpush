using System;
using Meeriis;
using Microsoft.Web.Administration;

namespace MeerIIS.IIS7
{
    public class Website : IWebsite
    {
        public string Server { get; set; }

        public Website(string server)
        {
            Server = server;
        }

        public int Create(string name, string homeDirectory)
        {
            return Create(name, homeDirectory, 80);
        }

        public int Create(string name, string homeDirectory, int port)
        {
            ServerManager serverManager = ServerManager.OpenRemote(Server);
            Site site = serverManager.Sites.Add(name, homeDirectory, port);
            serverManager.CommitChanges();

            return (int) site.Id;
        }

        public bool Exist(string name)
        {
            ServerManager serverManager = ServerManager.OpenRemote(Server);
            Site site = serverManager.Sites[name];
            return site != null;
        }

        public void Delete(string name)
        {
            ServerManager serverManager = ServerManager.OpenRemote(Server);
            serverManager.Sites[name].Delete();
            serverManager.CommitChanges();
        }
    }
}
