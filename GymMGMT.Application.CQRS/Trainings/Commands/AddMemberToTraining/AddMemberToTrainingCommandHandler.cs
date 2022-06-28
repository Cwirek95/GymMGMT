using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.Exceptions;
using GymMGMT.Application.Responses;
using GymMGMT.Domain.Entities;

namespace GymMGMT.Application.CQRS.Trainings.Commands.AddMemberToTraining
{
    public class AddMemberToTrainingCommandHandler : IRequestHandler<AddMemberToTrainingCommand, ICommandResponse>
    {
        private readonly IMemberRepository _memberRepository;
        private readonly ITrainingRepository _trainingRepository;

        public AddMemberToTrainingCommandHandler(IMemberRepository memberRepository, ITrainingRepository trainingRepository)
        {
            _memberRepository = memberRepository;
            _trainingRepository = trainingRepository;
        }

        public async Task<ICommandResponse> Handle(AddMemberToTrainingCommand request, CancellationToken cancellationToken)
        {
            var member = await _memberRepository.GetByIdAsync(request.MemberId);
            var training = await _trainingRepository.GetByIdWithDetailsAsync(request.TrainingId);
            if (training == null)
                throw new NotFoundException(nameof(Training), request.TrainingId);

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