using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.Responses;

namespace GymMGMT.Application.CQRS.Members.Commands.DeleteMember
{
    public class DeleteMemberCommandHandler : IRequestHandler<DeleteMemberCommand, ICommandResponse>
    {
        private readonly IMemberRepository _memberRepository;

        public DeleteMemberCommandHandler(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        public async Task<ICommandResponse> Handle(DeleteMemberCommand request, CancellationToken cancellationToken)
        {
            var member = await _memberRepository.GetByIdAsync(request.Id);

            await _memberRepository.DeleteAsync(member);

            return new CommandResponse();
        }
    }
}