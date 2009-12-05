using MeerPush.IIS6;
using Xunit;

namespace MeerPush.tests.IIS6
{
    public class AppPoolControllerTests
    {
        [Fact]
        public void Can_create_app_pool()
        {
            AppPoolController poolController = new AppPoolController(new Website { Server = "localhost", AppPool = new AppPool { Name = "MasterAppPool" } });
            poolController.Create();

            Assert.True(poolController.Exists());
        }

        [Fact]
        public void Exists_returns_false_if_not_created()
        {
            AppPoolController poolController = new AppPoolController(new Website { Server = "localhost", AppPool = new AppPool { Name = "MasterAppPool" } });
            Assert.False(poolController.Exists());
        }

        [Fact]
        public void Can_Delete_AppPool()
        {
            AppPoolController poolController = new AppPoolController(new Website { Server = "localhost", AppPool = new AppPool { Name = "MasterAppPool" } });
            bool deleted = poolController.Delete();

            Assert.True(deleted);
        }
    }
}
