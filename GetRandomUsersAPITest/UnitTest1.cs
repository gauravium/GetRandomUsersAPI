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
            //var userRepositoryMock = new Mock<UserRepository>();
            //userRepositoryMock.Setup(UserRepository.GetUsers()).Returns(new List<User> { new User { Username = "test", Password = "password" } });

            var httpClientMock = new Mock<HttpClient>();
            httpClientMock.Setup(client => client.GetAsync(It.IsAny<string>())).ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("{'name':'John','age':30}", Encoding.UTF8, "application/json")
            });

            var httpContextMock = new Mock<HttpContext>();
            httpContextMock.SetupGet(ctx => ctx.Request.Headers["Authorization"]).Returns("Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes("gaurav:gaurav123")));

            var controller = new RandomUsersController()
            {
                ControllerContext = new ControllerContext { HttpContext = httpContextMock.Object }
            };
            var result = await _randomUsersController.GetUser();

            //Assert
            Assert.NotNull(result);
            var okResult = Assert.IsType<OkObjectResult>(result);
        }
    }
}