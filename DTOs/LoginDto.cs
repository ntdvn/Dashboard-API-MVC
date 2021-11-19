using System.ComponentModel.DataAnnotations;

namespace DashboardMVC.DTOs
{
    public class LoginDto
    {
        public LoginDto()
        {
        }

        [Required(ErrorMessage = "login_required_username")]
        public string Username { get; set; }
        [Required(ErrorMessage = "login_required_password")]
        public string Password { get; set; }
    }
}