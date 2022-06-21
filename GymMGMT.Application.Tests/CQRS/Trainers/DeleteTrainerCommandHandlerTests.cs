using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.CQRS.Trainers.Commands.DeleteTrainer;
using GymMGMT.Application.Tests.Mocks;

namespace GymMGMT.Application.Tests.CQRS.Trainers
{
    public class DeleteTrainerCommandHandlerTests
    {
        private Mock<ITrainerRepository> _trainerRepositoryMock;

        public DeleteTrainerCommandHandlerTests()
        {
            _trainerRepositoryMock = TrainerRepositoryMock.GetTrainerRepository();
        }

        [Fact()]
        public async Task Handle_ForValidCommand_ReturnSuccessResponse()
        {
            // Arrange
            var items = await _trainerRepositoryMock.Object.GetAllAsync();
            var handler = new DeleteTrainerCommandHandler(_trainerRepositoryMock.Object);
            var command = new DeleteTrainerCommand()
            {
                Id = items.ToList().ElementAt(5).Id
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            response.Success.Should().BeTrue();
        }

        [Fact()]
        public async Task Handle_TrainerDeleted_ReturnOneLessTrainers()
        {
            // Arrange
            var items = await _trainerRepositoryMock.Object.GetAllAsync();
            var handler = new DeleteTrainerCommandHandler(_trainerRepositoryMock.Object);
            var countBefore = (await _trainerRepositoryMock.Object.GetAllAsync()).Count();
            var command = new DeleteTrainerCommand()
            {
                Id = items.ToList().ElementAt(5).Id
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);
            var countAfter = (await _trainerRepositoryMock.Object.GetAllAsync()).Count();

            // Assert
            countAfter.Should().Be(countBefore - 1);
        }
    }
}