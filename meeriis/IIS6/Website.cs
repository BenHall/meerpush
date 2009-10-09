﻿using System;
using System.DirectoryServices;
using Meeriis;

namespace MeerIIS.IIS6
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
            DirectoryEntry w3svc = new DirectoryEntry(string.Format("IIS://{0}/w3svc", Server));
          
            object[] newsite = new object[] { Name, new object[] { string.Format(":{0}:", Port) }, Home };
            int websiteId = (int)w3svc.Invoke("CreateNewSite", newsite);

            DirectoryEntry website = GetWebsite(Server, websiteId);
            SetSecurity(website);
            SetFriendlyName(website);
            SetASPnetVersion(website);
            StartWebsite(website);

            return websiteId;
        }

        private void SetSecurity(DirectoryEntry website)
        {
            website.Properties["AppPoolId"][0] = "DefaultAppPool";
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

        private DirectoryEntry GetWebsite(string server, int websiteId)
        {
            return new DirectoryEntry(string.Format("IIS://{0}/w3svc/{1}", server, websiteId));
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

            DirectoryEntry w3svc = new DirectoryEntry(string.Format("IIS://{0}/w3svc", Server));

            foreach (DirectoryEntry site in w3svc.Children)
            {
                if (string.Compare(site.Properties["ServerComment"].Value.ToString(), Name, false) == 0)
                {
                    result = true;
                }
            }

            return result;
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }
    }
}