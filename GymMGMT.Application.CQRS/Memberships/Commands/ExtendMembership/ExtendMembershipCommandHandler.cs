using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.Exceptions;
using GymMGMT.Application.Responses;
using GymMGMT.Domain.Entities;

namespace GymMGMT.Application.CQRS.Memberships.Commands.ExtendMembership
{
    public class ExtendMembershipCommandHandler : IRequestHandler<ExtendMembershipCommand, ICommandResponse>
    {
        private readonly IMembershipRepository _membershipRepository;

        public ExtendMembershipCommandHandler(IMembershipRepository membershipRepository)
        {
            _membershipRepository = membershipRepository;
        }

        public async Task<ICommandResponse> Handle(ExtendMembershipCommand request, CancellationToken cancellationToken)
        {
            var membership = await _membershipRepository.GetByIdWithDetailsAsync(request.Id);
            if (membership == null)
                throw new NotFoundException(nameof(Membership), request.Id);

            membership.LastExtension = DateTimeOffset.Now;
            if (membership.Status == false)
            {
                membership.EndDate = DateTimeOffset.Now.AddDays(membership.MembershipType.DurationInDays);
                membership.Status = true;
            }
            else
            {
                membership.EndDate = membership.EndDate.AddDays(membership.MembershipType.DurationInDays);
            }

            await _membershipRepository.UpdateAsync(membership);

            return new CommandResponse();
        }
    }
}