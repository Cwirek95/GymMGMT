using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.CQRS.Trainings.Commands.ChangeTrainingType;
using GymMGMT.Application.Tests.Mocks;

namespace GymMGMT.Application.Tests.CQRS.Trainings
{
    public class ChangeTrainingTypeCommandHandlerTests
    {
        private Mock<ITrainingRepository> _trainingRepositoryMock;

        public ChangeTrainingTypeCommandHandlerTests()
        {
            _trainingRepositoryMock = TrainingRepositoryMock.GetTrainingRepository();
        }

        [Fact()]
        public async Task Handle_ForValidCommand_ReturnSuccessResponse()
        {
            // Arrange
            var items = await _trainingRepositoryMock.Object.GetAllAsync();
            var handler = new ChangeTrainingTypeCommandHandler(_trainingRepositoryMock.Object);
            var command = new ChangeTrainingTypeCommand()
            {
                Id = items.First().Id,
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            response.Success.Should().BeTrue();
        }

        [Fact()]
        public async Task Handle_TrainingTypeChanged_ReturnNewTrainingType()
        {
            // Arrange
            var items = await _trainingRepositoryMock.Object.GetAllAsync();
            var handler = new ChangeTrainingTypeCommandHandler(_trainingRepositoryMock.Object);
            var typeBefore = (await _trainingRepositoryMock.Object.GetByIdAsync(items.Last().Id)).TrainingType;
            var command = new ChangeTrainingTypeCommand()
            {
                Id = items.Last().Id,
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);
            var typeAfter = (await _trainingRepositoryMock.Object.GetByIdAsync(items.Last().Id)).TrainingType;

            // Assert
            typeAfter.Should().NotBe(typeBefore);
        }
    }
}