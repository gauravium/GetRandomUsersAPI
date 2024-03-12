using System.Collections.Generic;
using GetRandomUsersAPI.Models;

namespace GetRandomUsersAPI.Data
{
    public interface IUserRepository
    {
        Task Authenticate(string username, string password);
        Task<List<User>> GetUserNames();
    }
}
