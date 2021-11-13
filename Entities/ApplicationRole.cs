using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace DashboardMVC.Entities
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        public ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
}