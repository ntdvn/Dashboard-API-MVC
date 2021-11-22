using DashboardMVC.Entities;
using DashboardMVC.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DashboardMVC.Data
{
    public class ApplicationRoleGroupRepository : RepositoryBase<ApplicationRoleGroup>, IApplicationRoleGroupRepository
    {
        protected ApplicationRoleGroupRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}