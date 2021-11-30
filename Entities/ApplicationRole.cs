using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace DashboardMVC.Entities
{
    public class ApplicationRole : IdentityRole<int>
    {
        public ICollection<ApplicationRoleGroup> RoleGroups { get; set; }

        [StringLength(250)]
        public string Description { get; set; }
    }
}