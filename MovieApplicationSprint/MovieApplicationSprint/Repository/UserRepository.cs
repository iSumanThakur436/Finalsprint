using MovieApplicationSprint.Entities;
using System.Collections.Generic;
using System.Linq;

namespace MovieApplicationSprint.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MovieAppContext _context;

        public UserRepository()
        {
            _context = new MovieAppContext();
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public User ValidateUser(string email, string password)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
        }

        public void UpdateUser(User user)
        {
            var existing = _context.Users.Find(user.UserId);
            if (existing != null)
            {
                existing.UserName = user.UserName;
                existing.Age = user.Age;
                existing.Gender = user.Gender;
                existing.Email = user.Email;
                existing.Mobile = user.Mobile;
                existing.Password = user.Password;
                existing.UserType = user.UserType;
                _context.SaveChanges();
            }
        }

        public User GetUserById(string userId)
        {
            return _context.Users.Find(userId);
        }

        public List<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }
    }
}
