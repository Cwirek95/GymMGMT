using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.CQRS.Trainings.Commands.ChangeTrainingDate;
using GymMGMT.Application.Tests.Mocks;

namespace GymMGMT.Application.Tests.CQRS.Trainings
{
    public class ChangeTrainingDateCommandValidatorTests
    {
        private Mock<ITrainingRepository> _trainingRepositoryMock;

        public ChangeTrainingDateCommandValidatorTests()
        {
            _trainingRepositoryMock = TrainingRepositoryMock.GetTrainingRepository();
        }

        [Fact()]
        public async Task Validate_ForEmptyStartDate_ReturnInvalidValidationAsync()
        {
            // Arrange
            var items = await _trainingRepositoryMock.Object.GetAllAsync();
            var validator = new ChangeTrainingDateCommandValidator();
            var command = new ChangeTrainingDateCommand()
            {
                Id = items.First().Id,
                EndDate = DateTimeOffset.Now.AddDays(-9),
            };

            // Act
            var response = validator.Validate(command);

            // Assert
            response.IsValid.Should().BeFalse();
        }

        [Fact()]
        public async Task Validate_ForEmptyEndDate_ReturnInvalidValidationAsync()
        {
            // Arrange
            var items = await _trainingRepositoryMock.Object.GetAllAsync();
            var validator = new ChangeTrainingDateCommandValidator();
            var command = new ChangeTrainingDateCommand()
            {
                Id = items.First().Id,
                StartDate = DateTimeOffset.Now.AddDays(-9),
            };

            // Act
            var response = validator.Validate(command);

            // Assert
            response.IsValid.Should().BeFalse();
        }
    }
}