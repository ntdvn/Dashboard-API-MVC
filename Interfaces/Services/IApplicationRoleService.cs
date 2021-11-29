using System;
using System.Collections.Generic;
using DashboardMVC.DTOs;
using DashboardMVC.Entities;

namespace DashboardMVC.Interfaces.Services
{
    public interface IApplicationRoleService
    {
        ApplicationRole GetDetail(string id);

        IEnumerable<ApplicationRole> GetAll(int page, int pageSize, out int totalRow, string filter);

        IEnumerable<ApplicationRole> GetAll();

        ApplicationRole Add(ApplicationRole appRole);

        void Update(ApplicationRole AppRole);

        void Delete(string id);

        //Add roles to a sepcify group
        bool AddRolesToGroup(IEnumerable<ApplicationRoleGroup> roleGroups, Guid groupId);

        //Get list role by group id
        IEnumerable<ApplicationRoleDto> GetListRoleByGroupId(Guid groupId);

        IEnumerable<ApplicationRoleDto> GetListRoleByUserId(Guid userId);

        void Save();
    }
}