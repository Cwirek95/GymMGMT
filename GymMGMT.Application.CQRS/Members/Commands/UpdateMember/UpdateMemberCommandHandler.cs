using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.Responses;

namespace GymMGMT.Application.CQRS.Members.Commands.UpdateMember
{
    public class UpdateMemberCommandHandler : IRequestHandler<UpdateMemberCommand, ICommandResponse>
    {
        private readonly IMemberRepository _memberRepository;

        public UpdateMemberCommandHandler(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        public async Task<ICommandResponse> Handle(UpdateMemberCommand request, CancellationToken cancellationToken)
        {
            var member = await _memberRepository.GetByIdAsync(request.Id);
            member.FirstName = request.FirstName;
            member.LastName = request.LastName;
            member.DateOfBirth = request.DateOfBirth;
            member.PhoneNumber = request.PhoneNumber;

            await _memberRepository.UpdateAsync(member);

            return new CommandResponse();
        }
    }
}