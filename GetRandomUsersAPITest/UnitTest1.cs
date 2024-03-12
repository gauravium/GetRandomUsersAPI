using GetRandomUsersAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace GetRandomUsersAPITest
{
    public class UnitTest1
    {
        private readonly RandomUsersController _randomUsersController;

        public UnitTest1()
        {
            _randomUsersController = new RandomUsersController();
        }

        [Fact]
        public void Get_WhenCalled_ReturnsOkResult()
        {
            var okResult = _randomUsersController.Get();
            // Assert
            Assert.IsType<OkObjectResult>(okResult as OkObjectResult);
        }

        [Fact]
        public async void GetJsonObject_Returns_Json()
        {
            var result = await _randomUsersController.GetUser();

            //Assert
            Assert.NotNull(result);
            var okResult = Assert.IsType<OkObjectResult>(result);
        }
    }
}