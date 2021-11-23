using Microsoft.EntityFrameworkCore;

namespace DashboardMVC.Data
{
    public class DbFactory : Disposable, IDbFactory
    {
        private ApplicationDbContext _dbContext;

        public DbFactory(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public ApplicationDbContext Init()
        {
            return _dbContext;
        }

        protected override void DisposeCore()
        {
            if (_dbContext != null)
                _dbContext.Dispose();
        }
    }
}