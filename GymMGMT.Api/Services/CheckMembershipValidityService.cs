using GymMGMT.Application.CQRS.Memberships.Commands.CheckMembershipValidity;
using MediatR;
using Quartz;

namespace GymMGMT.Api.Services
{
    public class CheckMembershipValidityService : IJob
    {
        private readonly IMediator _mediator;

        public CheckMembershipValidityService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await _mediator.Send(new CheckMembershipValidityCommand());
        }
    }
}