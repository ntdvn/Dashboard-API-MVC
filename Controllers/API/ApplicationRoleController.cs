using System.Collections;
using System;
using AutoMapper;
using DashboardMVC.Common.Exceptions;
using DashboardMVC.DTOs;
using DashboardMVC.Entities;
using DashboardMVC.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using DashboardMVC.Helpers.Params;
using Microsoft.Extensions.Localization;
using DashboardMVC.Helpers;

namespace DashboardMVC.Controllers.API
{
    [Authorize]
    [Route("api/roles")]
    public class ApplicationRoleController : BaseApiController
    {
        private IApplicationRoleService _applicationRoleService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResource> _localizer;
        public ApplicationRoleController(IApplicationRoleService applicationRoleService, IMapper mapper, IStringLocalizer<SharedResource> localizer)
        {
            this._localizer = localizer;
            this._mapper = mapper;
            this._applicationRoleService = applicationRoleService;
        }

        [HttpPost]
        public ActionResult<BaseDto> CreateAndUpdate(ApplicationRoleDto roleDto)
        {
            if (roleDto.Id == null)
            {
                // CREATE
                if (ModelState.IsValid)
                {
                    try
                    {
                        var createdRole = _applicationRoleService.Add(_mapper.Map<ApplicationRole>(roleDto));
                        _applicationRoleService.Save();
                        return Ok(new BaseDto
                        {
                            Status = true,
                            Data = _mapper.Map<ApplicationRoleDto>(createdRole),
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
                var foundRole = _applicationRoleService.GetBy(e => e.Id == roleDto.Id);
                foundRole.Description = roleDto.Description;
                if (foundRole == null)
                {
                    return NotFound(new BaseDto
                    {
                        Status = false,
                        Messages = new string[] { _localizer["exception_not_exist"] }
                    });
                }
                else
                {
                    foundRole.Description = roleDto.Description;
                    _applicationRoleService.Save();
                    return Ok(new BaseDto
                    {
                        Status = true,
                        Data = _mapper.Map<ApplicationRoleDto>(foundRole),
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
                Data = _mapper.Map<IEnumerable<ApplicationRole>, IEnumerable<ApplicationRoleDto>>(_applicationRoleService.GetAll()),
            });
        }


        [HttpGet("find")]
        public ActionResult<BaseDto> Read([FromQuery] UserParams userParams)
        {
            return Ok(new BaseDto
            {
                Status = true,
                Data = _applicationRoleService.GetListRoleByUserId(userParams.Id),
            });
        }

        [HttpDelete]
        public ActionResult<BaseDto> Delete([FromQuery] RoleParams roleParams)
        {
            _applicationRoleService.Delete(roleParams.Id);
            _applicationRoleService.Save();

            return Ok(new BaseDto
            {
                Status = true,
                Messages = new string[] { _localizer["success_deleted"] }
            });
        }
    }
}