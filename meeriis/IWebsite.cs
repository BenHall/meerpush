namespace Meeriis
{
    public interface IWebsite
    {
        int Create(string name, string homeDirectory);
        int Create(string name, string homeDirectory, int port);
        bool Exist(string websiteName);
    }
}