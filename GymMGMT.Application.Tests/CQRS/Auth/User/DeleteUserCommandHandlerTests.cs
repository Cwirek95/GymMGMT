using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.CQRS.Auth.Commands.DeleteUser;
using GymMGMT.Application.Security.Contracts;
using GymMGMT.Application.Tests.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMGMT.Application.Tests.CQRS.Auth.User
{
    public class DeleteUserCommandHandlerTests
    {
        private Mock<IAuthenticationService> _authenticationServiceMock;
        private Mock<IUserRepository> _userRepositoryMock;

        public DeleteUserCommandHandlerTests()
        {
            _authenticationServiceMock = UserRepositoryServiceMock.GetAuthService();
            _userRepositoryMock = UserRepositoryServiceMock.GetUserRepository();
        }

        [Fact()]
        public async Task Handle_ForValidCommand_ReturnSuccessResponse()
        {
            // Arrange
            var handler = new DeleteUserCommandHandler(_authenticationServiceMock.Object);
            var items = await _userRepositoryMock.Object.GetAllAsync();
            var command = new DeleteUserCommand()
            {
                Id = items.ToList().ElementAt(5).Id
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            response.Success.Should().BeTrue();
        }

        [Fact()]
        public async Task Handle_DeleteUser_ReturnOneLessUsers()
        {
            // Arrange
            var handler = new DeleteUserCommandHandler(_authenticationServiceMock.Object);
            var items = await _userRepositoryMock.Object.GetAllAsync();
            var countBefore = (await _userRepositoryMock.Object.GetAllAsync()).Count();
            var command = new DeleteUserCommand()
            {
                Id = items.ToList().ElementAt(3).Id
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);
            var countAfter = (await _userRepositoryMock.Object.GetAllAsync()).Count();

            // Assert
            countAfter.Should().Be(countBefore - 1);
        }
    }
}
