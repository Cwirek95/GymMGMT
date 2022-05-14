using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.CQRS.Auth.Commands.UpdateRole;
using GymMGMT.Application.Tests.Mocks;

namespace GymMGMT.Application.Tests.CQRS.Auth.Role
{
    public class UpdateRoleCommandValidatorTests
    {
        private Mock<IRoleRepository> _roleRepositoryMock;

        public UpdateRoleCommandValidatorTests()
        {
            _roleRepositoryMock = RoleRepositoryMock.GetRoleRepository();
        }

        [Fact()]
        public async Task Validate_ForEmptyName_ReturnInvalidValidation()
        {
            // Arrange
            var validator = new UpdateRoleCommandValidator();
            var items = await _roleRepositoryMock.Object.GetAllAsync();
            var command = new UpdateRoleCommand()
            {
                Id = items.First().Id,
                Name = ""
            };

            // Act
            var response = validator.Validate(command);

            // Assert
            response.IsValid.Should().BeFalse();
        }

        [Fact()]
        public async Task Validate_ForTooLongName_ReturnInvalidValidation()
        {
            // Arrange
            var validator = new UpdateRoleCommandValidator();
            var items = await _roleRepositoryMock.Object.GetAllAsync();
            var command = new UpdateRoleCommand()
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
