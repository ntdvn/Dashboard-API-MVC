using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DashboardMVC.Common.Exceptions;
using DashboardMVC.DTOs;
using DashboardMVC.Entities;
using DashboardMVC.Helpers;
using DashboardMVC.Interfaces;
using DashboardMVC.Interfaces.Services;
using Microsoft.Extensions.Localization;

namespace DashboardMVC.Data.Services
{
    public class ApplicationRoleService : IApplicationRoleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IApplicationRoleGroupRepository _applicationRoleGroupRepository;
        private readonly IApplicationRoleRepository _appRoleRepository;
        private readonly IStringLocalizer<SharedResource> _localizer;


        public ApplicationRoleService(IUnitOfWork unitOfWork,
            IApplicationRoleRepository appRoleRepository, IApplicationRoleGroupRepository applicationRoleGroupRepository, IStringLocalizer<SharedResource> localizer)
        {

            this._localizer = localizer;
            this._applicationRoleGroupRepository = applicationRoleGroupRepository;
            this._appRoleRepository = appRoleRepository;

            this._unitOfWork = unitOfWork;
        }

        public ApplicationRole GetBy(Expression<Func<ApplicationRole, bool>> predicate)
        {
            return _appRoleRepository.GetBy(predicate);
        }

        public ApplicationRole Add(ApplicationRole appRole)
        {
            if (_appRoleRepository.CheckContains(x => x.Name == appRole.Name))
            {
                throw new DuplicatedException(_localizer["exception_duplicated", appRole.Name]);
            }
            else
            {
                appRole.NormalizedName = appRole.Name.ToUpper();
                return _appRoleRepository.Add(appRole);
            }

        }

        public bool AddRolesToGroup(IEnumerable<ApplicationRoleGroup> roleGroups, int groupId)
        {
            _applicationRoleGroupRepository.DeletesBy(x => x.GroupId == groupId);
            foreach (var roleGroup in roleGroups)
            {
                _applicationRoleGroupRepository.Add(roleGroup);
            }
            return true;
        }

        public void Delete(int id)
        {
            _appRoleRepository.DeletesBy(x => x.Id == id);
        }

        public IEnumerable<ApplicationRole> GetAll(int page, int pageSize, out int totalRow, string filter)
        {
            var query = _appRoleRepository.Gets();
            if (!string.IsNullOrEmpty(filter))
                query = query.Where(x => x.Description.Contains(filter));

            totalRow = query.Count();
            return query.OrderBy(x => x.Description).Skip(page * pageSize).Take(pageSize);
        }

        public IEnumerable<ApplicationRole> GetAll()
        {
            return _appRoleRepository.Gets();
        }



        public ApplicationRole GetDetail(int id)
        {
            return _appRoleRepository.GetBy(x => x.Id == id);
        }

        public IEnumerable<ApplicationRoleDto> GetListRoleByGroupId(int groupId)
        {
            return _appRoleRepository.GetListRoleByGroupId(groupId);
        }

        public IEnumerable<ApplicationRoleDto> GetListRoleByUserId(int userId)
        {
            return _appRoleRepository.GetListRoleByUserId(userId);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(ApplicationRole AppRole)
        {
            if (_appRoleRepository.CheckContains(x => x.Description == AppRole.Description && x.Id != AppRole.Id))
                throw new DuplicatedException("T??n kh??ng ???????c tr??ng");
            _appRoleRepository.Update(AppRole);
        }
    }
}