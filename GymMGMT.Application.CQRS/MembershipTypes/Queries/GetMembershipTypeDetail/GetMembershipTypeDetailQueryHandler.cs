using AutoMapper;
using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.Exceptions;
using GymMGMT.Domain.Entities;

namespace GymMGMT.Application.CQRS.MembershipTypes.Queries.GetMembershipTypeDetail
{
    public class GetMembershipTypeDetailQueryHandler : IRequestHandler<GetMembershipTypeDetailQuery, MembershipTypeDetailViewModel>
    {
        private readonly IMembershipTypeRepository _membershipTypeRepository;
        private readonly IMapper _mapper;

        public GetMembershipTypeDetailQueryHandler(IMembershipTypeRepository membershipTypeRepository, IMapper mapper)
        {
            _membershipTypeRepository = membershipTypeRepository;
            _mapper = mapper;
        }

        public async Task<MembershipTypeDetailViewModel> Handle(GetMembershipTypeDetailQuery request, CancellationToken cancellationToken)
        {
            var membershipType = await _membershipTypeRepository.GetByIdWithDetailsAsync(request.Id);
            if (membershipType == null)
                throw new NotFoundException(nameof(MembershipType), request.Id);

            return _mapper.Map<MembershipTypeDetailViewModel>(membershipType);
        }
    }
}