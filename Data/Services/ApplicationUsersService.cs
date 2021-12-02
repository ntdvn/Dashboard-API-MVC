using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DashboardMVC.DTOs;
using DashboardMVC.Entities;
using DashboardMVC.Helpers;
using DashboardMVC.Helpers.Params;
using DashboardMVC.Interfaces;
using DashboardMVC.Interfaces.Services;

namespace DashboardMVC.Data.Services
{
    public class ApplicationUsersService : IApplicationUsersService
    {
        private readonly IApplicationUsersRepository _applicationUsersRepository;
        private readonly IUnitOfWork _unitOfWork;
        public ApplicationUsersService(IUnitOfWork unitOfWork, IApplicationUsersRepository applicationUsersRepository)
        {
            this._unitOfWork = unitOfWork;
            this._applicationUsersRepository = applicationUsersRepository;
        }



        public ApplicationUser GetBy(Expression<Func<ApplicationUser, bool>> predicate)
        {
            return this._applicationUsersRepository.GetBy(predicate);
        }

        public Task<PageList<UserDto>> GetUsersAsync(UserParams usersParams)
        {
            return _applicationUsersRepository.GetUsersAsync(usersParams);
        }

        public Task<PageList<UserWithRolesDto>> GetUsersWithRoleAsync(UserParams usersParams)
        {
            return _applicationUsersRepository.GetUsersWithRoleAsync(usersParams);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }
    }
}