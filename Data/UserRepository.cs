using System;
using System.Threading.Tasks;
using AutoMapper;
using DashboardMVC.DTOs;
using DashboardMVC.Entities;
using DashboardMVC.Interfaces;

namespace DashboardMVC.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        private readonly IMapper _mapper;
        public UserRepository(ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            this._mapper = mapper;
            this._applicationDbContext = applicationDbContext;
        }

        public async Task<MemberDto> GetUserByIdAsync(string id)
        {
            var user = await _applicationDbContext.Users.FindAsync(new Guid(id));
            return _mapper.Map<MemberDto>(user);
        }
    }
}