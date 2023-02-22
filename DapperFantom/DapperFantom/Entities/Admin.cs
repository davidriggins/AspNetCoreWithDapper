using System.ComponentModel.DataAnnotations;

namespace DapperFantom.Entities
{
    public class Admin
    {
        [Dapper.Contrib.Extensions.Key]
        public int AdminId { get; set; }

        [Required(ErrorMessage = "Please enter your user name")]
        [MinLength(4, ErrorMessage = "Minumum length of 4 characters")]
        [MaxLength(10, ErrorMessage = "Maximum length of 10 characters")]
        [Display(Name = "Your Username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please enter your password")]
        [MinLength(4, ErrorMessage = "Minumum length of 4 characters")]
        [MaxLength(10, ErrorMessage = "Maximum length of 10 characters")]
        [Display(Name = "Your Password")]
        public string Password { get; set; }
    }
}
