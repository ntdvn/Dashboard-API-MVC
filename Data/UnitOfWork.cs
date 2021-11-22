using DashboardMVC.Interfaces;

namespace DashboardMVC.Data
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly IDbFactory dbFactory;
        private ApplicationDbContext dbContext;
        public UnitOfWork(IDbFactory dbFactory)
        {
            this.dbFactory = dbFactory;
        }

        public ApplicationDbContext DbContext
        {
            get { return dbContext ?? (dbContext = dbFactory.Init()); }
        }

        public void Commit()
        {
            DbContext.SaveChanges();
        }
    }
}