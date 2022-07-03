using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.Exceptions;
using GymMGMT.Application.Responses;
using GymMGMT.Application.Security.Contracts;
using GymMGMT.Application.Security.Exceptions;
using GymMGMT.Domain.Entities;

namespace GymMGMT.Application.CQRS.Trainings.Commands.AddMemberToTraining
{
    public class AddMemberToTrainingCommandHandler : IRequestHandler<AddMemberToTrainingCommand, ICommandResponse>
    {
        private readonly IMemberRepository _memberRepository;
        private readonly ITrainingRepository _trainingRepository;
        private readonly ICurrentUserService _currentUserService;

        public AddMemberToTrainingCommandHandler(IMemberRepository memberRepository,
            ITrainingRepository trainingRepository, ICurrentUserService currentUserService)
        {
            _memberRepository = memberRepository;
            _trainingRepository = trainingRepository;
            _currentUserService = currentUserService;
        }

        public async Task<ICommandResponse> Handle(AddMemberToTrainingCommand request, CancellationToken cancellationToken)
        {
            var member = await _memberRepository.GetByIdAsync(request.MemberId);
            var training = await _trainingRepository.GetByIdWithDetailsAsync(request.TrainingId);
            if (training == null)
                throw new NotFoundException(nameof(Training), request.TrainingId);

            if (!_currentUserService.Role.Equals("Admin") && !training.CreatedBy.Equals(_currentUserService.UserId, StringComparison.OrdinalIgnoreCase))
                throw new ForbiddenException("You are not allowed to access this resource");

            foreach (var trainingMember in training.Members)
            {
                if (trainingMember.Id == member.Id)
                    throw new ConflictException("This member is already signed up for this training");
            }

            await _trainingRepository.AddMemberAsync(training, member);

            return new CommandResponse();
        }
    }
}