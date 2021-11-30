using System;
using Microsoft.AspNetCore.Identity;

namespace DashboardMVC.Entities
{
    public class ApplicationUserRole : IdentityUserRole<int>
    {
        public ApplicationUser User { get; set; }
        public ApplicationRole Role { get; set; }
    }
}