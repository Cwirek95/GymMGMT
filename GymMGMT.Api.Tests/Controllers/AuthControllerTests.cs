using GymMGMT.Api.Tests.Fakes;
using GymMGMT.Api.Tests.Helpers;
using GymMGMT.Application.CQRS.Auth.Commands.CreateUser;
using GymMGMT.Application.CQRS.Auth.Commands.SignInUser;
using GymMGMT.Domain.Entities;
using System.Net;

namespace GymMGMT.Api.Tests.Controllers
{
    public class AuthControllerTests : IClassFixture<ApiTestsServices>
    {
        private readonly ApiTestsServices _services;
        private readonly HttpClient _httpClient;

        public AuthControllerTests(ApiTestsServices services)
        {
            _services = services;
            _httpClient = _services.CreateClient();
        }

        [Fact]
        public async Task LogIn_ForValidCredentials_ReturnOKResponse()
        {
            // Arrange
            var role = new Role()
            {
                Id = Guid.NewGuid(),
                Name = "Role",
                Status = true
            };
            FakeDataSeed.SeedRole(role, _services);

            var user = new User()
            {
                Id = Guid.NewGuid(),
                Email = "userExist@email.com",
                Password = BCrypt.Net.BCrypt.HashPassword("12345"),
                RegisteredAt = DateTimeOffset.Now,
                RoleId = role.Id,
                Status = true
            };
            FakeDataSeed.SeedUser(user, _services);

            var model = new SignInUserCommand()
            {
                Email = "userExist@email.com",
                Password = "12345"
            };
            var httpContent = model.ToJsonHttpContent();

            // Act
            var response = await _httpClient.PostAsync("/api/login", httpContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task LogIn_ForInvalidCredentials_ReturnUnauthorizedResponse()
        {
            // Arrange
            var model = new SignInUserCommand()
            {
                Email = "userNotExist@email.com",
                Password = "12345"
            };
            var httpContent = model.ToJsonHttpContent();

            // Act
            var response = await _httpClient.PostAsync("/api/login", httpContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task Register_ForInvalidModel_ReturnUnprocessableEntityResponse()
        {
            // Arrange
            var model = new CreateUserCommand()
            {
                Email = "",
                Password = "12345"
            };
            var httpContent = model.ToJsonHttpContent();

            // Act
            var response = await _httpClient.PostAsync("/api/register", httpContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
        }

        [Fact]
        public async Task Register_ForExistingUser_ReturnConflictResponse()
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

            var model = new CreateUserCommand()
            {
                Email = "user@email.com",
                Password = "12345678"
            };
            var httpContent = model.ToJsonHttpContent();

            // Act
            var response = await _httpClient.PostAsync("/api/register", httpContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Conflict);
        }
    }
}