using GetRandomUsersAPI.Models;
using System.Collections.Generic;

namespace GetRandomUsersAPI.Data
{
    public class UserRepository
    {
        private static List<User> _users = new List<User>
        {
            new User
            {
                Id = 1, Username = "gaurav", Password = "gaurav123"
            },
            new User
            {
                Id = 2, Username = "amitabh", Password = "amitabh123"
            },
            new User
            {
                Id = 3, Username = "shahrukh", Password = "shahrukh123"
            }
        };
        public async Task<bool> Authenticate(string username, string password)
        {
            return await Task.FromResult(_users.Any(x => x.Username == username && x.Password == password));
        }
        public static List<User> GetUsers()
        {
            List<User> users = new List<User>();
            foreach (var user in _users)
            {
                users.Add(user);
            }
            return users;
        }
    }
}
