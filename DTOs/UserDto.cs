using System;

namespace DashboardMVC.DTOs
{
    public class UserDto
    {
        public string Username { get; set; }
        public string FullName { get; set; }
        public string Token { get; set; }

        public DateTime Created { get; set; } = DateTime.Now;
    }
}