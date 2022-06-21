using GymMGMT.Application.CQRS.Trainers.Commands.AddTrainer;

namespace GymMGMT.Application.Tests.CQRS.Trainers
{
    public class AddTrainerCommandValidatorTests
    {
        [Fact()]
        public void Validate_ForEmptyFirstName_ReturnInvalidValidation()
        {
            // Arrange
            var validator = new AddTrainerCommandValidator();
            var command = new AddTrainerCommand()
            {
                FirstName = "",
                LastName = "LName",
                UserId = Guid.NewGuid(),
            };

            // Act
            var response = validator.Validate(command);

            // Assert
            response.IsValid.Should().BeFalse();
        }

        [Fact()]
        public void Validate_ForTooLongFirstName_ReturnInvalidValidation()
        {
            // Arrange
            var validator = new AddTrainerCommandValidator();
            var command = new AddTrainerCommand()
            {
                FirstName = new string('A', 257),
                LastName = "LName",
                UserId = Guid.NewGuid(),
            };

            // Act
            var response = validator.Validate(command);

            // Assert
            response.IsValid.Should().BeFalse();
        }

        [Fact()]
        public void Validate_ForEmptyLastName_ReturnInvalidValidation()
        {
            // Arrange
            var validator = new AddTrainerCommandValidator();
            var command = new AddTrainerCommand()
            {
                FirstName = "FName",
                LastName = "",
                UserId = Guid.NewGuid(),
            };

            // Act
            var response = validator.Validate(command);

            // Assert
            response.IsValid.Should().BeFalse();
        }

        [Fact()]
        public void Validate_ForTooLongLastName_ReturnInvalidValidation()
        {
            // Arrange
            var validator = new AddTrainerCommandValidator();
            var command = new AddTrainerCommand()
            {
                FirstName = "FName",
                LastName = new string('A', 257),
                UserId = Guid.NewGuid(),
            };

            // Act
            var response = validator.Validate(command);

            // Assert
            response.IsValid.Should().BeFalse();
        }
    }
}