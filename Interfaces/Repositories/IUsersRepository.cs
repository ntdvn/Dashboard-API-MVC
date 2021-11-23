using System.Collections.Generic;
using System.Threading.Tasks;
using DashboardMVC.DTOs;

namespace DashboardMVC.Helpers
{
    public interface IUsersRepository
    {
        IEnumerable<MemberDto> GetUsersAsync();
    }
}