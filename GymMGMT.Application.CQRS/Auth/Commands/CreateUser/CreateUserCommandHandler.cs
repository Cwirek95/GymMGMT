using GymMGMT.Application.CQRS.Auth.Events.UserCreatedEvent;
using GymMGMT.Application.Responses;
using GymMGMT.Application.Security.Contracts;

namespace GymMGMT.Application.CQRS.Auth.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ICommandResponse>
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IMediator _mediator;

        public CreateUserCommandHandler(IAuthenticationService authenticationService, IMediator mediator)
        {
            _authenticationService = authenticationService;
            _mediator = mediator;
        }

        public async Task<ICommandResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _authenticationService.CreateUserAsync(request.Email, request.Password);

            //await _mediator.Publish(new UserCreatedEvent(user));

            return new CommandResponse(user.Id);
        }
    }
}