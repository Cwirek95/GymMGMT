using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.Responses;

namespace GymMGMT.Application.CQRS.Memberships.Commands.ChangeMembershipStatus
{
    public class ChangeMembershipStatusCommandHandler : IRequestHandler<ChangeMembershipStatusCommand, ICommandResponse>
    {
        private readonly IMembershipRepository _membershipRepository;

        public ChangeMembershipStatusCommandHandler(IMembershipRepository membershipRepository)
        {
            _membershipRepository = membershipRepository;
        }

        public async Task<ICommandResponse> Handle(ChangeMembershipStatusCommand request, CancellationToken cancellationToken)
        {
            var membership = await _membershipRepository.GetByIdAsync(request.Id);

            membership.Status = !membership.Status;

            await _membershipRepository.UpdateAsync(membership);

            return new CommandResponse();
        }
    }
}