using Microsoft.AspNetCore.Http;

namespace DashboardMVC.DTOs
{
    public class PostProfileDto
    {
        public string FullName { get; set; }
        public IFormFile avatar { get; set; }
    }
}