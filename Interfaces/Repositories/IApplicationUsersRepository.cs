using System.Threading.Tasks;
using DashboardMVC.DTOs;
using DashboardMVC.Entities;
using DashboardMVC.Helpers;
using DashboardMVC.Helpers.Params;

namespace DashboardMVC.Interfaces
{
    public interface IApplicationUsersRepository : IRepository<ApplicationUser>
    {
        Task<PageList<UserDto>> GetUsersAsync(UserParams usersParams);

        Task<PageList<UserWithRolesDto>> GetUsersWithRoleAsync(UserParams usersParams);
    }
}