using AutoMapper;
using GymMGMT.Application.CQRS.Auth.Commands.CreateRole;
using GymMGMT.Application.CQRS.Auth.Queries.GetRoleDetail;
using GymMGMT.Application.CQRS.Auth.Queries.GetRolesList;
using GymMGMT.Application.CQRS.Auth.Queries.GetUserDetail;
using GymMGMT.Application.CQRS.Auth.Queries.GetUsersList;
using GymMGMT.Application.CQRS.Members.Commands.AddMember;
using GymMGMT.Application.CQRS.Members.Queries.GetMemberDetail;
using GymMGMT.Application.CQRS.Members.Queries.GetMembersList;
using GymMGMT.Application.CQRS.Memberships.Commands.AddMembership;
using GymMGMT.Application.CQRS.Memberships.Queries.GetMembershipDetail;
using GymMGMT.Application.CQRS.Memberships.Queries.GetMembershipsList;
using GymMGMT.Application.CQRS.MembershipTypes.Commands.CreateMembershipType;
using GymMGMT.Application.CQRS.MembershipTypes.Queries.GetMembershipTypeDetail;
using GymMGMT.Application.CQRS.MembershipTypes.Queries.GetMembershipTypesList;
using GymMGMT.Application.CQRS.Trainers.Commands.AddTrainer;
using GymMGMT.Application.CQRS.Trainers.Queries.GetTrainerDetail;
using GymMGMT.Application.CQRS.Trainers.Queries.GetTrainersList;
using GymMGMT.Application.CQRS.Trainings.Commands.AddTraining;
using GymMGMT.Application.CQRS.Trainings.Queries.GetTrainingDetail;
using GymMGMT.Application.CQRS.Trainings.Queries.GetTrainingsList;
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
            CreateMap<Member, AddMemberCommand>()
                .ForMember(x => x.MembershipTypeId, opt => opt.Ignore())
                .ForMember(x => x.Price, opt => opt.Ignore());
            CreateMap<MembersInListViewModel, Member>();
            CreateMap<Member, MembersInListViewModel>()
                .ForMember(
                    dest => dest.MembershipStatus,
                    opt => opt.MapFrom(src => src.Membership.Status)
                );
            CreateMap<Member, MemberDetailViewModel>().ReverseMap();
            CreateMap<MemberDto, Member>().ReverseMap();
            #endregion

            #region Membership
            CreateMap<AddMembershipCommand, Membership>().ReverseMap();
            CreateMap<MembershipsInListViewModel, Membership>().ReverseMap();
            CreateMap<MembershipDetailViewModel, Membership>();
            CreateMap<Membership, MembershipDetailViewModel>()
                .ForMember(
                    dest => dest.MembershipTypeName,
                    opt => opt.MapFrom(src => src.MembershipType.Name)
                );
            CreateMap<Membership, MembershipDto>()
                .ForMember(
                    dest => dest.MembershipTypeName,
                    opt => opt.MapFrom(src => src.MembershipType.Name)
                );
            #endregion

            #region MembershipType
            CreateMap<CreateMembershipTypeCommand, MembershipType>().ReverseMap();
            CreateMap<MembershipType, MembershipTypesInListViewModel>().ReverseMap();
            CreateMap<MembershipType, MembershipTypeDetailViewModel>().ReverseMap();
            #endregion

            #region Trainers
            CreateMap<AddTrainerCommand, Trainer>().ReverseMap();
            CreateMap<TrainersInListViewModel, Trainer>();
            CreateMap<Trainer, TrainersInListViewModel>()
                .ForMember(
                    dest => dest.Email,
                    opt => opt.MapFrom(src => src.User.Email)
                );
            CreateMap<TrainerDetailViewModel, Trainer>();
            CreateMap<Trainer, TrainerDetailViewModel>()
                .ForMember(
                    dest => dest.Email,
                    opt => opt.MapFrom(src => src.User.Email)
                );
            #endregion

            #region Training
            CreateMap<AddTrainingCommand, Training>().ReverseMap();
            CreateMap<TrainingsInListViewModel, Training>();
            CreateMap<Training, TrainingsInListViewModel>()
                .ForMember(
                    dest => dest.TrainerFirstName,
                    opt => opt.MapFrom(src => src.Trainer.FirstName)
                )
                .ForMember(
                    dest => dest.TrainerLastName,
                    opt => opt.MapFrom(src => src.Trainer.LastName)
                );
            CreateMap<TrainingDetailViewModel, Training>();
            CreateMap<Training, TrainingDetailViewModel>()
                .ForMember(
                    dest => dest.TrainerFirstName,
                    opt => opt.MapFrom(src => src.Trainer.FirstName)
                )
                .ForMember(
                    dest => dest.TrainerLastName,
                    opt => opt.MapFrom(src => src.Trainer.LastName)
                );
            #endregion
        }
    }
}