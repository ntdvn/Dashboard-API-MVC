using System.Collections.Generic;
using DashboardMVC.DTOs;
using DashboardMVC.Entities;

namespace DashboardMVC.Interfaces
{
    public interface IApplicationGroupRepository : IRepository<ApplicationGroup>
    {
        IEnumerable<ApplicationGroupDto> GetListGroupByUserId(int userId);
        IEnumerable<ApplicationUser> GetListUserByGroupId(int groupId);

        IEnumerable<ApplicationRoleGroupDto> GetListGroupWithRoles();
    }
}