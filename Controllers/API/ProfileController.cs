using System.Linq;
using System;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using DashboardMVC.DTOs;
using DashboardMVC.Entities;
using DashboardMVC.Extensions;
using DashboardMVC.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using DashboardMVC.Helpers;

namespace DashboardMVC.Controllers.API
{
    public class ProfileController : BaseApiController
    {
        public UserManager<ApplicationUser> _userManager { get; set; }
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IHostingEnvironment _environment;
        private readonly ResourceManager _resourceUrlGenerate;
        public ProfileController(IUserRepository userRepository, UserManager<ApplicationUser> userManager, IMapper mapper, IHostingEnvironment environment, ResourceManager resourceUrlGenerate)
        {
            this._resourceUrlGenerate = resourceUrlGenerate;
            this._environment = environment;
            this._mapper = mapper;
            this._userRepository = userRepository;
            this._userManager = userManager;
            if (userManager is null)
            {
                throw new ArgumentNullException(nameof(userManager));
            }
        }

        public async Task<ActionResult<BaseDto>> profile()
        {

            var userId = User.GetUserId();
            var user = await _userRepository.GetUserByIdAsync(Guid.Parse(userId));
            return Ok(new BaseDto
            {
                Status = true,
                Data = user,
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BaseDto>> profileById(Guid id)
        {

            var userId = User.GetUserId();
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user != null)
            {
                return Ok(new BaseDto
                {
                    Status = true,
                    Data = user,
                });
            }
            else
            {
                return NotFound(new BaseDto
                {
                    Status = false
                });
            }
        }


        [HttpPost]
        public async Task<ActionResult<BaseDto>> updateProfile([FromForm] PostProfileDto postProfileDto)
        {
            var uploads = Path.Combine(_environment.WebRootPath, "images");
            String FileName = Guid.NewGuid().ToString() + "." + postProfileDto.avatar.FileName.Split('.').Last(); // Give file name


            var outputPath = Path.Combine(uploads, FileName);

            using (var fileStream = new FileStream(Path.Combine(uploads, FileName), FileMode.Create))
            {
                await postProfileDto.avatar.CopyToAsync(fileStream);
            }

            // var fileName = HttpContext.Request.Form.Files["[0]abc"].FileName;

            // var files = HttpContext.Request.Form.Files;
            // var name = HttpContext.Request.Form["name"].ToString();


            // foreach (var file in files)
            // {
            //     var uploads = Path.Combine(_environment.WebRootPath, "images");


            //     if (file.Length > 0)
            //     {
            // String FileName = Guid.NewGuid().ToString() + "." + file.FileName.Split('.').Last(); // Give file name
            // using (var fileStream = new FileStream(Path.Combine(uploads, FileName), FileMode.Create))
            // {
            //     await file.CopyToAsync(fileStream);
            // }
            //     }
            // }
            return Ok(
                new BaseDto
                {
                    Status = true,
                    // Data = File()
                }
            );
        }


        [HttpGet("avatar")]
        public IActionResult ReadAvatar()
        {
            return Ok(new BaseDto
            {
                Status = true,
                Data = _resourceUrlGenerate.GetImage("qc3a07619-5d70-4cff-9a06-e238914d7ec2.png")
            });
        }
    }
}