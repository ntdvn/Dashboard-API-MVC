using DashboardMVC.Entities;
using DashboardMVC.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DashboardMVC.Data
{
    public class ApplicationRoleRepository : RepositoryBase<ApplicationRole>, IApplicationRoleRepository
    {
        protected ApplicationRoleRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}