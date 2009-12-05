namespace MeerPush
{
    public interface IAppPool
    {
        void Create();
        bool Exists();
        void Delete();
    }
}