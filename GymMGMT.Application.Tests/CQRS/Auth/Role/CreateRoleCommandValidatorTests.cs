using GymMGMT.Application.CQRS.Auth.Commands.CreateRole;

namespace GymMGMT.Application.Tests.CQRS.Auth.Role
{
    public class CreateRoleCommandValidatorTests
    {
        [Fact()]
        public void Validate_ForEmptyName_ReturnInvalidValidation()
        {
            // Arrange
            var validator = new CreateRoleCommandValidator();
            var command = new CreateRoleCommand()
            {
                Name = ""
            };

            // Act
            var response = validator.Validate(command);

            // Assert
            response.IsValid.Should().BeFalse();
        }

        [Fact()]
        public void Validate_ForTooLongName_ReturnInvalidValidation()
        {
            // Arrange
            var validator = new CreateRoleCommandValidator();
            var command = new CreateRoleCommand()
            {
                Name = new string('A', 257)
            };

            // Act
            var response = validator.Validate(command);

            // Assert
            response.IsValid.Should().BeFalse();
        }
    }
}
