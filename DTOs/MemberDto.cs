using System;

namespace DashboardMVC.DTOs
{
    public class MemberDto
    {
         public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Gender { get; set; }

        public DateTime Created { get; set; } = DateTime.Now;

    }
}