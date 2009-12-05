using MeerPush.IIS6;
using Xunit;

namespace MeerPush.tests.IIS6
{
    public class AppPoolTests
    {
        [Fact]
        public void Can_create_app_pool()
        {
            AppPool pool = new AppPool("localhost");
            pool.Create("MasterAppPool");

            Assert.True(pool.Exists("MasterAppPool"));
        }

        [Fact]
        public void Exists_returns_false_if_not_created()
        {
            AppPool pool = new AppPool("localhost");
            Assert.False(pool.Exists("MasterAppPool"));
        }

        [Fact]
        public void Can_Delete_AppPool()
        {
            AppPool pool = new AppPool("localhost");
            bool deleted = pool.Delete("MasterAppPool");

            Assert.True(deleted);
        }
    }
}
