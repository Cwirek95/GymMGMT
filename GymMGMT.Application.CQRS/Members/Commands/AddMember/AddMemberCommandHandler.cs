using AutoMapper;
using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.CQRS.Members.Events.MemberAdded;
using GymMGMT.Application.Responses;
using GymMGMT.Domain.Entities;

namespace GymMGMT.Application.CQRS.Members.Commands.AddMember
{
    public class AddMemberCommandHandler : IRequestHandler<AddMemberCommand, CommandResponse>
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IMembershipTypeRepository _membershipTypeRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public AddMemberCommandHandler(IMemberRepository memberRepository, IMapper mapper,
            IMediator mediator, IMembershipTypeRepository membershipTypeRepository, IUserRepository userRepository)
        {
            _memberRepository = memberRepository;
            _mapper = mapper;
            _mediator = mediator;
            _membershipTypeRepository = membershipTypeRepository;
            _userRepository = userRepository;
        }

        public async Task<CommandResponse> Handle(AddMemberCommand request, CancellationToken cancellationToken)
        {
            var membershipType = await _membershipTypeRepository.GetByIdAsync(request.MembershipTypeId);
            var user = await _userRepository.GetByIdAsync(request.UserId);

            var member = _mapper.Map<Member>(request);
            member = await _memberRepository.AddAsync(member);

            await _mediator.Publish(new MemberAddedEvent(member.Id, membershipType.Id, request.Price));

            return new CommandResponse(member.Id);
        }
    }
}