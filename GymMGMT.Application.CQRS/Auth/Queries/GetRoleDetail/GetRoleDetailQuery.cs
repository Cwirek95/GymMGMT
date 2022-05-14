using MediatR;

namespace GymMGMT.Application.CQRS.Auth.Queries.GetRoleDetail
{
    public class GetRoleDetailQuery : IRequest<RoleDetailViewModel>
    {
        public Guid Id { get; set; }
    }
}
