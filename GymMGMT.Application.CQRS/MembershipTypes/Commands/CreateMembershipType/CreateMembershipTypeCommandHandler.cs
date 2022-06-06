using AutoMapper;
using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.Responses;
using GymMGMT.Domain.Entities;

namespace GymMGMT.Application.CQRS.MembershipTypes.Commands.CreateMembershipType
{
    public class CreateMembershipTypeCommandHandler : IRequestHandler<CreateMembershipTypeCommand, ICommandResponse>
    {
        private readonly IMembershipTypeRepository _membershipTypeRepository;
        private readonly IMapper _mapper;

        public CreateMembershipTypeCommandHandler(IMembershipTypeRepository membershipTypeRepository, IMapper mapper)
        {
            _membershipTypeRepository = membershipTypeRepository;
            _mapper = mapper;
        }

        public async Task<ICommandResponse> Handle(CreateMembershipTypeCommand request, CancellationToken cancellationToken)
        {
            var membershipType = _mapper.Map<MembershipType>(request);

            membershipType = await _membershipTypeRepository.AddAsync(membershipType);

            return new CommandResponse(membershipType.Id);
        }
    }
}