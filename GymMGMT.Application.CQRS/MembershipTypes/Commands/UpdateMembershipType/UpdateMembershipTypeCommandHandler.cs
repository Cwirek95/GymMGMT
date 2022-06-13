using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.Responses;

namespace GymMGMT.Application.CQRS.MembershipTypes.Commands.UpdateMembershipType
{
    public class UpdateMembershipTypeCommandHandler : IRequestHandler<UpdateMembershipTypeCommand, ICommandResponse>
    {
        private readonly IMembershipTypeRepository _membershipTypeRepository;

        public UpdateMembershipTypeCommandHandler(IMembershipTypeRepository membershipTypeRepository)
        {
            _membershipTypeRepository = membershipTypeRepository;
        }

        public async Task<ICommandResponse> Handle(UpdateMembershipTypeCommand request, CancellationToken cancellationToken)
        {
            var membershipType = await _membershipTypeRepository.GetByIdAsync(request.Id);

            membershipType.Name = request.Name;

            await _membershipTypeRepository.UpdateAsync(membershipType);

            return new CommandResponse();
        }
    }
}