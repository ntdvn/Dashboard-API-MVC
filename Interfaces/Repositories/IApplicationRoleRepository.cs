using System;
using System.Collections.Generic;
using DashboardMVC.DTOs;
using DashboardMVC.Entities;

namespace DashboardMVC.Interfaces
{
    public interface IApplicationRoleRepository : IRepository<ApplicationRole>
    {
        IEnumerable<ApplicationRoleDto> GetListRoleByGroupId(int groupId);


        IEnumerable<ApplicationRoleDto> GetListRoleByUserId(int userId);
    }
}
