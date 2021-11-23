using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace DashboardMVC.Entities
{
    // [Table("ApplicationUser")]
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string FullName { get; set; }

        public DateTime Created { get; set; } = DateTime.Now;

        public ICollection<ApplicationUserGroup> UserGroups { get; set; }
        // public ICollection<ApplicationRoleGroup> RoleGroups { get; set; }


    }
}