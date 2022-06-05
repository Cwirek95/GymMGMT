using AutoMapper;
using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.Responses;
using GymMGMT.Domain.Entities;

namespace GymMGMT.Application.CQRS.Members.Commands.AddMember
{
    public class AddMemberCommandHandler : IRequestHandler<AddMemberCommand, CommandResponse>
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IMapper _mapper;

        public AddMemberCommandHandler(IMemberRepository memberRepository, IMapper mapper)
        {
            _memberRepository = memberRepository;
            _mapper = mapper;
        }

        public async Task<CommandResponse> Handle(AddMemberCommand request, CancellationToken cancellationToken)
        {
            var member = _mapper.Map<Member>(request);
            member = await _memberRepository.AddAsync(member);

            return new CommandResponse(member.Id);
        }
    }
}
