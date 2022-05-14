using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.CQRS.Auth.Commands.DeleteRole;
using GymMGMT.Application.Tests.Mocks;

namespace GymMGMT.Application.Tests.CQRS.Auth.Role
{
    public class DeleteRoleCommandHandlerTests
    {
        private readonly Mock<IRoleRepository> _roleRepositoryMock;

        public DeleteRoleCommandHandlerTests()
        {
            _roleRepositoryMock = RoleRepositoryMock.GetRoleRepository();
        }

        [Fact()]
        public async Task Handle_ForValidCommand_ReturnSuccessResponse()
        {
            // Arrange
            var handler = new DeleteRoleCommandHandler(_roleRepositoryMock.Object);
            var items = await _roleRepositoryMock.Object.GetAllAsync();
            var command = new DeleteRoleCommand()
            {
                Id = items.First().Id
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            response.Success.Should().BeTrue();
        }

        [Fact()]
        public async Task Handle_DeleteRole_ReturnOneLessRoles()
        {
            // Arrange
            var handler = new DeleteRoleCommandHandler(_roleRepositoryMock.Object);
            var items = await _roleRepositoryMock.Object.GetAllAsync();
            var countBefore = (await _roleRepositoryMock.Object.GetAllAsync()).Count();
            var command = new DeleteRoleCommand()
            {
                Id = items.Last().Id
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);
            var countAfter = (await _roleRepositoryMock.Object.GetAllAsync()).Count();

            // Assert
            countAfter.Should().Be(countBefore - 1);
        }
    }
}
