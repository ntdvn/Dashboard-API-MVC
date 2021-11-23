using System;
using System.Collections.Generic;
using System.Linq;
using DashboardMVC.Entities;
using DashboardMVC.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DashboardMVC.Data
{
    public class ApplicationGroupRepository : RepositoryBase<ApplicationGroup>, IApplicationGroupRepository
    {
        public ApplicationGroupRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<ApplicationGroup> GetListGroupByUserId(string userId)
        {
            return DbContext
                .ApplicationGroups
                .Join<ApplicationGroup, ApplicationUserGroup, Guid, ApplicationGroup>(DbContext.ApplicationUserGroups, ag => ag.Id, aug => aug.GroupId, (ag, aug) => new ApplicationGroup
                {

                });
            // .Where(x => x.Id == Guid.Parse(userId)).ToList();
        }

        public IEnumerable<ApplicationUser> GetListUserByGroupId(int groupId)
        {
            return DbContext
                .ApplicationGroups
                .Join<ApplicationGroup, ApplicationUserGroup, Guid, ApplicationUser>(DbContext.ApplicationUserGroups, ag => ag.Id, aug => aug.UserId, (ag, aug) => new ApplicationUser
                {

                });
            // .Where(x => x.Id == groupId);
        }
    }
}