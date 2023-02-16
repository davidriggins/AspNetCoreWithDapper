using Adonet_Blog.Entities;

namespace Adonet_Blog.Models
{
    public class LoginModel
    {
        public LoginModel()
        {
            user = new User();
            Success = false;
            Message = string.Empty;
        }

        public User user { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
