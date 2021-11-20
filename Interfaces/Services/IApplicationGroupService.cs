using System.Collections;
using System.Collections.Generic;
using DashboardMVC.Entities;

namespace DashboardMVC.Interfaces.Services
{
    public interface IApplicationGroupService
    {
        ApplicationGroup GetDetail(int id);

        IEnumerable<ApplicationGroup> GetAll(int page, int pageSize, out int totalRow, string filter);

        IEnumerable<ApplicationGroup> GetAll();

        ApplicationGroup Add(ApplicationGroup applicationGroup);

        void Update(ApplicationGroup applicationGroup);

        ApplicationGroup Delete(int id);

        bool AddUserToGroups(IEnumerable<ApplicationGroup> groups, string userId);

        IEnumerable<ApplicationGroup> GetListGroupByUserId(string userId);

        IEnumerable<ApplicationUser> GetListGroupByGroupId(int groupId);

        void Save();
    }
}