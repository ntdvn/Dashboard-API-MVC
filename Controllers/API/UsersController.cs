using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DashboardMVC.DTOs;
using DashboardMVC.Entities;
using DashboardMVC.Extensions;
using DashboardMVC.Helpers.Params;
using DashboardMVC.Interfaces;
using DashboardMVC.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DashboardMVC.Controllers.API
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApplicationRoleService _applicationRoleService;

        private readonly IApplicationGroupService _applicationGroupService;
        private readonly IMapper _mapper;

        private readonly IApplicationUsersService _applicationUsersService;

        public UsersController(IApplicationUsersService applicationUsersService, UserManager<ApplicationUser> userManager, IApplicationRoleService applicationRoleService, IApplicationGroupService applicationGroupService, IMapper mapper)
        {
            this._applicationUsersService = applicationUsersService;
            this._mapper = mapper;
            this._applicationGroupService = applicationGroupService;
            this._applicationRoleService = applicationRoleService;
            this._userManager = userManager;
        }

        [HttpPost]
        public async Task<ActionResult<BaseDto>> Create(UserPostDto userPostDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var newUser = _mapper.Map<ApplicationUser>(userPostDto);
                    newUser.Id = Guid.NewGuid();
                    var result = await _userManager.CreateAsync(newUser, userPostDto.Password);

                    if (result.Succeeded)
                    {
                        var listAppUserGroup = new List<ApplicationUserGroup>();
                        foreach (var group in userPostDto.Groups)
                        {
                            listAppUserGroup.Add(new ApplicationUserGroup()
                            {
                                GroupId = group.Id,
                                UserId = newUser.Id
                            });
                            //add role to user
                            var listRole = _applicationRoleService.GetListRoleByGroupId(group.Id);
                            foreach (var role in listRole)
                            {
                                await _userManager.RemoveFromRoleAsync(newUser, role.Name);
                                await _userManager.AddToRoleAsync(newUser, role.Name);
                            }
                        }
                        _applicationGroupService.AddUserToGroups(listAppUserGroup, newUser.Id);
                        _applicationGroupService.Save();


                        return Ok(new BaseDto
                        {
                            Status = true,
                            Messages = new string[] { }
                        });

                    }
                    else
                    {
                        return BadRequest(new BaseDto
                        {
                            Status = false,
                            Messages = new string[] { result.Errors.ToString() }
                        });
                    }

                }
                catch (Exception e)
                {
                    return BadRequest(new BaseDto
                    {
                        Status = false,
                        Messages = new string[] { e.ToString() }
                    });
                }
                // return Ok(new BaseDto
                // {
                //     Status = true,
                //     Data = userPostDto
                // });
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

        [HttpGet]
        public async Task<ActionResult<BaseDto>> Read([FromQuery] UserParams usersParams)
        {
            var users = await this._applicationUsersService.GetUsersAsync(usersParams);
            Response.AddPaginationHeader(users.CurrentPage, users.PageSize, users.TotalCount, users.TotalPages);
            return Ok(new BaseDto
            {
                Status = true,
                Data = users
            });
        }


        [HttpGet("with_roles")]
        public async Task<ActionResult<BaseDto>> ReadUsersWithRole([FromQuery] UserParams userParams)
        {
            var users = await this._applicationUsersService.GetUsersWithRoleAsync(userParams);
            Response.AddPaginationHeader(users.CurrentPage, users.PageSize, users.TotalCount, users.TotalPages);
            return Ok(new BaseDto
            {
                Status = true,
                Data = users
            });
        }
    }
}