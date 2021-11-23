using AutoMapper;
using DashboardMVC.DTOs;
using DashboardMVC.Entities;

namespace DashboardMVC.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<RegisterDto, ApplicationUser>();
            CreateMap<ApplicationUser, RegisterDto>();

            CreateMap<ApplicationUser, MemberDto>();
            CreateMap<MemberDto, ApplicationUser>();


            CreateMap<ApplicationRole, RoleDto>();
            CreateMap<RoleDto, ApplicationRole>();

            CreateMap<ApplicationGroup, ApplicationGroupDto>();
            CreateMap<ApplicationGroupDto, ApplicationGroup>();
        }
    }
}