using GymMGMT.Api.Tests.Helpers;
using GymMGMT.Application.CQRS.Auth.Commands.ChangeRoleStatus;
using GymMGMT.Application.CQRS.Auth.Commands.CreateRole;
using GymMGMT.Application.CQRS.Auth.Commands.DeleteRole;
using GymMGMT.Application.CQRS.Auth.Commands.UpdateRole;
using GymMGMT.Domain.Entities;
using GymMGMT.Persistence.EF;
using System.Net;

namespace GymMGMT.Api.Tests.Controllers
{
    public class RolesControllerTests : IClassFixture<ApiTestsServices>
    {
        private readonly ApiTestsServices _services;
        private readonly HttpClient _httpClient;

        public RolesControllerTests(ApiTestsServices services)
        {
            _services = services;
            _httpClient = _services.CreateClient();
        }

        [Fact]
        public async Task Create_ForValidModel_ReturnOkResponse()
        {
            // Arrange
            var model = new CreateRoleCommand()
            {
                Name = "Role1",
            };
            var httpContent = model.ToJsonHttpContent();

            // Act
            var response = await _httpClient.PostAsync("/api/admin/roles", httpContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Create_ForInvalidModel_ReturnUnprocessableEntityResponse()
        {
            // Arrange
            var model = new CreateRoleCommand()
            {
                Name = "",
            };
            var httpContent = model.ToJsonHttpContent();

            // Act
            var response = await _httpClient.PostAsync("/api/admin/roles", httpContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
        }

        [Fact]
        public async Task Update_ForValidModel_ReturnNoContentResponse()
        {
            // Arrange
            var role = new Role()
            {
                Id = Guid.NewGuid(),
                Name = "Role1",
                Status = true
            };
            SeedRole(role);

            var model = new UpdateRoleCommand()
            {
                Id = role.Id,
                Name = "RoleUpdated",
            };
            var httpContent = model.ToJsonHttpContent();

            // Act
            var response = await _httpClient.PutAsync("/api/admin/roles", httpContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task Update_ForNonExistingRole_ReturnNotFoundResponse()
        {
            // Arrange
            var model = new UpdateRoleCommand()
            {
                Id = Guid.NewGuid(),
                Name = "RoleUpdated",
            };
            var httpContent = model.ToJsonHttpContent();

            // Act
            var response = await _httpClient.PutAsync("/api/admin/roles", httpContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task ChangeStatus_ForValidModel_ReturnOkResponse()
        {
            // Arrange
            var role = new Role()
            {
                Id = Guid.NewGuid(),
                Name = "Role1",
                Status = true
            };
            SeedRole(role);

            var model = new ChangeRoleStatusCommand()
            {
                Id = role.Id,
            };
            var httpContent = model.ToJsonHttpContent();

            // Act
            var response = await _httpClient.PutAsync("/api/admin/roles/changeStatus", httpContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        private void SeedRole(Role role)
        {
            var scopeFactory = _services.Services.GetService<IServiceScopeFactory>();
            using var scope = scopeFactory.CreateScope();
            var _dbContext = scope.ServiceProvider.GetService<AppDbContext>();

            _dbContext.Roles.Add(role);
            _dbContext.SaveChanges();
        }
    }
}
