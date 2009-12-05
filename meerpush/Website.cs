namespace MeerPush
{
    public class Website
    {
        public int WebsiteId { get; set; }
        public string Server { get; set; }
        public string Name { get; set; }
        public string Home { get; set; }
        public string HostHeader { get; set; }
        private AppPool _appPool;
        public AppPool AppPool
        {
            get
            {
                if(_appPool == null)
                    _appPool = new AppPool();
                return _appPool;
            }
            set { _appPool = value; }
        }

        private int _port = 80;
        public int Port { get { return _port; } set { _port = value; } }
    }
}