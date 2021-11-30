using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DashboardMVC.DTOs
{
    public class ApplicationGroupDto
    {
        public Nullable<int> Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }

        public IEnumerable<ApplicationRoleDto> Roles { get; set; }
    }
}