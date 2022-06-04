using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.Responses;

namespace GymMGMT.Application.CQRS.Auth.Commands.ChangeUserStatus
{
    public class ChangeUserStatusCommandHandler : IRequestHandler<ChangeUserStatusCommand, ICommandResponse>
    {
        private readonly IUserRepository _userRepository;

        public ChangeUserStatusCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ICommandResponse> Handle(ChangeUserStatusCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.Id);

            user.Status = !user.Status;
            await _userRepository.UpdateAsync(user);

            return new CommandResponse();
        }
    }
}