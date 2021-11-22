using System.Threading.Tasks;
using DashboardMVC.DTOs;
using DashboardMVC.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace DashboardMVC.Controllers.API
{
    public class ApplicationGroupController : BaseApiController

    {
        private IApplicationGroupService _appGroupService;
        // private IApplicationRoleService _appRoleService;
        // private ApplicationUserManager _userManager;

        public ApplicationGroupController(
            IApplicationGroupService appGroupService)
        {
            _appGroupService = appGroupService;

        }
        [HttpGet]
        public ActionResult<BaseDto> GetAll()
        {
            return  Ok(new BaseDto
            {
                Status = true,
                Data = _appGroupService.GetAll()
            });
        }
    }
}