using DashboardMVC.Entities;
using DashboardMVC.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DashboardMVC.Data
{
    public class ApplicationGroupRepository : RepositoryBase<ApplicationGroup>, IApplicationGroupRepository
    {
        public ApplicationGroupRepository(ApplicationDbContext dbContext, DbSet<ApplicationGroup> dbSet) : base(dbContext, dbSet)
        {
        }
    }
}