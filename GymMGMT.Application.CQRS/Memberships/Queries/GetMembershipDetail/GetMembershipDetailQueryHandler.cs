using AutoMapper;
using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.Exceptions;
using GymMGMT.Domain.Entities;

namespace GymMGMT.Application.CQRS.Memberships.Queries.GetMembershipDetail
{
    public class GetMembershipDetailQueryHandler : IRequestHandler<GetMembershipDetailQuery, MembershipDetailViewModel>
    {
        private readonly IMembershipRepository _membershipRepository;
        private readonly IMapper _mapper;

        public GetMembershipDetailQueryHandler(IMembershipRepository membershipRepository, IMapper mapper)
        {
            _membershipRepository = membershipRepository;
            _mapper = mapper;
        }

        public async Task<MembershipDetailViewModel> Handle(GetMembershipDetailQuery request, CancellationToken cancellationToken)
        {
            var membership = await _membershipRepository.GetByIdWithDetailsAsync(request.Id);
            if (membership == null)
                throw new NotFoundException(nameof(Membership), request.Id);

            return _mapper.Map<MembershipDetailViewModel>(membership);
        }
    }
}