using GymMGMT.Api.Tests.Fakes;
using GymMGMT.Api.Tests.Helpers;
using GymMGMT.Application.CQRS.Auth.Commands.ChangePassword;
using GymMGMT.Application.CQRS.Auth.Commands.ChangeUserRole;
using GymMGMT.Application.CQRS.Auth.Commands.ChangeUserStatus;
using GymMGMT.Domain.Entities;
using System.Net;

namespace GymMGMT.Api.Tests.Controllers
{
    public class UsersControllerTests : IClassFixture<ApiTestsServices>
    {
        private readonly ApiTestsServices _services;
        private readonly HttpClient _httpClient;

        public UsersControllerTests(ApiTestsServices services)
        {
            _services = services;
            _httpClient = _services.CreateClient();
        }

        [Fact]
        public async Task GetAll_WithQueryParameters_ReturnOkResponse()
        {
            // Act
            var response = await _httpClient.GetAsync("/api/admin/users");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Detail_ForQueryParameters_ReturnOkResponse()
        {
            // Arrange
            var user = new User()
            {
                Id = Guid.NewGuid(),
                Email = "user@email.com",
                Password = BCrypt.Net.BCrypt.HashPassword("12345"),
                RegisteredAt = DateTimeOffset.Now,
                RoleId = Guid.NewGuid(),
                Status = true
            };
            FakeDataSeed.SeedUser(user, _services);

            // Act
            var response = await _httpClient.GetAsync("/api/admin/users/" + user.Id);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task ChangeStatus_ForValidModel_ReturnNoContentResponse()
        {
            // Arrange
            var user = new User()
            {
                Id = Guid.NewGuid(),
                Email = "user@email.com",
                Password = BCrypt.Net.BCrypt.HashPassword("12345"),
                RegisteredAt = DateTimeOffset.Now,
                RoleId = Guid.NewGuid(),
                Status = true,
            };
            FakeDataSeed.SeedUser(user, _services);

            var model = new ChangeUserStatusCommand()
            {
                Id = user.Id,
            };
            var httpContent = model.ToJsonHttpContent();

            // Act
            var response = await _httpClient.PutAsync("/api/admin/users/status", httpContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task ChangeUserRole_ForValidModel_ReturnNoContentResponse()
        {
            // Arrange
            var user = new User()
            {
                Id = Guid.NewGuid(),
                Email = "user@email.com",
                Password = BCrypt.Net.BCrypt.HashPassword("12345"),
                RegisteredAt = DateTimeOffset.Now,
                RoleId = Guid.NewGuid(),
                Status = true
            };
            FakeDataSeed.SeedUser(user, _services);

            var role = new Role()
            {
                Id = Guid.NewGuid(),
                Name = "Role1",
                Status = true
            };
            FakeDataSeed.SeedRole(role, _services);

            var model = new ChangeUserRoleCommand()
            {
                UserId = user.Id,
                RoleId= role.Id
            };
            var httpContent = model.ToJsonHttpContent();

            // Act
            var response = await _httpClient.PutAsync("/api/admin/users/role", httpContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task ChangeUserRole_ForNonExistingUser_ReturnNotFoundResponse()
        {
            // Arrange
            var model = new ChangeUserRoleCommand()
            {
                UserId = Guid.NewGuid(),
                RoleId = Guid.NewGuid(),
            };
            var httpContent = model.ToJsonHttpContent();

            // Act
            var response = await _httpClient.PutAsync("/api/admin/users/role", httpContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task ChangePassword_ForValidModel_ReturnNoContentResponse()
        {
            // Arrange
            var user = new User()
            {
                Id = Guid.NewGuid(),
                Email = "user@email.com",
                Password = BCrypt.Net.BCrypt.HashPassword("12345"),
                RegisteredAt = DateTimeOffset.Now,
                RoleId = Guid.NewGuid(),
                Status = true
            };
            FakeDataSeed.SeedUser(user, _services);

            var model = new ChangePasswordCommand()
            {
                Id = user.Id,
                OldPassword = "12345",
                NewPassword = "12345678"
            };
            var httpContent = model.ToJsonHttpContent();

            // Act
            var response = await _httpClient.PutAsync("/api/admin/users/password", httpContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task ChangePassword_ForNewPasswordSameAsCurrent_ReturnConflictResponse()
        {
            // Arrange
            var user = new User()
            {
                Id = Guid.NewGuid(),
                Email = "user@email.com",
                Password = BCrypt.Net.BCrypt.HashPassword("12345"),
                RegisteredAt = DateTimeOffset.Now,
                RoleId = Guid.NewGuid(),
                Status = true
            };
            FakeDataSeed.SeedUser(user, _services);

            var model = new ChangePasswordCommand()
            {
                Id = user.Id,
                OldPassword = "12345",
                NewPassword = "12345"
            };
            var httpContent = model.ToJsonHttpContent();

            // Act
            var response = await _httpClient.PutAsync("/api/admin/users/password", httpContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Conflict);
        }
    }
}