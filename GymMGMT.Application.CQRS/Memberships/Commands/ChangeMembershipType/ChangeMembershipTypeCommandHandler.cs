using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.Exceptions;
using GymMGMT.Application.Responses;

namespace GymMGMT.Application.CQRS.Memberships.Commands.ChangeMembershipType
{
    public class ChangeMembershipTypeCommandHandler : IRequestHandler<ChangeMembershipTypeCommand, ICommandResponse>
    {
        private readonly IMembershipTypeRepository _membershipTypeRepository;
        private readonly IMembershipRepository _membershipRepository;

        public ChangeMembershipTypeCommandHandler(IMembershipTypeRepository membershipTypeRepository,
            IMembershipRepository membershipRepository)
        {
            _membershipTypeRepository = membershipTypeRepository;
            _membershipRepository = membershipRepository;
        }

        public async Task<ICommandResponse> Handle(ChangeMembershipTypeCommand request, CancellationToken cancellationToken)
        {
            var newMembershipType = await _membershipTypeRepository.GetByIdAsync(request.NewMembershipTypeId);

            var membership = await _membershipRepository.GetByMemberIdWithDetailsAsync(request.MemberId);
            if (membership == null)
                throw new NotFoundException("The Member with id: " + request.MemberId + " does not have a membership");

            membership.MembershipTypeId = request.NewMembershipTypeId;
            membership.StartDate = DateTimeOffset.Now;
            membership.LastExtension = DateTimeOffset.Now;
            membership.Price = newMembershipType.DefaultPrice;
            if (membership.Status == false)
            {
                membership.EndDate = DateTimeOffset.Now.AddDays(newMembershipType.DurationInDays);
                membership.Status = true;
            }
            else
            {
                membership.EndDate = membership.EndDate.AddDays(newMembershipType.DurationInDays);
            }

            await _membershipRepository.UpdateAsync(membership);

            return new CommandResponse();
        }
    }
}