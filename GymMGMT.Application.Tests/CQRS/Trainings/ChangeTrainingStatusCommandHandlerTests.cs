using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.CQRS.Trainings.Commands.ChangeTrainingStatus;
using GymMGMT.Application.Tests.Mocks;

namespace GymMGMT.Application.Tests.CQRS.Trainings
{
    public class ChangeTrainingStatusCommandHandlerTests
    {
        private Mock<ITrainingRepository> _trainingRepositoryMock;

        public ChangeTrainingStatusCommandHandlerTests()
        {
            _trainingRepositoryMock = TrainingRepositoryMock.GetTrainingRepository();
        }

        [Fact()]
        public async Task Handle_ForValidCommand_ReturnSuccessResponse()
        {
            // Arrange
            var items = await _trainingRepositoryMock.Object.GetAllAsync();
            var handler = new ChangeTrainingStatusCommandHandler(_trainingRepositoryMock.Object);
            var command = new ChangeTrainingStatusCommand()
            {
                Id = items.First().Id,
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            response.Success.Should().BeTrue();
        }

        [Fact()]
        public async Task Handle_StatusChanged_ReturnOppositeStatus()
        {
            // Arrange
            var items = await _trainingRepositoryMock.Object.GetAllAsync();
            var handler = new ChangeTrainingStatusCommandHandler(_trainingRepositoryMock.Object);
            var statusBefore = (await _trainingRepositoryMock.Object.GetByIdAsync(items.Last().Id)).Status;
            var command = new ChangeTrainingStatusCommand()
            {
                Id = items.Last().Id,
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);
            var statusAfter = (await _trainingRepositoryMock.Object.GetByIdAsync(items.Last().Id)).Status;

            // Assert
            statusAfter.Should().NotBe(statusBefore);
        }
    }
}