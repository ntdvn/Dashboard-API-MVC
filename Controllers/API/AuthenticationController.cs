using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DashboardMVC.DTOs;
using DashboardMVC.Entities;
using DashboardMVC.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace DashboardMVC.Controllers.API
{
    public class AuthenticationController : BaseApiController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<AuthenticationController> _localizer;
        public AuthenticationController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ITokenService tokenService, IMapper mapper, IStringLocalizer<AuthenticationController> localizer)
        {
            this._localizer = localizer;
            this._mapper = mapper;
            this._tokenService = tokenService;
            this._signInManager = signInManager;
            this._userManager = userManager;
        }



        [HttpPost("register")]
        public async Task<ActionResult<BaseDto>> register(RegisterDto registerDto)
        {
            if (ModelState.IsValid)
            {
                if (await UserExist(registerDto.Username))
                {
                    return BadRequest(new BaseDto
                    {
                        Status = false,
                        Messages = new string[] { _localizer["register_failed_user"] },
                    });
                }
                var user = _mapper.Map<ApplicationUser>(registerDto);

                var result = await _userManager.CreateAsync(user, registerDto.Password);
                if (!result.Succeeded) return BadRequest(new BaseDto
                {
                    Status = false,
                    Messages = result.Errors.Select(e => e.Description).ToArray()
                });

                return Ok(new BaseDto
                {
                    Status = true,
                    Data = new UserAuthDto
                    {
                        Username = user.UserName,
                        FullName = user.FullName,
                        Token = await _tokenService.BuildToken(user),
                    },
                });
            }
            else
            {
                var error = ModelState.Values.Where(v => v.Errors.Count > 0).SelectMany(v => v.Errors).Select(v => v.ErrorMessage);
                return BadRequest(new BaseDto
                {
                    Status = false,
                    Messages = error.ToArray()
                });
            }
        }
        [HttpPost("login")]
        public async Task<ActionResult<BaseDto>> login(LoginDto loginDto)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.Users.SingleOrDefaultAsync(u => u.UserName == loginDto.Username.ToLower());
                if (user == null)
                {
                    return NotFound(new BaseDto
                    {
                        Status = false,
                        Messages = new string[] { _localizer["login_failed_user"] },
                    });
                }
                else
                {
                    try
                    {
                        var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
                        if (!result.Succeeded) return BadRequest(new BaseDto
                        {
                            Status = false,
                            // Messages = new string[] { _localizer["login_failed_password"] }
                            Messages = new string[] { result.ToString() }
                        });
                        return Ok(new BaseDto
                        {
                            Status = true,
                            Data = new UserAuthDto
                            {
                                Username = user.UserName,
                                FullName = user.FullName,
                                Token = await _tokenService.BuildToken(user),
                            },
                            Messages = new string[] { _localizer["login_success"] }
                        });
                    }
                    catch (Exception e)
                    {
                        return BadRequest(new BaseDto
                        {
                            Status = false,
                            // Messages = new string[] { _localizer["login_failed_password"] }
                            Messages = new string[] { e.ToString() }
                        });
                    }
                }
            }
            else
            {
                var error = ModelState.Values
                 .Where(v => v.Errors.Count > 0)
                 .SelectMany(v => v.Errors)
                 .Select(v => v.ErrorMessage);
                return BadRequest(new BaseDto
                {
                    Status = false,
                    Messages = error.ToArray()
                });
            }
        }

        private async Task<bool> UserExist(string username)
        {
            return await _userManager.Users.AnyAsync(u => u.UserName == username.ToLower());
        }
    }
}