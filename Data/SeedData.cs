using DashboardMVC.Interfaces.Services;

namespace DashboardMVC.Data
{
    public class SeedData
    {
        private readonly IApplicationGroupService _applicationGroupService;
        public SeedData(IApplicationGroupService applicationGroupService)
        {
            this._applicationGroupService = applicationGroupService;
        }
    }
}