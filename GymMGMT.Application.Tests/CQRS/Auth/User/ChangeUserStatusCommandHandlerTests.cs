using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.CQRS.Auth.Commands.ChangeUserStatus;
using GymMGMT.Application.Tests.Mocks;

namespace GymMGMT.Application.Tests.CQRS.Auth.User
{
    public class ChangeUserStatusCommandHandlerTests
    {
        private Mock<IUserRepository> _userRepositoryMock;

        public ChangeUserStatusCommandHandlerTests()
        {
            _userRepositoryMock = UserRepositoryServiceMock.GetUserRepository();
        }

        [Fact()]
        public async Task Handle_ForValidCommand_ReturnSuccessResponse()
        {
            // Arrange
            var items = await _userRepositoryMock.Object.GetAllAsync();
            var handler = new ChangeUserStatusCommandHandler(_userRepositoryMock.Object);
            var command = new ChangeUserStatusCommand()
            {
                Id = items.First().Id
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
            var items = await _userRepositoryMock.Object.GetAllAsync();
            var handler = new ChangeUserStatusCommandHandler(_userRepositoryMock.Object);
            var statusBefore = (await _userRepositoryMock.Object.GetByIdAsync(items.First().Id)).Status;
            var command = new ChangeUserStatusCommand()
            {
                Id = items.First().Id
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);
            var statusAfter = (await _userRepositoryMock.Object.GetByIdAsync(items.First().Id)).Status;

            // Assert
            statusAfter.Should().NotBe(statusBefore);
        }
    }
}
