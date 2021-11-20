using DashboardMVC.Entities;
using DashboardMVC.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DashboardMVC.Data
{
    public class ApplicationUserGroupRepository : RepositoryBase<ApplicationUserGroup>, IApplicationUserGroupRepository
    {
        public ApplicationUserGroupRepository(ApplicationDbContext dbContext, DbSet<ApplicationUserGroup> dbSet) : base(dbContext, dbSet)
        {
        }
    }
}