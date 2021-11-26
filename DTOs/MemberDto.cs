using System;

namespace DashboardMVC.DTOs
{
    public class MemberDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public DateTime Created
        {
            get; set;
        }
    }
}