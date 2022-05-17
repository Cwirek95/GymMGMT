using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.CQRS.Auth.Commands.CreateUser;
using GymMGMT.Application.Security.Contracts;
using GymMGMT.Application.Tests.Mocks;

namespace GymMGMT.Application.Tests.CQRS.Auth.User
{
    public class CreateUserCommandHandlerTests
    {
        private Mock<IAuthenticationService> _authenticationServiceMock;
        private Mock<IUserRepository> _userRepositoryMock;

        public CreateUserCommandHandlerTests()
        {
            _authenticationServiceMock = UserRepositoryServiceMock.GetAuthService();
            _userRepositoryMock = UserRepositoryServiceMock.GetUserRepository();
        }

        [Fact()]
        public async Task Handle_ForValidCommand_ReturnSuccessResponse()
        {
            // Arrange
            var handler = new CreateUserCommandHandler(_authenticationServiceMock.Object);
            var command = new CreateUserCommand()
            {
                Email = "email@gym.com",
                Password = "12345678"
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            response.Success.Should().BeTrue();
        }

        [Fact()]
        public async Task Handle_ForValidCommand_ReturnOneMoreUsers()
        {
            // Arrange
            var handler = new CreateUserCommandHandler(_authenticationServiceMock.Object);
            var countBefore = (await _userRepositoryMock.Object.GetAllAsync()).Count;
            var command = new CreateUserCommand()
            {
                Email = "email@gym.com",
                Password = "12345678"
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);
            var countAfter = (await _userRepositoryMock.Object.GetAllAsync()).Count;

            // Assert
            countAfter.Should().Be(countBefore + 1);
        }
    }
}
