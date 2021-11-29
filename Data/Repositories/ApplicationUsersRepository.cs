using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DashboardMVC.DTOs;
using DashboardMVC.Entities;
using DashboardMVC.Helpers;
using DashboardMVC.Helpers.Params;
using DashboardMVC.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DashboardMVC.Data
{
    public class ApplicationUsersRepository : RepositoryBase<ApplicationUser>, IApplicationUsersRepository
    {
        public ApplicationUsersRepository(IDbFactory dbFactory, ApplicationDbContext applicationDbContext, IApplicationRoleRepository applicationRoleRepository, IMapper mapper) : base(dbFactory)
        {
            this._applicationRoleRepository = applicationRoleRepository;
            this._mapper = mapper;
            this._applicationDbContext = applicationDbContext;
        }

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;
        private readonly IApplicationRoleRepository _applicationRoleRepository;

        public async Task<PageList<UserDto>> GetUsersAsync(UserParams usersParams)
        {
            var users = _applicationDbContext.Users;
            return await PageList<UserDto>
                .CreateAsync(users.ProjectTo<UserDto>(_mapper.ConfigurationProvider).AsNoTracking(), usersParams.PageNumber, usersParams.PageSize);
        }

        public async Task<PageList<UserWithRolesDto>> GetUsersWithRoleAsync(UserParams usersParams)
        {
            var usersWithRoles = _applicationDbContext.Users;

            var pageList = await PageList<UserWithRolesDto>
                .CreateAsync(usersWithRoles.ProjectTo<UserWithRolesDto>(_mapper.ConfigurationProvider).AsNoTracking(), usersParams.PageNumber, usersParams.PageSize);

            foreach (var user in pageList)
            {

                var roles = _applicationRoleRepository.GetListRoleByUserId(user.Id);
                user.Roles = roles;
            }
            return pageList;
        }
    }
}