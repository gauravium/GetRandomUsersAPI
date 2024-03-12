using GetRandomUsersAPI.Data;
using GetRandomUsersAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

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
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetUser()
        {
            string authorizationHeader = HttpContext.Request.Headers["Authorization"];
            if (!string.IsNullOrEmpty(authorizationHeader) && authorizationHeader.StartsWith("Basic"))
            {
                string encodedCredentials = authorizationHeader.Substring("Basic ".Length).Trim();
                string decodedCredentials = Encoding.UTF8.GetString(Convert.FromBase64String(encodedCredentials));
                string[] credentials = decodedCredentials.Split(':', 2);

                string username = credentials[0];
                string password = credentials[1];
                var authenticatedUser = UserRepository.GetUsers().FirstOrDefault(u => u.Username == username && u.Password == password);
                if(authenticatedUser == null)
                {
                    return Unauthorized();
                }
            }
            else
            {
                return BadRequest();
            }

            var response = await _httpClient.GetAsync(_randomUserUrl);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            var userData = JsonConvert.DeserializeObject<object>(responseBody);
            return Ok(userData);
        }
    }
}
