using System;
using System.Collections.Generic;
using DashboardMVC.DTOs;
using DashboardMVC.Entities;

namespace DashboardMVC.Interfaces.Services
{
    public interface IApplicationGroupService
    {
        ApplicationGroup GetDetail(int id);

        IEnumerable<ApplicationGroup> GetAll(int page, int pageSize, out int totalRow, string filter);

        IEnumerable<ApplicationGroupDto> GetAll();

        ApplicationGroup Add(ApplicationGroup applicationGroup);

        void Update(ApplicationGroup applicationGroup);

        ApplicationGroup Delete(int id);

        bool AddUserToGroups(IEnumerable<ApplicationUserGroup> groups, Guid userId);

        IEnumerable<ApplicationGroupDto> GetListGroupByUserId(string userId);

        IEnumerable<ApplicationUser> GetListGroupByGroupId(int groupId);

        IEnumerable<ApplicationRoleGroupDto> GetListGroupWithRoles();
        void Save();
    }
}