namespace MeerIIS.IIS6
{
    public interface IAppPool
    {
        void Create(string name);
        bool Exists(string name);
        bool Delete(string name);
    }
}