using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.Exceptions;
using GymMGMT.Application.Responses;
using GymMGMT.Application.Security.Contracts;
using GymMGMT.Application.Security.Exceptions;
using GymMGMT.Domain.Entities;

namespace GymMGMT.Application.CQRS.Trainings.Commands.ChangeTrainingPrice
{
    public class ChangeTrainingPriceCommandHandler : IRequestHandler<ChangeTrainingPriceCommand, ICommandResponse>
    {
        private readonly ITrainingRepository _trainingRepository;
        private readonly ICurrentUserService _currentUserService;

        public ChangeTrainingPriceCommandHandler(ITrainingRepository trainingRepository, ICurrentUserService currentUserService)
        {
            _trainingRepository = trainingRepository;
            _currentUserService = currentUserService;
        }

        public async Task<ICommandResponse> Handle(ChangeTrainingPriceCommand request, CancellationToken cancellationToken)
        {
            var training = await _trainingRepository.GetByIdWithDetailsAsync(request.Id);
            if (training == null)
                throw new NotFoundException(nameof(Training), request.Id);

            if (!_currentUserService.Role.Equals("Admin") && !training.CreatedBy.Equals(_currentUserService.UserId, StringComparison.OrdinalIgnoreCase))
                throw new ForbiddenException("You are not allowed to access this resource");

            if (DateTimeOffset.Compare(training.EndDate, DateTimeOffset.Now) < 0)
                throw new UnprocessableEntityException("Validation failure", "The Training has already taken place");

            training.Price = request.Price;

            await _trainingRepository.UpdateAsync(training);

            return new CommandResponse();
        }
    }
}