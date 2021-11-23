using System.Collections.Generic;
using DashboardMVC.Entities;

namespace DashboardMVC.Interfaces
{
    public interface IApplicationRoleRepository : IRepository<ApplicationRole>
    {
        IEnumerable<ApplicationRole> GetListRoleByGroupId(int groupId);
    }
}
