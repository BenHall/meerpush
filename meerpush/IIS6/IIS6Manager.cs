using System.DirectoryServices;

namespace MeerPush.IIS6
{
    public abstract class IIS6Manager
    {
        public IIS6Manager()
        {
            Site = new Website();
        }
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

        public DirectoryEntry GetWebsite()
        {
            DirectoryEntry admin = GetIISAdmin();

            foreach (DirectoryEntry site in admin.Children)
            {
                PropertyValueCollection serverComment = site.Properties["ServerComment"];
                if (serverComment == null)
                    continue;

                if (serverComment.Value == null || string.IsNullOrEmpty(serverComment.Value.ToString()))
                    continue;

                if (string.Compare(serverComment.Value.ToString(), Site.Name, false) == 0)
                {
                    return site;
                }
            }

            return null;
        }

        protected DirectoryEntry GetAppPoolAdmin()
        {
            return new DirectoryEntry(string.Format("IIS://{0}/w3svc/AppPools", Site.Server));
        }
    }
}