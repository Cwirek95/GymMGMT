using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.CQRS.Auth.Commands.ChangeUserRole;
using GymMGMT.Application.Security.Contracts;
using GymMGMT.Application.Tests.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMGMT.Application.Tests.CQRS.Auth.User
{
    public class ChangeUserRoleCommandHandlerTests
    {
        private Mock<IAuthenticationService> _authenticationServiceMock;
        private Mock<IUserRepository> _userRepositoryMock;

        public ChangeUserRoleCommandHandlerTests()
        {
            _authenticationServiceMock = UserRepositoryServiceMock.GetAuthService();
            _userRepositoryMock = UserRepositoryServiceMock.GetUserRepository();
        }

        [Fact]
        public async Task Handle_ForValidCommand_ReturnSuccessResponse()
        {
            // Arrange
            var items = await _userRepositoryMock.Object.GetAllAsync();
            var handler = new ChangeUserRoleCommandHandler(_authenticationServiceMock.Object, _userRepositoryMock.Object);
            var command = new ChangeUserRoleCommand()
            {
                UserId = items.First().Id,
                RoleId = Guid.NewGuid()
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            response.Success.Should().BeTrue();
        }

        [Fact]
        public async Task Handle_RoleChanged_ReturnNewRole()
        {
            // Arrange
            var items = await _userRepositoryMock.Object.GetAllAsync();
            var roleBefore = (await _userRepositoryMock.Object.GetByIdAsync(items.First().Id)).RoleId;
            var handler = new ChangeUserRoleCommandHandler(_authenticationServiceMock.Object, _userRepositoryMock.Object);
            var command = new ChangeUserRoleCommand()
            {
                UserId = items.First().Id,
                RoleId = Guid.NewGuid()
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);
            var roleAfter = (await _userRepositoryMock.Object.GetByIdAsync(items.First().Id)).RoleId;

            // Assert
            roleBefore.ToString().Should().NotBe(roleAfter.ToString());
        }

        [Fact]
        public async Task Handle_RoleChangedToTheSame_ReturnTheSameRole()
        {
            // Arrange
            var items = await _userRepositoryMock.Object.GetAllAsync();
            var roleBefore = (await _userRepositoryMock.Object.GetByIdAsync(items.First().Id)).RoleId;
            var handler = new ChangeUserRoleCommandHandler(_authenticationServiceMock.Object, _userRepositoryMock.Object);
            var command = new ChangeUserRoleCommand()
            {
                UserId = items.First().Id,
                RoleId = (Guid)items.First().RoleId
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);
            var roleAfter = (await _userRepositoryMock.Object.GetByIdAsync(items.First().Id)).RoleId;

            // Assert
            roleBefore.ToString().Should().Be(roleAfter.ToString());
        }
    }
}
