using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.CQRS.Trainings.Commands.AddTraining;
using GymMGMT.Application.Tests.Mocks;
using GymMGMT.Domain.Enums;

namespace GymMGMT.Application.Tests.CQRS.Trainings
{
    public class AddTrainingCommandValidatorTests
    {
        private Mock<ITrainingRepository> _trainingRepositoryMock;
        private Mock<ITrainerRepository> _trainerRepositoryMock;

        public AddTrainingCommandValidatorTests()
        {
            _trainingRepositoryMock = TrainingRepositoryMock.GetTrainingRepository();
            _trainerRepositoryMock = TrainerRepositoryMock.GetTrainerRepository();
        }

        [Fact()]
        public async Task Validate_ForEmptyStartDate_ReturnInvalidValidationAsync()
        {
            // Arrange
            var items = await _trainerRepositoryMock.Object.GetAllAsync();
            var validator = new AddTrainingCommandValidator();
            var command = new AddTrainingCommand()
            {
                EndDate = DateTimeOffset.Now.AddDays(-9),
                Price = 25,
                TrainingType = TrainingType.INDIVIDUAL,
                TrainerId = items.First().Id
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
            var items = await _trainerRepositoryMock.Object.GetAllAsync();
            var validator = new AddTrainingCommandValidator();
            var command = new AddTrainingCommand()
            {
                StartDate = DateTimeOffset.Now.AddDays(-10),
                Price = 25,
                TrainingType = TrainingType.INDIVIDUAL,
                TrainerId = items.First().Id
            };

            // Act
            var response = validator.Validate(command);

            // Assert
            response.IsValid.Should().BeFalse();
        }

        [Fact()]
        public async Task Validate_ForStartDateAfterEndDate_ReturnInvalidValidationAsync()
        {
            // Arrange
            var items = await _trainerRepositoryMock.Object.GetAllAsync();
            var validator = new AddTrainingCommandValidator();
            var command = new AddTrainingCommand()
            {
                StartDate = DateTimeOffset.Now.AddDays(-10),
                EndDate = DateTimeOffset.Now.AddDays(-11),
                Price = 25,
                TrainingType = TrainingType.INDIVIDUAL,
                TrainerId = items.First().Id
            };

            // Act
            var response = validator.Validate(command);

            // Assert
            response.IsValid.Should().BeFalse();
        }

        [Fact()]
        public async Task Validate_ForBadPriceFormat_ReturnInvalidValidationAsync()
        {
            // Arrange
            var items = await _trainerRepositoryMock.Object.GetAllAsync();
            var validator = new AddTrainingCommandValidator();
            var command = new AddTrainingCommand()
            {
                StartDate = DateTimeOffset.Now.AddDays(-10),
                Price = 25.999,
                TrainingType = TrainingType.INDIVIDUAL,
                TrainerId = items.First().Id
            };

            // Act
            var response = validator.Validate(command);

            // Assert
            response.IsValid.Should().BeFalse();
        }
    }
}