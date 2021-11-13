using System;
using System.Threading.Tasks;
using DashboardMVC.Entities;

namespace DashboardMVC.Interfaces
{
    public interface ITokenService
    {
        Task<String> BuildToken(ApplicationUser user);
    }
}