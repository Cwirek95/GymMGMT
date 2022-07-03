using AutoMapper;
using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.Exceptions;
using GymMGMT.Application.Security.Contracts;
using GymMGMT.Application.Security.Exceptions;
using GymMGMT.Domain.Entities;

namespace GymMGMT.Application.CQRS.Trainings.Queries.GetTrainingDetail
{
    public class GetTrainingDetailQueryHandler : IRequestHandler<GetTrainingDetailQuery, TrainingDetailViewModel>
    {
        private readonly ITrainingRepository _trainingRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public GetTrainingDetailQueryHandler(ITrainingRepository trainingRepository,
            IMapper mapper, ICurrentUserService currentUserService)
        {
            _trainingRepository = trainingRepository;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<TrainingDetailViewModel> Handle(GetTrainingDetailQuery request, CancellationToken cancellationToken)
        {
            var training = await _trainingRepository.GetByIdWithDetailsAsync(request.Id);
            if (training == null)
                throw new NotFoundException(nameof(Training), request.Id);

            if((!_currentUserService.Role.Equals("Admin")) && !training.CreatedBy.Equals(_currentUserService.UserId, StringComparison.OrdinalIgnoreCase))
                throw new ForbiddenException("You are not allowed to access this resource");

            return _mapper.Map<TrainingDetailViewModel>(training);
        }
    }
}