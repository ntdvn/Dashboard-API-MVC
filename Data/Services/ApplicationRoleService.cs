using System;
using System.Collections.Generic;
using System.Linq;
using DashboardMVC.Common.Exceptions;
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

        public ApplicationRole Add(ApplicationRole appRole)
        {
            if (_appRoleRepository.CheckContains(x => x.Description == appRole.Description))
            {
                throw new DuplicatedException(_localizer["exception_duplicated", appRole.Name]);
            }
            else
            {
                return _appRoleRepository.Add(appRole);
            }

        }

        public bool AddRolesToGroup(IEnumerable<ApplicationRoleGroup> roleGroups, Guid groupId)
        {
            _applicationRoleGroupRepository.DeleteMulti(x => x.GroupId == groupId);
            foreach (var roleGroup in roleGroups)
            {
                _applicationRoleGroupRepository.Add(roleGroup);
            }
            return true;
        }

        public void Delete(string id)
        {
            _appRoleRepository.DeleteMulti(x => x.Id.CompareTo(id) == 0);
        }

        public IEnumerable<ApplicationRole> GetAll(int page, int pageSize, out int totalRow, string filter)
        {
            var query = _appRoleRepository.GetAll();
            if (!string.IsNullOrEmpty(filter))
                query = query.Where(x => x.Description.Contains(filter));

            totalRow = query.Count();
            return query.OrderBy(x => x.Description).Skip(page * pageSize).Take(pageSize);
        }

        public IEnumerable<ApplicationRole> GetAll()
        {
            return _appRoleRepository.GetAll();
        }

        public ApplicationRole GetDetail(string id)
        {
            return _appRoleRepository.GetSingleByCondition(x => x.Id.CompareTo(id) == 0);
        }

        public IEnumerable<ApplicationRole> GetListRoleByGroupId(int groupId)
        {
            return _appRoleRepository.GetListRoleByGroupId(groupId);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(ApplicationRole AppRole)
        {
            if (_appRoleRepository.CheckContains(x => x.Description == AppRole.Description && x.Id != AppRole.Id))
                throw new DuplicatedException("Tên không được trùng");
            _appRoleRepository.Update(AppRole);
        }
    }
}