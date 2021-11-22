using System.Collections.Generic;
using DashboardMVC.Entities;

namespace DashboardMVC.Interfaces
{
    public interface IApplicationGroupRepository : IRepository<ApplicationGroup>
    {
        IEnumerable<ApplicationGroup> GetListGroupByUserId(string userId);
        IEnumerable<ApplicationUser> GetListUserByGroupId(int groupId);
    }
}