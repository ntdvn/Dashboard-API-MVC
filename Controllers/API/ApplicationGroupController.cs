using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DashboardMVC.DTOs;
using DashboardMVC.Entities;
using DashboardMVC.Helpers;
using DashboardMVC.Helpers.Params;
using DashboardMVC.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using MoreLinq;

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
        private readonly IStringLocalizer<SharedResource> _localizer;

        public ApplicationGroupController(
            IApplicationGroupService applicationGroupService, IApplicationRoleService applicationRoleService, UserManager<ApplicationUser> userManager, IMapper mapper, IStringLocalizer<SharedResource> localizer)
        {
            this._localizer = localizer;
            this._applicationGroupService = applicationGroupService;
            this._applicationRoleService = applicationRoleService;
            this._userManager = userManager;
            this._mapper = mapper;

        }

        [HttpPost]
        public ActionResult<BaseDto> Create(ApplicationGroupDto applicationGroupDto)
        {
            if (applicationGroupDto.Id == null)
            {
                // CREATE
                if (ModelState.IsValid)
                {
                    try
                    {
                        var appGroup = _applicationGroupService.Add(_mapper.Map<ApplicationGroup>(applicationGroupDto));
                        //save group
                        var listRoleGroup = new List<ApplicationRoleGroup>();
                        if (applicationGroupDto.Roles != null)
                        {
                            foreach (var role in applicationGroupDto.Roles)
                            {
                                listRoleGroup.Add(new ApplicationRoleGroup()
                                {
                                    GroupId = appGroup.Id,
                                    RoleId = (int)role.Id
                                });
                            }

                        }
                        _applicationRoleService.AddRolesToGroup(listRoleGroup, appGroup.Id);
                        _applicationRoleService.Save();
                        return Ok(new BaseDto
                        {
                            Status = true,
                            Messages = new string[] { _localizer["success_created"] }
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
            }
            else
            {
                // UPDATE
                var foundGroup = _applicationGroupService.GetBy(e => e.Id == applicationGroupDto.Id);
                if (foundGroup == null)
                {
                    return NotFound(new BaseDto
                    {
                        Status = false,
                        Messages = new string[] { _localizer["exception_not_exist"] }
                    });
                }
                else
                {
                    foundGroup.Name = applicationGroupDto.Name;
                    foundGroup.Description = applicationGroupDto.Description;
                    _applicationGroupService.Save();
                    
                    var listRoleGroup = new List<ApplicationRoleGroup>();
                    if (applicationGroupDto.Roles != null)
                    {
                        foreach (var role in applicationGroupDto.Roles.DistinctBy(e => e.Id))
                        {
                            listRoleGroup.Add(new ApplicationRoleGroup()
                            {
                                GroupId = foundGroup.Id,
                                RoleId = (int)role.Id
                            });
                        }

                        _applicationRoleService.AddRolesToGroup(listRoleGroup, foundGroup.Id);
                        _applicationRoleService.Save();

                    }
                    return Ok(new BaseDto
                    {
                        Status = true,
                        // Data = foundGroup,
                        Messages = new string[] { _localizer["success_updated"] }
                    });

                }
            }
        }


        [HttpGet]
        public ActionResult<BaseDto> Read()
        {
            return Ok(new BaseDto
            {
                Status = true,
                Data = _applicationGroupService.GetAll()
            });
        }


        [HttpGet("detail")]
        public ActionResult<BaseDto> ReadDetail()
        {
            return Ok(new BaseDto
            {
                Status = true,
                Data = _applicationGroupService.GetListGroupWithRoles()
            });
        }

        [HttpDelete]
        public ActionResult<BaseDto> DeleteById([FromQuery] GroupParams groupParams)
        {
            var resultGroup = _applicationGroupService.GetBy(e => e.Id == groupParams.Id);
            if (resultGroup == null)
            {
                return NotFound(new BaseDto
                {
                    Status = false,
                    Messages = new string[] {
                        _localizer["exception_not_exist"]
                    }
                });
            }
            else
            {
                var deletedGroup = _applicationGroupService.Delete(resultGroup.Id);
                _applicationGroupService.Save();
                return Ok(new BaseDto
                {
                    Status = true,
                    Data = deletedGroup,
                    Messages = new string[] {
                        _localizer["success_deleted"]
                    }
                });
            }

        }
    }
}