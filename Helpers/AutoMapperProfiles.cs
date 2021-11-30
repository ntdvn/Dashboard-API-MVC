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

            CreateMap<ApplicationUser, UserDto>();
            CreateMap<UserDto, ApplicationUser>();


            CreateMap<ApplicationRole, ApplicationRoleDto>();
            CreateMap<ApplicationRoleDto, ApplicationRole>();

            CreateMap<ApplicationGroup, ApplicationGroupDto>();
            CreateMap<ApplicationGroupDto, ApplicationGroup>();


            CreateMap<ApplicationUser, UserCreateDto>();
            CreateMap<UserCreateDto, ApplicationUser>();

            // For Manager Users profile with roles
            CreateMap<ApplicationUser, UserWithRolesDto>();
            CreateMap<UserWithRolesDto, ApplicationUser>();
        }
    }
}