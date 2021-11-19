using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace DashboardMVC.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string FullName { get; set; }

        public DateTime Created { get; set; } = DateTime.Now;
        public ICollection<ApplicationUserRole> UserRoles { get; set; }

    }
}