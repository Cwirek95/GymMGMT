using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.CQRS.Trainings.Commands.ChangeTrainingPrice;
using GymMGMT.Application.Tests.Mocks;

namespace GymMGMT.Application.Tests.CQRS.Trainings
{
    public class ChangeTrainingPriceCommandValidatorTests
    {
        private Mock<ITrainingRepository> _trainingRepositoryMock;

        public ChangeTrainingPriceCommandValidatorTests()
        {
            _trainingRepositoryMock = TrainingRepositoryMock.GetTrainingRepository();
        }

        [Fact()]
        public async Task Validate_ForBadPriceFormat_ReturnInvalidValidationAsync()
        {
            // Arrange
            var items = await _trainingRepositoryMock.Object.GetAllAsync();
            var validator = new ChangeTrainingPriceCommandValidator();
            var command = new ChangeTrainingPriceCommand()
            {
                Id = items.First().Id,
                Price = 25.999
            };

            // Act
            var response = validator.Validate(command);

            // Assert
            response.IsValid.Should().BeFalse();
        }
    }
}