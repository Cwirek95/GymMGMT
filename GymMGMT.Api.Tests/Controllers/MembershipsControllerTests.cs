using GymMGMT.Api.Tests.Helpers;
using GymMGMT.Application.CQRS.Memberships.Commands.AddMembership;
using GymMGMT.Application.CQRS.Memberships.Commands.ChangeMembershipPrice;
using GymMGMT.Application.CQRS.Memberships.Commands.ChangeMembershipStatus;
using GymMGMT.Application.CQRS.Memberships.Commands.ChangeMembershipType;
using GymMGMT.Application.CQRS.Memberships.Commands.ExtendMembership;
using GymMGMT.Application.CQRS.Memberships.Commands.SetDefaultPriceForCurrentMembers;
using GymMGMT.Domain.Entities;
using GymMGMT.Persistence.EF;
using System.Net;

namespace GymMGMT.Api.Tests.Controllers
{
    public class MembershipsControllerTests : IDisposable, IClassFixture<ApiTestsServices>
    {
        private readonly ApiTestsServices _services;
        private readonly HttpClient _httpClient;
        private readonly MembershipType _membershipType;
        private readonly User _user;

        public MembershipsControllerTests(ApiTestsServices services)
        {
            _services = services;
            _httpClient = _services.CreateClient();

            _membershipType = new MembershipType()
            {
                Id = new Random().Next(),
                Name = "MType1",
                DurationInDays = 30,
                DefaultPrice = 39.99,
                Status = true,
            };
            SeedMembershipType(_membershipType);

            _user = new User()
            {
                Id = Guid.NewGuid(),
                Email = "email@email.com",
                Password = BCrypt.Net.BCrypt.HashPassword("12345"),
                RegisteredAt = DateTimeOffset.Now,
                Status = true,
            };
            SeedUser(_user);
        }

        public void Dispose()
        {
            RemoveMembershipType(_membershipType);
            RemoveUser(_user);
        }

        [Fact]
        public async Task GetAll_WithQueryParameters_ReturnOkResponse()
        {
            // Act
            var response = await _httpClient.GetAsync("/api/admin/memberships");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Detail_ForQueryParameters_ReturnOkResponse()
        {
            // Arrange
            var membership = new Membership()
            {
                Id = new Random().Next(),
                StartDate = DateTimeOffset.Now,
                LastExtension = DateTimeOffset.Now,
                EndDate = DateTimeOffset.Now,
                Price = 102.49,
                MembershipTypeId = _membershipType.Id,
                Status = true
            };
            SeedMembership(membership);

            // Act
            var response = await _httpClient.GetAsync("/api/admin/memberships/" + membership.Id);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Create_ForValidModel_ReturnOkResponse()
        {
            // Arrange
            var member = new Member()
            {
                Id = new Random().Next(),
                FirstName = "FName",
                LastName = "LName",
                DateOfBirth = DateTimeOffset.Now.AddYears(-23),
                PhoneNumber = "+4812343548",
                UserId = _user.Id,
                Status = true
            };
            SeedMember(member);

            var model = new AddMembershipCommand()
            {
                Price = 119.19,
                MemberId = member.Id,
                MembershipTypeId = _membershipType.Id,
            };
            var httpContent = model.ToJsonHttpContent();

            // Act
            var response = await _httpClient.PostAsync("/api/admin/memberships", httpContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task ChangeStatus_ForValidModel_ReturnNoContentResponse()
        {
            // Arrange
            var membership = new Membership()
            {
                Id = new Random().Next(),
                StartDate = DateTimeOffset.Now,
                LastExtension = DateTimeOffset.Now,
                EndDate = DateTimeOffset.Now,
                Price = 102.49,
                MembershipTypeId = _membershipType.Id,
                Status = true
            };
            SeedMembership(membership);

            var model = new ChangeMembershipStatusCommand()
            {
                Id = membership.Id
            };
            var httpContent = model.ToJsonHttpContent();

            // Act
            var response = await _httpClient.PutAsync("/api/admin/memberships/status", httpContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task ChangeStatus_ForNonExistingMember_ReturnNotFoundResponse()
        {
            // Arrange
            var model = new ChangeMembershipStatusCommand()
            {
                Id = new Random().Next(),
            };
            var httpContent = model.ToJsonHttpContent();

            // Act
            var response = await _httpClient.PutAsync("/api/admin/memberships/status", httpContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task ChangePrice_ForValidModel_ReturnNoContentResponse()
        {
            // Arrange
            var membership = new Membership()
            {
                Id = new Random().Next(),
                StartDate = DateTimeOffset.Now,
                LastExtension = DateTimeOffset.Now,
                EndDate = DateTimeOffset.Now,
                Price = 102.49,
                MembershipTypeId = _membershipType.Id,
                Status = true
            };
            SeedMembership(membership);

            var model = new ChangeMembershipPriceCommand()
            {
                Id = membership.Id,
                Price = 99.99
            };
            var httpContent = model.ToJsonHttpContent();

            // Act
            var response = await _httpClient.PutAsync("/api/admin/memberships/price", httpContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task ChangePrice_ForNonExistingMember_ReturnNotFoundResponse()
        {
            // Arrange
            var model = new ChangeMembershipPriceCommand()
            {
                Id = new Random().Next(),
                Price = 99.99
            };
            var httpContent = model.ToJsonHttpContent();

            // Act
            var response = await _httpClient.PutAsync("/api/admin/memberships/price", httpContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task ChangeType_ForValidModel_ReturnNoContentResponse()
        {
            // Arrange
            var membershipType = new MembershipType()
            {
                Id = new Random().Next(),
                Name = "MType2",
                DurationInDays = 25,
                DefaultPrice = 89.99,
                Status = true,
            };
            SeedMembershipType(membershipType);

            var member = new Member()
            {
                Id = new Random().Next(),
                FirstName = "FName",
                LastName = "LName",
                DateOfBirth = DateTimeOffset.Now.AddYears(-23),
                PhoneNumber = "+4812343548",
                UserId = _user.Id,
                Status = true
            };
            SeedMember(member);

            var membership = new Membership()
            {
                Id = new Random().Next(),
                StartDate = DateTimeOffset.Now,
                LastExtension = DateTimeOffset.Now,
                EndDate = DateTimeOffset.Now,
                Price = 102.49,
                MembershipTypeId = membershipType.Id,
                MemberId = member.Id,
                Status = true
            };
            SeedMembership(membership);

            var model = new ChangeMembershipTypeCommand()
            {
                MemberId = member.Id,
                NewMembershipTypeId = _membershipType.Id
            };
            var httpContent = model.ToJsonHttpContent();

            // Act
            var response = await _httpClient.PutAsync("/api/admin/memberships/type", httpContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task ChangeType_ForNonExistingMember_ReturnNotFoundResponse()
        {
            // Arrange
            var model = new ChangeMembershipTypeCommand()
            {
                MemberId = new Random().Next(),
                NewMembershipTypeId = _membershipType.Id
            };
            var httpContent = model.ToJsonHttpContent();

            // Act
            var response = await _httpClient.PutAsync("/api/admin/memberships/type", httpContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task SetDefaultPrice_ForValidModel_ReturnNoContentResponse()
        {
            // Arrange
            var model = new SetDefaultPriceForCurrentMembersCommand()
            {
                MembershipTypeId = _membershipType.Id
            };
            var httpContent = model.ToJsonHttpContent();

            // Act
            var response = await _httpClient.PutAsync("/api/admin/memberships/default-price", httpContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task SetDefaultPrice_ForNonExistingMember_ReturnNotFoundResponse()
        {
            // Arrange
            var model = new SetDefaultPriceForCurrentMembersCommand()
            {
                MembershipTypeId = new Random().Next()
            };
            var httpContent = model.ToJsonHttpContent();

            // Act
            var response = await _httpClient.PutAsync("/api/admin/memberships/default-price", httpContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Extend_ForValidModel_ReturnNoContentResponse()
        {
            // Arrange
            var membership = new Membership()
            {
                Id = new Random().Next(),
                StartDate = DateTimeOffset.Now,
                LastExtension = DateTimeOffset.Now,
                EndDate = DateTimeOffset.Now,
                Price = 102.49,
                MembershipTypeId = _membershipType.Id,
                Status = true
            };
            SeedMembership(membership);

            var model = new ExtendMembershipCommand()
            {
                Id = membership.Id
            };
            var httpContent = model.ToJsonHttpContent();

            // Act
            var response = await _httpClient.PutAsync("/api/admin/memberships/extend", httpContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task Extend_ForNonExistingMember_ReturnNotFoundResponse()
        {
            // Arrange
            var model = new ExtendMembershipCommand()
            {
                Id = new Random().Next()
            };
            var httpContent = model.ToJsonHttpContent();

            // Act
            var response = await _httpClient.PutAsync("/api/admin/memberships/extend", httpContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Delete_ForValidModel_ReturnNoContentResponse()
        {
            // Arrange
            var membership = new Membership()
            {
                Id = new Random().Next(),
                StartDate = DateTimeOffset.Now,
                LastExtension = DateTimeOffset.Now,
                EndDate = DateTimeOffset.Now,
                Price = 99.19,
                MembershipTypeId = _membershipType.Id,
                Status = true
            };
            SeedMembership(membership);

            // Act
            var response = await _httpClient.DeleteAsync("/api/admin/memberships/" + membership.Id);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task Delete_ForNonExistingMember_ReturnNotFoundResponse()
        {
            // Act
            var response = await _httpClient.DeleteAsync("/api/admin/memberships/" + new Random().Next());

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        private void SeedMembership(Membership membership)
        {
            var scopeFactory = _services.Services.GetService<IServiceScopeFactory>();
            using var scope = scopeFactory.CreateScope();
            var _dbContext = scope.ServiceProvider.GetService<AppDbContext>();

            _dbContext.Memberships.Add(membership);
            _dbContext.SaveChanges();
        }

        private void SeedMembershipType(MembershipType membershipType)
        {
            var scopeFactory = _services.Services.GetService<IServiceScopeFactory>();
            using var scope = scopeFactory.CreateScope();
            var _dbContext = scope.ServiceProvider.GetService<AppDbContext>();

            _dbContext.MembershipTypes.Add(membershipType);
            _dbContext.SaveChanges();
        }

        private void RemoveMembershipType(MembershipType membershipType)
        {
            var scopeFactory = _services.Services.GetService<IServiceScopeFactory>();
            using var scope = scopeFactory.CreateScope();
            var _dbContext = scope.ServiceProvider.GetService<AppDbContext>();

            _dbContext.MembershipTypes.Remove(membershipType);
            _dbContext.SaveChanges();
        }

        private void SeedMember(Member member)
        {
            var scopeFactory = _services.Services.GetService<IServiceScopeFactory>();
            using var scope = scopeFactory.CreateScope();
            var _dbContext = scope.ServiceProvider.GetService<AppDbContext>();

            _dbContext.Members.Add(member);
            _dbContext.SaveChanges();
        }

        private void SeedUser(User user)
        {
            var scopeFactory = _services.Services.GetService<IServiceScopeFactory>();
            using var scope = scopeFactory.CreateScope();
            var _dbContext = scope.ServiceProvider.GetService<AppDbContext>();

            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }

        private void RemoveUser(User user)
        {
            var scopeFactory = _services.Services.GetService<IServiceScopeFactory>();
            using var scope = scopeFactory.CreateScope();
            var _dbContext = scope.ServiceProvider.GetService<AppDbContext>();

            _dbContext.Users.Remove(user);
            _dbContext.SaveChanges();
        }
    }
}
