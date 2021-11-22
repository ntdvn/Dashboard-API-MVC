namespace DashboardMVC.Data
{
    public interface IDbFactory
    {
        ApplicationDbContext Init();
    }
}