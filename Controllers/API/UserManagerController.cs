using System;
using System.Threading.Tasks;
using AutoMapper;
using DashboardMVC.DTOs;
using DashboardMVC.Entities;
using DashboardMVC.Extensions;
using DashboardMVC.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DashboardMVC.Controllers.API
{
    [Authorize]
    [Route("user_manager")]
    public class UserManagerController : BaseApiController
    {
        public UserManager<ApplicationUser> _userManager { get; set; }
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserManagerController(IUserRepository userRepository, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            this._mapper = mapper;
            this._userRepository = userRepository;
            this._userManager = userManager;
            if (userManager is null)
            {
                throw new ArgumentNullException(nameof(userManager));
            }
        }

        [HttpGet("profile")]
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

        [HttpGet("profile/{id}")]
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
    }
}