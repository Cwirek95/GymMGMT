using GymMGMT.Api.Tests.Helpers;
using GymMGMT.Application.CQRS.MembershipTypes.Commands.ChangeMembershipTypeStatus;
using GymMGMT.Application.CQRS.MembershipTypes.Commands.CreateMembershipType;
using GymMGMT.Domain.Entities;
using GymMGMT.Persistence.EF;
using System.Net;

namespace GymMGMT.Api.Tests.Controllers
{
    public class MembershipTypesControllerTests : IClassFixture<ApiTestsServices>
    {
        private readonly ApiTestsServices _services;
        private readonly HttpClient _httpClient;

        public MembershipTypesControllerTests(ApiTestsServices services)
        {
            _services = services;
            _httpClient = _services.CreateClient();
        }

        [Fact]
        public async Task GetAll_WithQueryParameters_ReturnOkResponse()
        {
            // Act
            var response = await _httpClient.GetAsync("/api/admin/membershiptypes");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Detail_ForQueryParameters_ReturnOkResponse()
        {
            // Arrange
            var membershipType = new MembershipType()
            {
                Id = new Random().Next(),
                Name = "MType",
                DefaultPrice = 99.12,
                DurationInDays = 20,
                Status = true
            };
            SeedMembershipType(membershipType);

            // Act
            var response = await _httpClient.GetAsync("/api/admin/membershiptypes/" + membershipType.Id);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Create_ForValidModel_ReturnOkResponse()
        {
            // Arrange
            var model = new CreateMembershipTypeCommand()
            {
                Name = "MType1",
                DefaultPrice = 99.99,
                DurationInDays = 30
            };
            var httpContent = model.ToJsonHttpContent();

            // Act
            var response = await _httpClient.PostAsync("/api/admin/membershiptypes", httpContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Create_ForInvalidModel_ReturnUnprocessableEntityResponse()
        {
            // Arrange
            var model = new CreateMembershipTypeCommand()
            {
                Name = "",
                DefaultPrice = 99.99,
                DurationInDays = 30
            };
            var httpContent = model.ToJsonHttpContent();

            // Act
            var response = await _httpClient.PostAsync("/api/admin/membershiptypes", httpContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
        }

        [Fact]
        public async Task ChangeStatus_ForValidModel_ReturnNoContentResponse()
        {
            // Arrange
            var membershipType = new MembershipType()
            {
                Id = new Random().Next(),
                Name = "MType",
                DefaultPrice = 99.12,
                DurationInDays = 20,
                Status = true
            };
            SeedMembershipType(membershipType);

            var model = new ChangeMembershipTypeStatusCommand()
            {
                Id = membershipType.Id,
            };
            var httpContent = model.ToJsonHttpContent();

            // Act
            var response = await _httpClient.PutAsync("/api/admin/membershiptypes/status", httpContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task ChangeStatus_ForNonExistingMembershipType_ReturnNotFoundResponse()
        {
            // Arrange
            var model = new ChangeMembershipTypeStatusCommand()
            {
                Id = new Random().Next()
            };
            var httpContent = model.ToJsonHttpContent();

            // Act
            var response = await _httpClient.PutAsync("/api/admin/membershiptypes/status", httpContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Delete_ForValidModel_ReturnNoContentResponse()
        {
            // Arrange
            var membershipType = new MembershipType()
            {
                Id = new Random().Next(),
                Name = "MType",
                DefaultPrice = 99.12,
                DurationInDays = 20,
                Status = true
            };
            SeedMembershipType(membershipType);

            // Act
            var response = await _httpClient.DeleteAsync("/api/admin/membershiptypes/" + membershipType.Id);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task Delete_ForNonExistingMembershipType_ReturnNotFoundResponse()
        {
            // Act
            var response = await _httpClient.DeleteAsync("/api/admin/membershiptypes/" + new Random().Next());

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        private void SeedMembershipType(MembershipType membershipType)
        {
            var scopeFactory = _services.Services.GetService<IServiceScopeFactory>();
            using var scope = scopeFactory.CreateScope();
            var _dbContext = scope.ServiceProvider.GetService<AppDbContext>();

            _dbContext.MembershipTypes.Add(membershipType);
            _dbContext.SaveChanges();
        }
    }
}