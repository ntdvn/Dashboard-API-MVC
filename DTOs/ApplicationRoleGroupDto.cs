using System;
using System.Collections;
using System.Collections.Generic;
using DashboardMVC.Entities;

namespace DashboardMVC.DTOs
{
    public class ApplicationRoleGroupDto
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
        public string Description { get; set; }
        public IEnumerable<ApplicationRoleDto> Roles { get; set; }
    }
}