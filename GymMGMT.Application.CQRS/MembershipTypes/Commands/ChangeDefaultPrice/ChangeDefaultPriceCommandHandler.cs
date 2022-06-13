using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.Responses;

namespace GymMGMT.Application.CQRS.MembershipTypes.Commands.ChangeDefaultPrice
{
    public class ChangeDefaultPriceCommandHandler : IRequestHandler<ChangeDefaultPriceCommand, ICommandResponse>
    {
        private readonly IMembershipTypeRepository _membershipTypeRepository;

        public ChangeDefaultPriceCommandHandler(IMembershipTypeRepository membershipTypeRepository)
        {
            _membershipTypeRepository = membershipTypeRepository;
        }

        public async Task<ICommandResponse> Handle(ChangeDefaultPriceCommand request, CancellationToken cancellationToken)
        {
            var membershipType = await _membershipTypeRepository.GetByIdAsync(request.Id);

            membershipType.DefaultPrice = request.DefaultPrice;

            await _membershipTypeRepository.UpdateAsync(membershipType);

            return new CommandResponse();
        }
    }
}