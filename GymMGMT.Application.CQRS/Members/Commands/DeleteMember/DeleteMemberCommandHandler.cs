using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.CQRS.Members.Events.MemberDeleted;
using GymMGMT.Application.Responses;

namespace GymMGMT.Application.CQRS.Members.Commands.DeleteMember
{
    public class DeleteMemberCommandHandler : IRequestHandler<DeleteMemberCommand, ICommandResponse>
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IMediator _mediator;

        public DeleteMemberCommandHandler(IMemberRepository memberRepository, IMediator mediator)
        {
            _memberRepository = memberRepository;
            _mediator = mediator;
        }

        public async Task<ICommandResponse> Handle(DeleteMemberCommand request, CancellationToken cancellationToken)
        {
            var member = await _memberRepository.GetByIdAsync(request.Id);

            await _memberRepository.DeleteAsync(member);

            await _mediator.Publish(new MemberDeletedEvent(member.Id, (int)member.MembershipId));

            return new CommandResponse();
        }
    }
}