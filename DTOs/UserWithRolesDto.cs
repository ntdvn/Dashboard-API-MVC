using System;
using System.Collections.Generic;

namespace DashboardMVC.DTOs
{
    public class UserWithRolesDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public DateTime Created { get; set; }

        public IEnumerable<ApplicationRoleDto> Roles { get; set; }
         public IEnumerable<ApplicationGroupDto> Groups { get; set; }
    }
}