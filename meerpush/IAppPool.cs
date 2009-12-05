namespace MeerPush
{
    public interface IAppPool
    {
        void Create();
        bool Exists();
        bool Delete();
    }
}