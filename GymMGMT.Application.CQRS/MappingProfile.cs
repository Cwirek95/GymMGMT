using AutoMapper;
using GymMGMT.Application.CQRS.Auth.Commands.CreateRole;
using GymMGMT.Application.CQRS.Auth.Queries.GetRoleDetail;
using GymMGMT.Application.CQRS.Auth.Queries.GetRolesList;
using GymMGMT.Application.CQRS.Auth.Queries.GetUserDetail;
using GymMGMT.Application.CQRS.Auth.Queries.GetUsersList;
using GymMGMT.Application.CQRS.Members.Commands.AddMember;
using GymMGMT.Application.CQRS.Members.Queries.GetMemberDetail;
using GymMGMT.Application.CQRS.Members.Queries.GetMembersList;
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

            #region Member
            CreateMap<AddMemberCommand, Member>();
            CreateMap<MembersInListViewModel, Member>();
            CreateMap<Member, MembersInListViewModel>()
                .ForMember(
                    dest => dest.MembershipStatus,
                    opt => opt.MapFrom(src => src.Membership.Status)
                );
            CreateMap<Member, MemberDetailViewModel>().ReverseMap();
            #endregion

            #region Membership
            CreateMap<Membership, MembershipDto>()
                .ForMember(
                    dest => dest.MembershipTypeName,
                    opt => opt.MapFrom(src => src.MembershipType.Name)
                );
            #endregion
        }
    }
}