using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GetRandomUsersAPI.Controllers
{
    [Route("api/RandomUser")]
    public class RandomUsersController : Controller
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private readonly string _randomUserUrl = "https://randomuser.me/api/";
        //public IActionResult Index()
        //{
        //    return View();
        //}

        //This is the method to check if the service is up and running "http://localhost:5296/api/RandomUser".
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            return Ok(200);
        }

        [HttpGet("GetUser")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetUser()
        {
            var response = await _httpClient.GetAsync(_randomUserUrl);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            var userData = JsonConvert.DeserializeObject<object>(responseBody);
            return Ok(userData);
        }
    }
}
