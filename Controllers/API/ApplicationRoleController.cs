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

namespace DashboardMVC.Controllers.API
{
    [Authorize]
    [Route("api/roles")]
    public class ApplicationRoleController : BaseApiController
    {
        private IApplicationRoleService _applicationRoleService;
        private readonly IMapper _mapper;
        public ApplicationRoleController(IApplicationRoleService applicationRoleService, IMapper mapper)
        {
            this._mapper = mapper;
            this._applicationRoleService = applicationRoleService;
        }

        [HttpPost]
        public ActionResult<BaseDto> Create(RoleDto roleDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _applicationRoleService.Add(_mapper.Map<ApplicationRole>(roleDto));
                    _applicationRoleService.Save();
                    return Ok(new BaseDto
                    {
                        Status = true,
                        Data = roleDto
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
                return BadRequest(new BaseDto
                {
                    Status = false
                });
            }
        }


        [HttpGet]
        public ActionResult<BaseDto> Read()
        {
            return Ok(new BaseDto
            {
                Status = true,
                Data = _mapper.Map<IEnumerable<ApplicationRole>, IEnumerable<RoleDto>>(_applicationRoleService.GetAll()),
            });
        }
    }
}