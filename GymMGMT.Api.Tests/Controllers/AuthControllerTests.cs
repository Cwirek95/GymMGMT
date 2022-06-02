using GymMGMT.Api.Tests.Helpers;
using GymMGMT.Application.CQRS.Auth.Commands.CreateUser;
using GymMGMT.Application.CQRS.Auth.Commands.SignInUser;
using GymMGMT.Domain.Entities;
using GymMGMT.Persistence.EF;
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
            var user = new User()
            {
                Id = Guid.NewGuid(),
                Email = "userExist@email.com",
                Password = BCrypt.Net.BCrypt.HashPassword("12345"),
                RegisteredAt = DateTimeOffset.Now,
                RoleId = Guid.NewGuid(),
                Status = true
            };
            SeedUser(user);

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
            SeedUser(user);

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

        private void SeedUser(User user)
        {
            var scopeFactory = _services.Services.GetService<IServiceScopeFactory>();
            using var scope = scopeFactory.CreateScope();
            var _dbContext = scope.ServiceProvider.GetService<AppDbContext>();

            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }
    }
}
