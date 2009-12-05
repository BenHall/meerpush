using MeerPush.IIS6;
using Xunit;

namespace MeerPush.tests.IIS6
{
    public class AppPoolControllerTests
    {
        [Fact]
        public void Can_create_app_pool()
        {
            AppPoolController poolControllerController = new AppPoolController(new Website { Server = "localhost", AppPool = new AppPool { Name = "MasterAppPool" } });
            poolControllerController.Create();

            Assert.True(poolControllerController.Exists());
        }

        [Fact]
        public void Exists_returns_false_if_not_created()
        {
            AppPoolController poolControllerController = new AppPoolController(new Website { Server = "localhost", AppPool = new AppPool { Name = "MasterAppPool" } });
            Assert.False(poolControllerController.Exists());
        }

        [Fact]
        public void Can_Delete_AppPool()
        {
            AppPoolController poolControllerController = new AppPoolController(new Website { Server = "localhost", AppPool = new AppPool { Name = "MasterAppPool" } });
            Assert.DoesNotThrow(poolControllerController.Delete);
        }
    }
}
