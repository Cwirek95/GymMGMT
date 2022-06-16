using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.Responses;

namespace GymMGMT.Application.CQRS.Memberships.Commands.CheckMembershipValidity
{
    public class CheckMembershipValidityCommandHandler : IRequestHandler<CheckMembershipValidityCommand, ICommandResponse>
    {
        private readonly IMembershipRepository _membershipRepository;

        public CheckMembershipValidityCommandHandler(IMembershipRepository membershipRepository)
        {
            _membershipRepository = membershipRepository;
        }

        public async Task<ICommandResponse> Handle(CheckMembershipValidityCommand request, CancellationToken cancellationToken)
        {
            var memberships = await _membershipRepository.GetAllAsync();

            foreach (var membership in memberships)
            {
                if(DateTimeOffset.Compare(membership.EndDate, DateTimeOffset.Now) < 0)
                {
                    membership.Status = false;
                }
            }
            await _membershipRepository.UpdateListAsync(memberships);

            return new CommandResponse();
        }
    }
}