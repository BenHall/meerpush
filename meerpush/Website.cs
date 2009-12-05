namespace MeerPush
{
    public class Website
    {
        public string Server { get; set; }
        public string Name { get; set; }
        public string Home { get; set; }
        public string HostHeader { get; set; }
        public AppPool AppPool { get; set; }

        private int _port = 80;
        public int Port { get { return _port; } set { _port = value; } }

        
    }
}