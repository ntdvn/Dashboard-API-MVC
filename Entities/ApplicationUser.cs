using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace DashboardMVC.Entities
{
   public class ApplicationUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Gender { get; set; }

        public DateTime Created { get; set; } = DateTime.Now;
        public ICollection<ApplicationUserRole> UserRoles { get; set; }

    }
}