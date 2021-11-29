using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DashboardMVC.DTOs;
using DashboardMVC.Helpers;
using DashboardMVC.Helpers.Params;
using DashboardMVC.Interfaces;
using DashboardMVC.Interfaces.Services;

namespace DashboardMVC.Data.Services
{
    public class ApplicationUsersService : IApplicationUsersService
    {
        private readonly IApplicationUsersRepository _applicationUsersRepository;
        public ApplicationUsersService(IApplicationUsersRepository applicationUsersRepository)
        {
            this._applicationUsersRepository = applicationUsersRepository;
        }

        public Task<PageList<UserDto>> GetUsersAsync(UserParams usersParams)
        {
            return _applicationUsersRepository.GetUsersAsync(usersParams);
        }

        public Task<PageList<UserWithRolesDto>> GetUsersWithRoleAsync(UserParams usersParams)
        {
            return _applicationUsersRepository.GetUsersWithRoleAsync(usersParams);
        }
    }
}