using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DashboardMVC.Common.Exceptions;
using DashboardMVC.DTOs;
using DashboardMVC.Entities;
using DashboardMVC.Helpers;
using DashboardMVC.Interfaces;
using DashboardMVC.Interfaces.Services;
using Microsoft.Extensions.Localization;
using System.Linq.Expressions;

namespace DashboardMVC.Data.Services
{
    public class ApplicationGroupService : IApplicationGroupService
    {
        private IApplicationGroupRepository _appGroupRepository;
        private IUnitOfWork _unitOfWork;
        private IApplicationUserGroupRepository _appUserGroupRepository;
        private IStringLocalizer<SharedResource> _localizer;

        private readonly IMapper _mapper;

        public ApplicationGroupService(IUnitOfWork unitOfWork,
           IApplicationUserGroupRepository appUserGroupRepository,
           IApplicationGroupRepository appGroupRepository, IStringLocalizer<SharedResource> localizer, IMapper mapper)
        {
            this._mapper = mapper;
            this._localizer = localizer;
            this._appGroupRepository = appGroupRepository;
            this._appUserGroupRepository = appUserGroupRepository;
            this._unitOfWork = unitOfWork;
        }

        public ApplicationGroup GetBy(Expression<Func<ApplicationGroup, bool>> predicate)
        {
            return _appGroupRepository.GetBy(predicate);
        }

        public ApplicationGroup Add(ApplicationGroup applicationGroup)
        {
            if (_appGroupRepository.CheckContains(x => x.Name == applicationGroup.Name))
            {
                throw new DuplicatedException(_localizer["exception_duplicated", applicationGroup.Name]);
            }
            else
            {
                return _appGroupRepository.Add(applicationGroup);
            }
        }

        public bool AddUserToGroups(IEnumerable<ApplicationUserGroup> userGroups, int userId)
        {
            _appUserGroupRepository.DeletesBy(x => x.UserId == userId);
            foreach (var userGroup in userGroups)
            {
                _appUserGroupRepository.Add(userGroup);
            }
            return true;
        }

        public ApplicationGroup Delete(int id)
        {
            var applicationGroup = this._appGroupRepository.GetById(id);
            return _appGroupRepository.Delete(applicationGroup);
        }

        public IEnumerable<ApplicationGroup> GetAll(int page, int pageSize, out int totalRow, string filter)
        {
            var query = _appGroupRepository.Gets();
            if (!string.IsNullOrEmpty(filter))
                query = query.Where(x => x.Name.Contains(filter));

            totalRow = query.Count();
            return query.OrderBy(x => x.Name).Skip(page * pageSize).Take(pageSize);

        }

        public IEnumerable<ApplicationGroupDto> GetAll()
        {
            return _mapper.Map<IEnumerable<ApplicationGroup>, IEnumerable<ApplicationGroupDto>>(_appGroupRepository.Gets());
        }

        public ApplicationGroup GetDetail(int id)
        {
            return _appGroupRepository.GetById(id);
        }

        public IEnumerable<ApplicationUser> GetListGroupByGroupId(int groupId)
        {
            return _appGroupRepository.GetListUserByGroupId(groupId);
        }

        public IEnumerable<ApplicationGroupDto> GetListGroupByUserId(int userId)
        {
            return _appGroupRepository.GetListGroupByUserId(userId);
        }

        public IEnumerable<ApplicationRoleGroupDto> GetListGroupWithRoles()
        {
            return _appGroupRepository.GetListGroupWithRoles();
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(ApplicationGroup applicationGroup)
        {
            if (_appGroupRepository.CheckContains(x => x.Name == applicationGroup.Name && x.Id != applicationGroup.Id))
                throw new DuplicatedException("Tên không được trùng");
            _appGroupRepository.Update(applicationGroup);
        }


    }
}