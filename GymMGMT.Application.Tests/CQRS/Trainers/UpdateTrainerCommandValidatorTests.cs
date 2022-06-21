using GymMGMT.Application.CQRS.Trainers.Commands.UpdateTrainer;

namespace GymMGMT.Application.Tests.CQRS.Trainers
{
    public class UpdateTrainerCommandValidatorTests
    {
        [Fact()]
        public void Validate_ForEmptyFirstName_ReturnInvalidValidation()
        {
            // Arrange
            var validator = new UpdateTrainerCommandValidator();
            var command = new UpdateTrainerCommand()
            {
                FirstName = "",
                LastName = "LName"
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
            var validator = new UpdateTrainerCommandValidator();
            var command = new UpdateTrainerCommand()
            {
                FirstName = new string('A', 257),
                LastName = "LName"
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
            var validator = new UpdateTrainerCommandValidator();
            var command = new UpdateTrainerCommand()
            {
                FirstName = "FName",
                LastName = ""
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
            var validator = new UpdateTrainerCommandValidator();
            var command = new UpdateTrainerCommand()
            {
                FirstName = "FName",
                LastName = new string('A', 257)
            };

            // Act
            var response = validator.Validate(command);

            // Assert
            response.IsValid.Should().BeFalse();
        }
    }
}