using System;
using System.ComponentModel.DataAnnotations;

namespace DashboardMVC.DTOs
{
    public class RoleDto
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [StringLength(250)]
        public string Description { get; set; }
    }
}