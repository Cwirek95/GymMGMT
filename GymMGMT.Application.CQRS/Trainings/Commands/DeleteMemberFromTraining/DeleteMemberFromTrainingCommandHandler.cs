using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.Exceptions;
using GymMGMT.Application.Responses;
using GymMGMT.Application.Security.Contracts;
using GymMGMT.Application.Security.Exceptions;
using GymMGMT.Domain.Entities;

namespace GymMGMT.Application.CQRS.Trainings.Commands.DeleteMemberFromTraining
{
    public class DeleteMemberFromTrainingCommandHandler : IRequestHandler<DeleteMemberFromTrainingCommand, ICommandResponse>
    {
        private readonly ITrainingRepository _trainingRepository;
        private readonly IMemberRepository _memberRepository;
        private readonly ICurrentUserService _currentUserService;

        public DeleteMemberFromTrainingCommandHandler(ITrainingRepository trainingRepository,
            IMemberRepository memberRepository, ICurrentUserService currentUserService)
        {
            _trainingRepository = trainingRepository;
            _memberRepository = memberRepository;
            _currentUserService = currentUserService;
        }

        public async Task<ICommandResponse> Handle(DeleteMemberFromTrainingCommand request, CancellationToken cancellationToken)
        {
            var member = await _memberRepository.GetByIdAsync(request.MemberId);
            var training = await _trainingRepository.GetByIdWithDetailsAsync(request.TrainingId);
            if (training == null)
                throw new NotFoundException(nameof(Training), request.TrainingId);

            if (!_currentUserService.Role.Equals("Admin") && !training.CreatedBy.Equals(_currentUserService.UserId, StringComparison.OrdinalIgnoreCase))
                throw new ForbiddenException("You are not allowed to access this resource");

            foreach (var trainingMember in training.Members)
            {
                if (trainingMember.Id != member.Id)
                    throw new NotFoundException("This member is not signed up for this training");
            }

            await _trainingRepository.DeleteMemberAsync(training, member);

            return new CommandResponse();
        }
    }
}