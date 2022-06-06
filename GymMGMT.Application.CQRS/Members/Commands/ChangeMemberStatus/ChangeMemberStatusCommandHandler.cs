using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.Responses;

namespace GymMGMT.Application.CQRS.Members.Commands.ChangeMemberStatus
{
    public class ChangeMemberStatusCommandHandler : IRequestHandler<ChangeMemberStatusCommand, ICommandResponse>
    {
        private readonly IMemberRepository _memberRepository;

        public ChangeMemberStatusCommandHandler(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        public async Task<ICommandResponse> Handle(ChangeMemberStatusCommand request, CancellationToken cancellationToken)
        {
            var member = await _memberRepository.GetByIdAsync(request.Id);
            member.Status = !member.Status;

            await _memberRepository.UpdateAsync(member);

            return new CommandResponse();
        }
    }
}