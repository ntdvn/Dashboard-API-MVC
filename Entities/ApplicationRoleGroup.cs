using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DashboardMVC.Entities
{
    [Table("ApplicationRoleGroups")]

    public class ApplicationRoleGroup
    {
        public int GroupId { get; set; }
        [StringLength(128)]
        public int RoleId { get; set; }
        [ForeignKey("RoleId")]
        public virtual ApplicationRole Role { get; set; }
        public virtual ApplicationGroup Group { get; set; }
    }
}