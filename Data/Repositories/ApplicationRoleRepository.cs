using System;
using System.Collections.Generic;
using System.Linq;
using DashboardMVC.DTOs;
using DashboardMVC.Entities;
using DashboardMVC.Interfaces;

namespace DashboardMVC.Data
{
    public class ApplicationRoleRepository : RepositoryBase<ApplicationRole>, IApplicationRoleRepository
    {
        public ApplicationRoleRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<ApplicationRoleDto> GetListRoleByGroupId(int groupId)
        {
            return DbContext
                .ApplicationGroups
                .Join<ApplicationGroup, ApplicationRoleGroup, int, ApplicationRoleGroup>(
                    DbContext.ApplicationRoleGroups,
                    ar => ar.Id, arg => arg.GroupId,
                    (ArgIterator, arg) =>
                        new ApplicationRoleGroup
                        {
                            RoleId = arg.RoleId,
                            GroupId = arg.GroupId
                        }
                )
                .Where(arg => arg.GroupId == groupId)
                .Join<ApplicationRoleGroup, ApplicationRole, int, ApplicationRoleDto>(
                    DbContext.ApplicationRoles,
                    arg => arg.RoleId, ar => ar.Id,
                    (arg, ar) =>
                        new ApplicationRoleDto
                        {
                            Id = ar.Id,
                            Name = ar.Name,
                            Description = ar.Description,

                        }
                )
                .ToList();
        }

        public IEnumerable<ApplicationRoleDto> GetListRoleByUserId(int userId)
        {
            return DbContext
                .UserRoles
                .Where(aur => aur.UserId == userId)
                .Join<ApplicationUserRole, ApplicationRole, int, ApplicationRoleDto>(
                    DbContext.ApplicationRoles,
                    aur => aur.RoleId,
                    ar => ar.Id,
                    (aur, ar) =>
                        new ApplicationRoleDto
                        {
                            Id = ar.Id,
                            Name = ar.Name,
                            Description = ar.Description
                        }
                )
                .ToList();
        }
    }
}