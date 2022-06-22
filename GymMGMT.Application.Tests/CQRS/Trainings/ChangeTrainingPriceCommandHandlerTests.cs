using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.CQRS.Trainings.Commands.ChangeTrainingPrice;
using GymMGMT.Application.Tests.Mocks;

namespace GymMGMT.Application.Tests.CQRS.Trainings
{
    public class ChangeTrainingPriceCommandHandlerTests
    {
        private Mock<ITrainingRepository> _trainingRepositoryMock;

        public ChangeTrainingPriceCommandHandlerTests()
        {
            _trainingRepositoryMock = TrainingRepositoryMock.GetTrainingRepository();
        }

        [Fact()]
        public async Task Handle_ForValidCommand_ReturnSuccessResponse()
        {
            // Arrange
            var items = await _trainingRepositoryMock.Object.GetAllAsync();
            var handler = new ChangeTrainingPriceCommandHandler(_trainingRepositoryMock.Object);
            var command = new ChangeTrainingPriceCommand()
            {
                Id = items.First().Id,
                Price = 39.99
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            response.Success.Should().BeTrue();
        }

        [Fact()]
        public async Task Handle_PriceChanged_ReturnNewPrice()
        {
            // Arrange
            var items = await _trainingRepositoryMock.Object.GetAllAsync();
            var handler = new ChangeTrainingPriceCommandHandler(_trainingRepositoryMock.Object);
            var priceBefore = (await _trainingRepositoryMock.Object.GetByIdAsync(items.Last().Id)).Price;
            var command = new ChangeTrainingPriceCommand()
            {
                Id = items.Last().Id,
                Price = 99.99
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);
            var priceAfter = (await _trainingRepositoryMock.Object.GetByIdAsync(items.Last().Id)).Price;

            // Assert
            priceAfter.Should().NotBe(priceBefore);
        }
    }
}