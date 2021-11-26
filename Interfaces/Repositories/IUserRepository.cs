using System;
using System.Threading.Tasks;
using DashboardMVC.DTOs;
using DashboardMVC.Entities;

namespace DashboardMVC.Interfaces
{
    public interface IUserRepository
    {
        Task<MemberDto> GetUserByIdAsync(Guid id);
    }
}