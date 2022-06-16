using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.Responses;

namespace GymMGMT.Application.CQRS.Memberships.Commands.SetDefaultPriceForCurrentMembers
{
    public class SetDefaultPriceForCurrentMembersCommandHandler : IRequestHandler<SetDefaultPriceForCurrentMembersCommand, ICommandResponse>
    {
        private readonly IMembershipRepository _membershipRepository;
        private readonly IMembershipTypeRepository _membershipTypeRepository;

        public SetDefaultPriceForCurrentMembersCommandHandler(IMembershipRepository membershipRepository,
            IMembershipTypeRepository membershipTypeRepository)
        {
            _membershipRepository = membershipRepository;
            _membershipTypeRepository = membershipTypeRepository;
        }

        public async Task<ICommandResponse> Handle(SetDefaultPriceForCurrentMembersCommand request, CancellationToken cancellationToken)
        {
            var membershipType = await _membershipTypeRepository.GetByIdAsync(request.MembershipTypeId);
            var memberships = await _membershipRepository.GetAllByMembershipTypeIdWithDetailsAsync(membershipType.Id);

            foreach (var membership in memberships)
            {
                membership.Price = membership.MembershipType.DefaultPrice;
            }
            await _membershipRepository.UpdateListAsync(memberships);

            return new CommandResponse();
        }
    }
}