using System;

namespace DashboardMVC.Helpers.Params
{
    public class UserParams : PaginationParams
    {
        public Guid Id { get; set; }
    }
}