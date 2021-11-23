using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DashboardMVC.DTOs;
using DashboardMVC.Entities;
using DashboardMVC.Helpers;

namespace DashboardMVC.Data
{
    public class UsersRepository : IUsersRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;
        public UsersRepository(ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            this._mapper = mapper;
            this._applicationDbContext = applicationDbContext;
        }

        public  IEnumerable<MemberDto> GetUsersAsync()
        {
            var users = _applicationDbContext.Users.ToList();
            return _mapper.Map<IEnumerable<ApplicationUser>, IEnumerable<MemberDto>>(users);
        }
    }
}