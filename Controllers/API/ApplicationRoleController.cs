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
        public ActionResult<BaseDto> Create(ApplicationRoleDto roleDto)
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
                var error = ModelState.Values.Where(v => v.Errors.Count > 0).SelectMany(v => v.Errors).Select(v => v.ErrorMessage);
                return BadRequest(new BaseDto
                {
                    Status = false,
                    Messages = error.ToArray()
                });
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
    }
}