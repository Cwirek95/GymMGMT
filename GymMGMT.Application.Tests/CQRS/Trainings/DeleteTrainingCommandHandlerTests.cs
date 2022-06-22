using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.CQRS.Trainings.Commands.DeleteTraining;
using GymMGMT.Application.Tests.Mocks;

namespace GymMGMT.Application.Tests.CQRS.Trainings
{
    public class DeleteTrainingCommandHandlerTests
    {
        private Mock<ITrainingRepository> _trainingRepositoryMock;

        public DeleteTrainingCommandHandlerTests()
        {
            _trainingRepositoryMock = TrainingRepositoryMock.GetTrainingRepository();
        }

        [Fact()]
        public async Task Handle_ForValidCommand_ReturnSuccessResponse()
        {
            // Arrange
            var items = await _trainingRepositoryMock.Object.GetAllAsync();
            var handler = new DeleteTrainingCommandHandler(_trainingRepositoryMock.Object);
            var command = new DeleteTrainingCommand()
            {
                Id = items.ToList().ElementAt(5).Id
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            response.Success.Should().BeTrue();
        }

        [Fact()]
        public async Task Handle_TrainingDeleted_ReturnOneLessTrainings()
        {
            // Arrange
            var items = await _trainingRepositoryMock.Object.GetAllAsync();
            var handler = new DeleteTrainingCommandHandler(_trainingRepositoryMock.Object);
            var countBefore = (await _trainingRepositoryMock.Object.GetAllAsync()).Count();
            var command = new DeleteTrainingCommand()
            {
                Id = items.ToList().ElementAt(3).Id
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);
            var countAfter = (await _trainingRepositoryMock.Object.GetAllAsync()).Count();

            // Assert
            countAfter.Should().Be(countBefore - 1);
        }
    }
}