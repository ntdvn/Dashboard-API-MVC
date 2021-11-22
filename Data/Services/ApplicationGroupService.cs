using System;
using System.Collections.Generic;
using System.Linq;
using DashboardMVC.Entities;
using DashboardMVC.Interfaces;
using DashboardMVC.Interfaces.Services;
using TeduShop.Common.Exceptions;

namespace DashboardMVC.Data.Services
{
    public class ApplicationGroupService : IApplicationGroupService
    {
        private IApplicationGroupRepository _appGroupRepository;
        private IUnitOfWork _unitOfWork;
        private IApplicationUserGroupRepository _appUserGroupRepository;
        public ApplicationGroupService(IUnitOfWork unitOfWork,
           IApplicationUserGroupRepository appUserGroupRepository,
           IApplicationGroupRepository appGroupRepository)
        {
            this._appGroupRepository = appGroupRepository;
            this._appUserGroupRepository = appUserGroupRepository;
            this._unitOfWork = unitOfWork;
        }
        public ApplicationGroup Add(ApplicationGroup applicationGroup)
        {
            if (_appGroupRepository.CheckContains(x => x.Name == applicationGroup.Name))
            {
                throw new NameDuplicatedException("Tên không được trùng");
            }
            else
            {
                return _appGroupRepository.Add(applicationGroup);
            }
        }

        public bool AddUserToGroups(IEnumerable<ApplicationUserGroup> userGroups, Guid userId)
        {
            _appUserGroupRepository.DeleteMulti(x => x.UserId == userId);
            foreach (var userGroup in userGroups)
            {
                _appUserGroupRepository.Add(userGroup);
            }
            return true;
        }

        public ApplicationGroup Delete(int id)
        {
            var applicationGroup = this._appGroupRepository.GetSingleById(id);
            return _appGroupRepository.Delete(applicationGroup);
        }

        public IEnumerable<ApplicationGroup> GetAll(int page, int pageSize, out int totalRow, string filter)
        {
            var query = _appGroupRepository.GetAll();
            if (!string.IsNullOrEmpty(filter))
                query = query.Where(x => x.Name.Contains(filter));

            totalRow = query.Count();
            return query.OrderBy(x => x.Name).Skip(page * pageSize).Take(pageSize);

        }

        public IEnumerable<ApplicationGroup> GetAll()
        {
            return _appGroupRepository.GetAll();
        }

        public ApplicationGroup GetDetail(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<ApplicationUser> GetListGroupByGroupId(int groupId)
        {
            return _appGroupRepository.GetListUserByGroupId(groupId);
        }

        public IEnumerable<ApplicationGroup> GetListGroupByUserId(string userId)
        {
            return _appGroupRepository.GetListGroupByUserId(userId);
        }

        public void Save()
        {
           _unitOfWork.Commit();
        }

        public void Update(ApplicationGroup applicationGroup)
        {
             if (_appGroupRepository.CheckContains(x => x.Name == applicationGroup.Name && x.Id != applicationGroup.Id))
                throw new NameDuplicatedException("Tên không được trùng");
            _appGroupRepository.Update(applicationGroup);
        }
    }
}