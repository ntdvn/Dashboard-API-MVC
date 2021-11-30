using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using DashboardMVC.DTOs;
using DashboardMVC.Entities;

namespace DashboardMVC.Interfaces.Services
{
    public interface IApplicationRoleService
    {
        ApplicationRole GetBy(Expression<Func<ApplicationRole, bool>> predicate);
        ApplicationRole GetDetail(int id);
        IEnumerable<ApplicationRole> GetAll(int page, int pageSize, out int totalRow, string filter);

        IEnumerable<ApplicationRole> GetAll();

        ApplicationRole Add(ApplicationRole appRole);

        void Update(ApplicationRole AppRole);

        void Delete(int id);

        //Add roles to a sepcify group
        bool AddRolesToGroup(IEnumerable<ApplicationRoleGroup> roleGroups, int groupId);

        //Get list role by group id
        IEnumerable<ApplicationRoleDto> GetListRoleByGroupId(int groupId);

        IEnumerable<ApplicationRoleDto> GetListRoleByUserId(int userId);

        void Save();
    }
}