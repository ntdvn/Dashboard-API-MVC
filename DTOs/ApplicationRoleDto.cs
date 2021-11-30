using System;
using System.ComponentModel.DataAnnotations;

namespace DashboardMVC.DTOs
{
    public class ApplicationRoleDto
    {
        public Nullable<int> Id { get; set; }
        [Required]
        public string Name { get; set; }

        public string NormalizedName { get; set; }
        [Required]
        public string Description { get; set; }
    }
}