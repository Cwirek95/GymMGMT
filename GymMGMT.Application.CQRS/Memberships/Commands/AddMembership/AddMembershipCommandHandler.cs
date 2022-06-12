using AutoMapper;
using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.CQRS.Memberships.Events.MembershipAddedEvent;
using GymMGMT.Application.Exceptions;
using GymMGMT.Application.Responses;
using GymMGMT.Domain.Entities;

namespace GymMGMT.Application.CQRS.Memberships.Commands.AddMembership
{
    public class AddMembershipCommandHandler : IRequestHandler<AddMembershipCommand, ICommandResponse>
    {
        private readonly IMembershipRepository _membershipRepository;
        private readonly IMembershipTypeRepository _membershipTypeRepository;
        private readonly IMemberRepository _memberRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public AddMembershipCommandHandler(IMembershipRepository membershipRepository,
            IMembershipTypeRepository membershipTypeRepository, IMemberRepository memberRepository, IMapper mapper, IMediator mediator)
        {
            _membershipTypeRepository = membershipTypeRepository;
            _memberRepository = memberRepository;
            _membershipRepository = membershipRepository;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<ICommandResponse> Handle(AddMembershipCommand request, CancellationToken cancellationToken)
        {
            var memberRepository = await _memberRepository.GetByIdWithDetailsAsync(request.MemberId);
            if (memberRepository == null)
                throw new NotFoundException(nameof(Member), request.MemberId);

            var membershipType = await _membershipTypeRepository.GetByIdAsync(request.MembershipTypeId);

            var membership = _mapper.Map<Membership>(request);
            membership.StartDate = DateTimeOffset.Now;
            membership.LastExtension = DateTimeOffset.Now;
            membership.EndDate = DateTimeOffset.Now.AddDays(membershipType.DurationInDays);
            if(request.Price == null || request.Price <= 0)
                membership.Price = membershipType.DefaultPrice;

            membership = await _membershipRepository.AddAsync(membership);

            await _mediator.Publish(new MembershipAddedEvent(request.MemberId, membership.Id));

            return new CommandResponse(membership.Id);
        }
    }
}