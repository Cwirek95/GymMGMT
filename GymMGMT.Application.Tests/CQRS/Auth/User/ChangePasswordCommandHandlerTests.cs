using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.CQRS.Auth.Commands.ChangePassword;
using GymMGMT.Application.Security.Contracts;
using GymMGMT.Application.Tests.Mocks;

namespace GymMGMT.Application.Tests.CQRS.Auth.User
{
    public class ChangePasswordCommandHandlerTests
    {
        private Mock<IAuthenticationService> _authenticationServiceMock;
        private Mock<IUserRepository> _userRepositoryMock;

        public ChangePasswordCommandHandlerTests()
        {
            _authenticationServiceMock = UserRepositoryServiceMock.GetAuthService();
            _userRepositoryMock = UserRepositoryServiceMock.GetUserRepository();
        }

        [Theory()]
        [InlineData("newPassword1")]
        [InlineData("pass9#4921")]
        [InlineData("$Fdf4^%3sd$")]
        public async Task Handle_ForValidCommand_ReturnSuccessResponse(string newPassword)
        {
            // Arrange
            var items = await _userRepositoryMock.Object.GetAllAsync();
            var handler = new ChangePasswordCommandHandler(_authenticationServiceMock.Object);
            var command = new ChangePasswordCommand()
            {
                Id = items.First().Id,
                OldPassword = items.First().Password,
                NewPassword = newPassword
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            response.Success.Should().BeTrue();
        }

        [Theory()]
        [InlineData("newPass123")]
        [InlineData("newPass987")]
        [InlineData("newPa$%%23")]
        public async Task Handle_PasswordChanged_ReturnNewPassword(string newPassword)
        {
            // Arrange
            var items = await _userRepositoryMock.Object.GetAllAsync();
            var handler = new ChangePasswordCommandHandler(_authenticationServiceMock.Object);
            var command = new ChangePasswordCommand()
            {
                Id = items.First().Id,
                OldPassword = items.First().Password,
                NewPassword = newPassword
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);
            var changedPassword = (await _userRepositoryMock.Object.GetByIdAsync(items.First().Id)).Password;

            // Assert
            changedPassword.Should().Be(newPassword);
        }
    }
}
