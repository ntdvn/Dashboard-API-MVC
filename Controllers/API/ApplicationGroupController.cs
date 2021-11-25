using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DashboardMVC.DTOs;
using DashboardMVC.Entities;
using DashboardMVC.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DashboardMVC.Controllers.API
{
    [Authorize]
    [Route("api/application_groups")]
    public class ApplicationGroupController : BaseApiController

    {
        private IApplicationGroupService _applicationGroupService;
        private IApplicationRoleService _applicationRoleService;
        private UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public ApplicationGroupController(
            IApplicationGroupService applicationGroupService, IApplicationRoleService applicationRoleService, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            this._applicationGroupService = applicationGroupService;
            this._applicationRoleService = applicationRoleService;
            this._userManager = userManager;
            this._mapper = mapper;

        }

        [HttpPost]
        public ActionResult<BaseDto> Create(ApplicationGroupDto applicationGroupDto)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    var appGroup = _applicationGroupService.Add(_mapper.Map<ApplicationGroup>(applicationGroupDto));
                    _applicationGroupService.Save();

                    //save group
                    var listRoleGroup = new List<ApplicationRoleGroup>();
                    foreach (var role in applicationGroupDto.Roles)
                    {
                        listRoleGroup.Add(new ApplicationRoleGroup()
                        {
                            GroupId = appGroup.Id,
                            RoleId = role.Id
                        });
                    }
                    
                    _applicationRoleService.AddRolesToGroup(listRoleGroup, appGroup.Id);
                    _applicationRoleService.Save();


                    return Ok(new BaseDto
                    {
                        Status = true,
                    });


                }
                catch (Exception ex)
                {
                    return BadRequest(new BaseDto
                    {
                        Status = false,
                        Messages = new string[] { ex.Message }
                    });
                }

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

            // return Ok(new BaseDto
            // {
            //     Status = true,
            //     Data = applicationGroupDto.Roles
            // });

        }

        [HttpGet]
        public ActionResult<BaseDto> GetAll()
        {
            return Ok(new BaseDto
            {
                Status = true,
                Data = _applicationGroupService.GetAll()
            });
        }
    }
}