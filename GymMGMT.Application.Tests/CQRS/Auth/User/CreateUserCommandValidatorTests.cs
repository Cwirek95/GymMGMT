using GymMGMT.Application.CQRS.Auth.Commands.CreateUser;

namespace GymMGMT.Application.Tests.CQRS.Auth.User
{
    public class CreateUserCommandValidatorTests
    {
        [Fact()]
        public void Validate_ForEmptyEmail_ReturnInvalidValidation()
        {
            // Arrange
            var validator = new CreateUserCommandValidator();
            var command = new CreateUserCommand()
            {
                Email = "",
                Password = "12345678"
            };

            // Act
            var response = validator.Validate(command);

            // Assert
            response.IsValid.Should().BeFalse();
        }

        [Fact()]
        public void Validate_ForTooLongEmail_ReturnInvalidValidation()
        {
            // Arrange
            var validator = new CreateUserCommandValidator();
            var command = new CreateUserCommand()
            {
                Email = new string('A', 257),
                Password = "12345678"
            };

            // Act
            var response = validator.Validate(command);

            // Assert
            response.IsValid.Should().BeFalse();
        }

        [Fact()]
        public void Validate_ForEmptyPassword_ReturnInvalidValidation()
        {
            // Arrange
            var validator = new CreateUserCommandValidator();
            var command = new CreateUserCommand()
            {
                Email = "email@gym.com",
                Password = ""
            };

            // Act
            var response = validator.Validate(command);

            // Assert
            response.IsValid.Should().BeFalse();
        }

        [Fact()]
        public void Validate_ForTooShortPassword_ReturnInvalidValidation()
        {
            // Arrange
            var validator = new CreateUserCommandValidator();
            var command = new CreateUserCommand()
            {
                Email = "email@gym.com",
                Password = new string('A', 4)
            };

            // Act
            var response = validator.Validate(command);

            // Assert
            response.IsValid.Should().BeFalse();
        }

        [Fact()]
        public void Validate_ForTooLongPassword_ReturnInvalidValidation()
        {
            // Arrange
            var validator = new CreateUserCommandValidator();
            var command = new CreateUserCommand()
            {
                Email = "email@gym.com",
                Password = new string('A', 1025)
            };

            // Act
            var response = validator.Validate(command);

            // Assert
            response.IsValid.Should().BeFalse();
        }
    }
}
