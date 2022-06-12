using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.Responses;

namespace GymMGMT.Application.CQRS.Memberships.Commands.ChangeMembershipPrice
{
    public class ChangeMembershipPriceCommandHandler : IRequestHandler<ChangeMembershipPriceCommand, ICommandResponse>
    {
        private readonly IMembershipRepository _membershipRepository;

        public ChangeMembershipPriceCommandHandler(IMembershipRepository membershipRepository)
        {
            _membershipRepository = membershipRepository;
        }

        public async Task<ICommandResponse> Handle(ChangeMembershipPriceCommand request, CancellationToken cancellationToken)
        {
            var membership = await _membershipRepository.GetByIdAsync(request.Id);

            if(membership.Price != 0)
                membership.Price = request.Price;

            await _membershipRepository.UpdateAsync(membership);

            return new CommandResponse();
        }
    }
}