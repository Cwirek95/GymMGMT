using MediatR;

namespace GymMGMT.Application.CQRS.Auth.Queries.GetRolesList
{
    public class GetRolesListQuery : IRequest<IEnumerable<RolesInListViewModel>>
    {
        public Guid Id { get; set; }
    }
}
