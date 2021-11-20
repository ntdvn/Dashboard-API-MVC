using DashboardMVC.Entities;
using DashboardMVC.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DashboardMVC.Data
{
    public class ApplicationRoleGroupRepository : RepositoryBase<ApplicationRoleGroup>, IApplicationRoleGroupRepository
    {
        public ApplicationRoleGroupRepository(ApplicationDbContext dbContext, DbSet<ApplicationRoleGroup> dbSet) : base(dbContext, dbSet)
        {
        }
    }
}