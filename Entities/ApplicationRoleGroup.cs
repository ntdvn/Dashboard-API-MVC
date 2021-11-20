using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DashboardMVC.Entities
{
    [Table("ApplicationRoleGroups")]

    public class ApplicationRoleGroup
    {
        public Guid GroupId { get; set; }
        [StringLength(128)]
        public string RoleId { get; set; }
        [ForeignKey("RoleId")]
        public virtual ApplicationRole Role { get; set; }

        [ForeignKey("GroupId")]
        public virtual ApplicationGroup Group { get; set; }
    }
}