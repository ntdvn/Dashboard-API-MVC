using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DashboardMVC.DTOs;
using DashboardMVC.Entities;
using DashboardMVC.Helpers;
using DashboardMVC.Helpers.Params;

namespace DashboardMVC.Interfaces.Services
{
    public interface IApplicationUsersService
    {
        ApplicationUser GetBy(Expression<Func<ApplicationUser, bool>> predicate);
        Task<PageList<UserDto>> GetUsersAsync(UserParams usersParams);
        Task<PageList<UserWithRolesDto>> GetUsersWithRoleAsync(UserParams usersParams);
        void Save();
    }
}