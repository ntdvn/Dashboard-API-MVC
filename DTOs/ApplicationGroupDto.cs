using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DashboardMVC.DTOs
{
    public class ApplicationGroupDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }

        public IEnumerable<RoleDto> Roles { get; set; }
    }
}