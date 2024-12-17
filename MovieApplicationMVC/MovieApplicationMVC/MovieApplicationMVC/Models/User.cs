using System.ComponentModel.DataAnnotations;

namespace MovieApplicationMVC.Models
{
    public class User
    {
        [Key]
        public string UserId { get; set; }

        public string UserName { get; set; }

        public int Age { get; set; }
        public string Gender { get; set; }


        public string Email { get; set; }


        public string Mobile { get; set; }


        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
        public string UserType { get; set; }
    }
}
