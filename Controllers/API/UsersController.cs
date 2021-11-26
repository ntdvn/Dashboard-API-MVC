using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;
using DashboardMVC.DTOs;
using DashboardMVC.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DashboardMVC.Controllers.API
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IUsersRepository _usersRepository;
        public UsersController(IUsersRepository usersRepository)
        {
            this._usersRepository = usersRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<MemberDto>> Read()
        {
            return Ok(this._usersRepository.GetUsersAsync());
        }
    }
}