using MediatR;

namespace GymMGMT.Application.CQRS.Auth.Queries.GetUserDetail
{
    public class GetUserDetailQuery : IRequest<UserDetailViewModel>
    {
        public Guid Id { get; set; }
    }
}