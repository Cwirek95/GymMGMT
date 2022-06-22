using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.CQRS.Trainings.Commands.ChangeTrainer;
using GymMGMT.Application.Tests.Mocks;

namespace GymMGMT.Application.Tests.CQRS.Trainings
{
    public class ChangeTrainerCommandHandlerTests
    {
        private Mock<ITrainingRepository> _trainingRepositoryMock;
        private Mock<ITrainerRepository> _trainerRepositoryMock;

        public ChangeTrainerCommandHandlerTests()
        {
            _trainingRepositoryMock = TrainingRepositoryMock.GetTrainingRepository();
            _trainerRepositoryMock = TrainerRepositoryMock.GetTrainerRepository();
        }

        [Fact()]
        public async Task Handle_ForValidCommand_ReturnSuccessResponse()
        {
            // Arrange
            var items = await _trainingRepositoryMock.Object.GetAllAsync();
            var trainers = await _trainerRepositoryMock.Object.GetAllAsync();
            var handler = new ChangeTrainerCommandHandler(_trainingRepositoryMock.Object, _trainerRepositoryMock.Object);
            var command = new ChangeTrainerCommand()
            {
                Id = items.First().Id,
                NewTrainerId = trainers.First().Id
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            response.Success.Should().BeTrue();
        }

        [Fact()]
        public async Task Handle_TrainerChanged_ReturnNewTrainerId()
        {
            // Arrange
            var items = await _trainingRepositoryMock.Object.GetAllAsync();
            var trainers = await _trainerRepositoryMock.Object.GetAllAsync();
            var handler = new ChangeTrainerCommandHandler(_trainingRepositoryMock.Object, _trainerRepositoryMock.Object);
            var trainerIdBefore = (await _trainingRepositoryMock.Object.GetByIdAsync(items.Last().Id)).TrainerId;
            var command = new ChangeTrainerCommand()
            {
                Id = items.Last().Id,
                NewTrainerId = trainers.ElementAt(4).Id
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);
            var trainerIdAfter = (await _trainingRepositoryMock.Object.GetByIdAsync(items.Last().Id)).TrainerId;

            // Assert
            trainerIdAfter.Should().NotBe(trainerIdBefore);
        }
    }
}