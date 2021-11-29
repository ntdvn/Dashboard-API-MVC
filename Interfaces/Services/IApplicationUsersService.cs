using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DashboardMVC.DTOs;
using DashboardMVC.Helpers;
using DashboardMVC.Helpers.Params;

namespace DashboardMVC.Interfaces.Services
{
    public interface IApplicationUsersService
    {
        Task<PageList<UserDto>> GetUsersAsync(UserParams usersParams);
        Task<PageList<UserWithRolesDto>> GetUsersWithRoleAsync(UserParams usersParams);
    }
}