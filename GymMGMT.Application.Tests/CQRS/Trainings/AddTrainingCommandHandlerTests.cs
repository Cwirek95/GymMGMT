using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.CQRS;
using GymMGMT.Application.CQRS.Trainings.Commands.AddTraining;
using GymMGMT.Application.Tests.Mocks;
using GymMGMT.Domain.Enums;

namespace GymMGMT.Application.Tests.CQRS.Trainings
{
    public class AddTrainingCommandHandlerTests
    {
        private IMapper _mapper;
        private Mock<ITrainingRepository> _trainingRepositoryMock;
        private Mock<ITrainerRepository> _trainerRepositoryMock;

        public AddTrainingCommandHandlerTests()
        {
            var confProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = confProvider.CreateMapper();
            _trainingRepositoryMock = TrainingRepositoryMock.GetTrainingRepository();
            _trainerRepositoryMock = TrainerRepositoryMock.GetTrainerRepository();
        }

        [Fact()]
        public async Task Handle_ForValidCommand_ReturnSuccessResponse()
        {
            // Arrange
            var trainers = await _trainerRepositoryMock.Object.GetAllAsync();
            var handler = new AddTrainingCommandHandler(_trainingRepositoryMock.Object, _mapper,
                _trainerRepositoryMock.Object);
            var command = new AddTrainingCommand()
            {
                StartDate = DateTimeOffset.Now.AddDays(-10),
                EndDate = DateTimeOffset.Now.AddDays(-9),
                Price = 25,
                TrainingType = TrainingType.INDIVIDUAL,
                TrainerId = trainers.First().Id
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            response.Success.Should().BeTrue();
        }

        [Fact()]
        public async Task Handle_ForValidCommand_ReturnOneMoreTrainings()
        {
            // Arrange
            var items = await _trainerRepositoryMock.Object.GetAllAsync();
            var countBefore = (await _trainingRepositoryMock.Object.GetAllAsync()).Count;
            var handler = new AddTrainingCommandHandler(_trainingRepositoryMock.Object, _mapper,
                _trainerRepositoryMock.Object);
            var command = new AddTrainingCommand()
            {
                StartDate = DateTimeOffset.Now.AddDays(-10),
                EndDate = DateTimeOffset.Now.AddDays(-9),
                Price = 25,
                TrainingType = TrainingType.INDIVIDUAL,
                TrainerId = items.First().Id
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);
            var countAfter = (await _trainingRepositoryMock.Object.GetAllAsync()).Count;

            // Assert
            countAfter.Should().Be(countBefore + 1);
        }
    }
}