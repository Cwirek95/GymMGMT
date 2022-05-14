using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.CQRS.Auth.Commands.UpdateRole;
using GymMGMT.Application.Tests.Mocks;

namespace GymMGMT.Application.Tests.CQRS.Auth.Role
{
    public class UpdateRoleCommandHandlerTests
    {
        private Mock<IRoleRepository> _roleRepositoryMock;

        public UpdateRoleCommandHandlerTests()
        {
            _roleRepositoryMock = RoleRepositoryMock.GetRoleRepository();
        }

        [Fact()]
        public async Task Handle_ForValidCommand_ReturnSuccessResponse()
        {
            // Arrange
            var items = await _roleRepositoryMock.Object.GetAllAsync();
            var handler = new UpdateRoleCommandHandler(_roleRepositoryMock.Object);
            var command = new UpdateRoleCommand()
            {
                Id = items.First().Id,
                Name = "UpdateRoleName"
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            response.Success.Should().BeTrue();
        }

        [Fact()]
        public async Task Handle_UpdateRoleName_ReturnRoleWithNewName()
        {
            // Arrange
            var items = await _roleRepositoryMock.Object.GetAllAsync();
            var handler = new UpdateRoleCommandHandler(_roleRepositoryMock.Object);
            var existItemName = (await _roleRepositoryMock.Object.GetByIdAsync(items.First().Id)).Name;
            var command = new UpdateRoleCommand()
            {
                Id = items.First().Id,
                Name = "UpdateRoleName"
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);
            var updatedItem = await _roleRepositoryMock.Object.GetByIdAsync(items.First().Id);

            // Assert
            updatedItem.Name.Should().NotBe(existItemName);
        }
    }
}
