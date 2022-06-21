using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.CQRS.Trainers.Commands.ChangeTrainerStatus;
using GymMGMT.Application.Tests.Mocks;

namespace GymMGMT.Application.Tests.CQRS.Trainers
{
    public class ChangeTrainerStatusCommandHandlerTests
    {
        private Mock<ITrainerRepository> _trainerRepositoryMock;

        public ChangeTrainerStatusCommandHandlerTests()
        {
            _trainerRepositoryMock = TrainerRepositoryMock.GetTrainerRepository();
        }

        [Fact()]
        public async Task Handle_ForValidCommand_ReturnSuccessResponse()
        {
            // Arrange
            var items = await _trainerRepositoryMock.Object.GetAllAsync();
            var handler = new ChangeTrainerStatusCommandHandler(_trainerRepositoryMock.Object);
            var command = new ChangeTrainerStatusCommand()
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
            var items = await _trainerRepositoryMock.Object.GetAllAsync();
            var handler = new ChangeTrainerStatusCommandHandler(_trainerRepositoryMock.Object);
            var statusBefore = (await _trainerRepositoryMock.Object.GetByIdAsync(items.Last().Id)).Status;
            var command = new ChangeTrainerStatusCommand()
            {
                Id = items.Last().Id,
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);
            var statusAfter = (await _trainerRepositoryMock.Object.GetByIdAsync(items.Last().Id)).Status;

            // Assert
            statusAfter.Should().NotBe(statusBefore);
        }
    }
}