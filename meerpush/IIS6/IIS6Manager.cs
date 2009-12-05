using System.DirectoryServices;

namespace MeerPush.IIS6
{
    public abstract class IIS6Manager
    {
        protected IIS6Manager(Website website)
        {
            Site = website;
        }

        public Website Site { get; set; }
  
        internal object[] GetIISSiteEntryName()
        {
            return new object[] { Site.Name, new object[] { string.Format(":{0}:", Site.Port) }, Site.Home };
        }

        internal string GetIISEntry()
        {
            return string.Format("IIS://{0}/w3svc", Site.Server);
        }

        internal DirectoryEntry GetIISAdmin()
        {
            return new DirectoryEntry(GetIISEntry());
        }

        internal DirectoryEntry GetWebsite(string server, int websiteId)
        {
            return new DirectoryEntry(string.Format("IIS://{0}/w3svc/{1}", server, websiteId));
        }

        protected DirectoryEntry GetAppPoolAdmin()
        {
            return new DirectoryEntry(string.Format("IIS://{0}/w3svc/AppPools", Site.Server));
        }
    }
}