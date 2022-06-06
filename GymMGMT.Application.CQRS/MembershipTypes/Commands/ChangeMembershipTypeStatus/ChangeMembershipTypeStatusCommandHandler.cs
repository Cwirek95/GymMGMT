using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.Responses;

namespace GymMGMT.Application.CQRS.MembershipTypes.Commands.ChangeMembershipTypeStatus
{
    public class ChangeMembershipTypeStatusCommandHandler : IRequestHandler<ChangeMembershipTypeStatusCommand, ICommandResponse>
    {
        private readonly IMembershipTypeRepository _membershipTypeRepository;

        public ChangeMembershipTypeStatusCommandHandler(IMembershipTypeRepository membershipTypeRepository)
        {
            _membershipTypeRepository = membershipTypeRepository;
        }

        public async Task<ICommandResponse> Handle(ChangeMembershipTypeStatusCommand request, CancellationToken cancellationToken)
        {
            var membershipType = await _membershipTypeRepository.GetByIdAsync(request.Id);

            membershipType.Status = !membershipType.Status;
            await _membershipTypeRepository.UpdateAsync(membershipType);

            return new CommandResponse();
        }
    }
}