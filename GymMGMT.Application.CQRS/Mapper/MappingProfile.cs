using AutoMapper;
using GymMGMT.Application.CQRS.Auth.Commands.CreateRole;
using GymMGMT.Application.CQRS.Auth.Queries.GetRoleDetail;
using GymMGMT.Application.CQRS.Auth.Queries.GetRolesList;
using GymMGMT.Domain.Entities;

namespace GymMGMT.Application.CQRS.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Role
            CreateMap<CreateRoleCommand, Role>();
            CreateMap<Role, RolesInListViewModel>().ReverseMap();
            CreateMap<Role, RoleDetailViewModel>().ReverseMap();
            #endregion
        }
    }
}
