using System;

namespace DashboardMVC.DTOs
{
    public class UserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Gender { get; set; }
        public string Token { get; set; }

        public DateTime Created { get; set; } = DateTime.Now;
    }
}