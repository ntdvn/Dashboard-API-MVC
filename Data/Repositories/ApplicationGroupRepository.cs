using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using DashboardMVC.DTOs;
using DashboardMVC.Entities;
using DashboardMVC.Interfaces;

namespace DashboardMVC.Data
{
    public class ApplicationGroupRepository : RepositoryBase<ApplicationGroup>, IApplicationGroupRepository
    {
        private readonly IApplicationRoleRepository _applicationRoleRepository;
        public ApplicationGroupRepository(IDbFactory dbFactory, IApplicationRoleRepository applicationRoleRepository) : base(dbFactory)
        {
            this._applicationRoleRepository = applicationRoleRepository;
        }

        public IEnumerable<ApplicationGroupDto> GetListGroupByUserId(int userId)
        {
            return DbContext
                .ApplicationUserGroups
                .Where(e => e.UserId == userId)
                .Join<ApplicationUserGroup, ApplicationGroup, int, ApplicationGroupDto>(DbContext.ApplicationGroups, aug => aug.GroupId, ag => ag.Id, (aug, ag) => new ApplicationGroupDto
                {
                    Id = ag.Id,
                    Name = ag.Name,
                    Description = ag.Description
                }).ToList();
            // .Where(x => x.Id == int.Parse(userId)).ToList();
        }



        public IEnumerable<ApplicationUser> GetListUserByGroupId(int groupId)
        {
            return DbContext
                .ApplicationGroups
                .Join<ApplicationGroup, ApplicationUserGroup, int, ApplicationUser>(DbContext.ApplicationUserGroups, ag => ag.Id, aug => aug.UserId, (ag, aug) => new ApplicationUser
                {

                });
            // .Where(x => x.Id == groupId);
        }

        public IEnumerable<ApplicationRoleGroupDto> GetListGroupWithRoles()
        {
            var results = new List<ApplicationRoleGroupDto>();
            var applicationGroups = DbContext.ApplicationGroups.ToList();
            foreach (var group in applicationGroups)
            {
                var roles = _applicationRoleRepository.GetListRoleByGroupId(group.Id);
                var result = new ApplicationRoleGroupDto
                {
                    Id = group.Id,
                    GroupName = group.Name,
                    Description = group.Description,
                    Roles = roles
                };
                results.Add(result);
            }
            return results;
        }
    }
}