using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.CQRS.Auth.Commands.ChangeRoleStatus;
using GymMGMT.Application.Tests.Mocks;

namespace GymMGMT.Application.Tests.CQRS.Auth.Role
{
    public class ChangeRoleStatusCommandHandlerTests
    {
        private Mock<IRoleRepository> _roleRepositoryMock;

        public ChangeRoleStatusCommandHandlerTests()
        {
            _roleRepositoryMock = RoleRepositoryMock.GetRoleRepository();
        }

        [Fact()]
        public async Task Handle_ForValidCommand_ReturnSuccessResponse()
        {
            // Arrange
            var items = await _roleRepositoryMock.Object.GetAllAsync();
            var handler = new ChangeRoleStatusCommandHandler(_roleRepositoryMock.Object);
            var command = new ChangeRoleStatusCommand()
            {
                Id = items.First().Id,
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            response.Success.Should().BeTrue();
        }

        [Fact()]
        public async Task Handle_StatusChanged_ReturnOppositeStatus()
        {
            // Arrange
            var items = await _roleRepositoryMock.Object.GetAllAsync();
            var handler = new ChangeRoleStatusCommandHandler(_roleRepositoryMock.Object);
            var statusBefore = (await _roleRepositoryMock.Object.GetByIdAsync(items.First().Id)).Status;
            var command = new ChangeRoleStatusCommand()
            {
                Id = items.First().Id,
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);
            var statusAfter = (await _roleRepositoryMock.Object.GetByIdAsync(items.First().Id)).Status;

            // Assert
            statusAfter.Should().NotBe(statusBefore);
        }
    }
}
