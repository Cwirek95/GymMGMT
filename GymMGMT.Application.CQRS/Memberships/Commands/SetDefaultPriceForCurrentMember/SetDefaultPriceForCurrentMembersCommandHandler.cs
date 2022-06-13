using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.Responses;

namespace GymMGMT.Application.CQRS.Memberships.Commands.SetDefaultPriceForCurrentMember
{
    public class SetDefaultPriceForCurrentMembersCommandHandler : IRequestHandler<SetDefaultPriceForCurrentMembersCommand, ICommandResponse>
    {
        private readonly IMembershipRepository _membershipRepository;

        public SetDefaultPriceForCurrentMembersCommandHandler(IMembershipRepository membershipRepository)
        {
            _membershipRepository = membershipRepository;
        }

        public async Task<ICommandResponse> Handle(SetDefaultPriceForCurrentMembersCommand request, CancellationToken cancellationToken)
        {
            var memberships = await _membershipRepository.GetAllByMembershipTypeIdWithDetailsAsync(request.MembershipTypeId);

            foreach (var membership in memberships)
            {
                membership.Price = membership.MembershipType.DefaultPrice;
            }
            await _membershipRepository.UpdateListAsync(memberships);

            return new CommandResponse();
        }
    }
}