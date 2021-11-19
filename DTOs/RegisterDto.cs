using System.ComponentModel.DataAnnotations;

namespace DashboardMVC.DTOs
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "register_required_username")]
        public string Username { get; set; }
        [Required(ErrorMessage = "register_required_password")]
        [StringLength(20, MinimumLength = 4)]
        public string Password { get; set; }

        [Required(ErrorMessage = "register_required_full_name")]
        public string FullName { get; set; }
    }
}