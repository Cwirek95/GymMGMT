using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.Exceptions;
using GymMGMT.Application.Responses;
using GymMGMT.Application.Security.Contracts;
using GymMGMT.Application.Security.Exceptions;
using GymMGMT.Domain.Entities;

namespace GymMGMT.Application.CQRS.Trainings.Commands.ChangeTrainingDate
{
    public class ChangeTrainingDateCommandHandler : IRequestHandler<ChangeTrainingDateCommand, ICommandResponse>
    {
        private readonly ITrainingRepository _trainingRepository;
        private readonly ICurrentUserService _currentUserService;

        public ChangeTrainingDateCommandHandler(ITrainingRepository trainingRepository, ICurrentUserService currentUserService)
        {
            _trainingRepository = trainingRepository;
            _currentUserService = currentUserService;
        }

        public async Task<ICommandResponse> Handle(ChangeTrainingDateCommand request, CancellationToken cancellationToken)
        {
            var training = await _trainingRepository.GetByIdWithDetailsAsync(request.Id);
            if (training == null)
                throw new NotFoundException(nameof(Training), request.Id);

            if (!_currentUserService.Role.Equals("Admin") && !training.CreatedBy.Equals(_currentUserService.UserId, StringComparison.OrdinalIgnoreCase))
                throw new ForbiddenException("You are not allowed to access this resource");

            training.StartDate = request.StartDate;
            training.EndDate = request.EndDate;

            await _trainingRepository.UpdateAsync(training);

            return new CommandResponse();
        }
    }
}