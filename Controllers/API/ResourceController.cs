using System;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace DashboardMVC.Controllers.API
{
    [Authorize]
    public class ResourceController : BaseApiController
    {
        private readonly IHostingEnvironment _environment;
        public ResourceController(IHostingEnvironment environment)
        {
            this._environment = environment;
        }

        [HttpGet("images/{imagePath}")]
        public IActionResult ReadImage(string imagePath)
        {
            var uploadPath = Path.Combine(_environment.WebRootPath, "images");
            Byte[] b = System.IO.File.ReadAllBytes(Path.Combine(uploadPath, imagePath));   // 
            return File(b, "image/jpeg");
        }
    }
}