using MovieApplicationSprint.Entities;
using System.Collections.Generic;

namespace MovieApplicationSprint.Repositories
{
    public interface IUserRepository
    {
        void AddUser(User user);
        User ValidateUser(string email, string password);
        void UpdateUser(User user);
        User GetUserById(string userId);
        List<User> GetAllUsers();
    }
}
