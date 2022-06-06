using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.Responses;

namespace GymMGMT.Application.CQRS.MembershipTypes.Commands.DeleteMembershipType
{
    public class DeleteMembershipTypeCommandHandler : IRequestHandler<DeleteMembershipTypeCommand, ICommandResponse>
    {
        private readonly IMembershipTypeRepository _membershipTypeRepository;

        public DeleteMembershipTypeCommandHandler(IMembershipTypeRepository membershipTypeRepository)
        {
            _membershipTypeRepository = membershipTypeRepository;
        }

        public async Task<ICommandResponse> Handle(DeleteMembershipTypeCommand request, CancellationToken cancellationToken)
        {
            var membershipType = await _membershipTypeRepository.GetByIdAsync(request.Id);

            await _membershipTypeRepository.DeleteAsync(membershipType);

            return new CommandResponse();
        }
    }
}