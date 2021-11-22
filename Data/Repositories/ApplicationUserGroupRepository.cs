using DashboardMVC.Entities;
using DashboardMVC.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DashboardMVC.Data
{
    public class ApplicationUserGroupRepository : RepositoryBase<ApplicationUserGroup>, IApplicationUserGroupRepository
    {
        protected ApplicationUserGroupRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}