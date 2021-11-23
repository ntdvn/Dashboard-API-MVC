using System;
using System.Collections.Generic;
using System.Linq;
using DashboardMVC.Entities;
using DashboardMVC.Interfaces;

namespace DashboardMVC.Data
{
    public class ApplicationRoleRepository : RepositoryBase<ApplicationRole>, IApplicationRoleRepository
    {
        public ApplicationRoleRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<ApplicationRole> GetListRoleByGroupId(int groupId)
        {
            return DbContext
                .ApplicationRoles
                .Join<ApplicationRole, ApplicationRoleGroup, Guid, ApplicationRole>(
                    DbContext.ApplicationRoleGroups,
                    ar => ar.Id, arg => arg.RoleId,
                    (ArgIterator, arg) =>
                        new ApplicationRole
                        {

                        }
                );
        }
    }
}