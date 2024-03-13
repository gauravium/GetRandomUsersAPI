using GetRandomUsersAPI.Controllers;
using GetRandomUsersAPI.Data;
using GetRandomUsersAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Text;

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
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["Authorization"] = "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes("gaurav:gaurav123"));
            _randomUsersController.ControllerContext = new ControllerContext { HttpContext = httpContext };

            var result = await _randomUsersController.GetUser();

            //Assert
            Assert.NotNull(result);
            var okResult = Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void GetJsonObject_Returns_UnauthorizedObj()
        {
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["Authorization"] = "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes("gaurav:wrongpassword"));
            _randomUsersController.ControllerContext = new ControllerContext { HttpContext = httpContext };

            var result = await _randomUsersController.GetUser();

            //Assert
            var unauthorizedResult = Assert.IsType<UnauthorizedResult>(result);
        }
    }
}