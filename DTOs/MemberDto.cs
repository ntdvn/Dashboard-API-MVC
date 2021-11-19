using System;

namespace DashboardMVC.DTOs
{
    public class MemberDto
    {
        public string UserName { get; set; }
        public string FullName { get; set; }
        public DateTime Created
        {
            get; set;
        }
    }
}