using AutoMapper;
using GymMGMT.Application.CQRS.Auth.Commands.CreateRole;
using GymMGMT.Application.CQRS.Auth.Queries.GetRoleDetail;
using GymMGMT.Application.CQRS.Auth.Queries.GetRolesList;
using GymMGMT.Application.CQRS.Auth.Queries.GetUserDetail;
using GymMGMT.Application.CQRS.Auth.Queries.GetUsersList;
using GymMGMT.Domain.Entities;

namespace GymMGMT.Application.CQRS
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region User
            CreateMap<User, UsersInListViewModel>().ReverseMap();
            CreateMap<User, UserDetailViewModel>().ReverseMap();
            #endregion

            #region Role
            CreateMap<CreateRoleCommand, Role>();
            CreateMap<Role, RolesInListViewModel>().ReverseMap();
            CreateMap<Role, RoleDetailViewModel>().ReverseMap();
            #endregion
        }
    }
}