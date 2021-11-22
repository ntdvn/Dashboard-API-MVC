using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DashboardMVC.Entities
{
    public class ApplicationUserGroup
    {
        [StringLength(128)]
        public Guid UserId { get; set; }
        public Guid GroupId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
        [ForeignKey("GroupId")]

        public virtual ApplicationGroup ApplicationGroup { get; set; }
    }
}