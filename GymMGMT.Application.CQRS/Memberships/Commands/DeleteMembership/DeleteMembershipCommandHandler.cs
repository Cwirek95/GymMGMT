using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.Responses;

namespace GymMGMT.Application.CQRS.Memberships.Commands.DeleteMembership
{
    public class DeleteMembershipCommandHandler : IRequestHandler<DeleteMembershipCommand, ICommandResponse>
    {
        private readonly IMembershipRepository _membershipRepository;

        public DeleteMembershipCommandHandler(IMembershipRepository membershipRepository)
        {
            _membershipRepository = membershipRepository;
        }

        public async Task<ICommandResponse> Handle(DeleteMembershipCommand request, CancellationToken cancellationToken)
        {
            var membership = await _membershipRepository.GetByIdAsync(request.Id);

            await _membershipRepository.DeleteAsync(membership);

            return new CommandResponse();
        }
    }
}