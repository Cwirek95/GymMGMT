﻿using GymMGMT.Api.Tests.Fakes;
using GymMGMT.Api.Tests.Helpers;
using GymMGMT.Application.CQRS.Members.Commands.AddMember;
using GymMGMT.Application.CQRS.Members.Commands.ChangeMemberStatus;
using GymMGMT.Application.CQRS.Members.Commands.SetMembershipToMember;
using GymMGMT.Application.CQRS.Members.Commands.UpdateMember;
using GymMGMT.Domain.Entities;
using System.Net;

namespace GymMGMT.Api.Tests.Controllers
{
    public class MembersControllerTests : IDisposable, IClassFixture<ApiTestsServices>
    {
        private readonly ApiTestsServices _services;
        private readonly HttpClient _httpClient;
        private readonly MembershipType _membershipType;
        private readonly User _user;

        public MembersControllerTests(ApiTestsServices services)
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
            FakeDataSeed.SeedMembershipType(_membershipType, _services);

            _user = new User()
            {
                Id = Guid.NewGuid(),
                Email = "email@email.com",
                Password = BCrypt.Net.BCrypt.HashPassword("12345"),
                RegisteredAt = DateTimeOffset.Now,
                Status = true,
            };
            FakeDataSeed.SeedUser(_user, _services);
        }

        public void Dispose()
        {
            FakeDataSeed.RemoveMembershipType(_membershipType, _services);
            FakeDataSeed.RemoveUser(_user, _services);
        }

        [Fact]
        public async Task GetAll_WithQueryParameters_ReturnOkResponse()
        {
            // Act
            var response = await _httpClient.GetAsync("/api/admin/members");

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
                Price = 99.19,
                MembershipTypeId = _membershipType.Id,
                Status = true
            };
            FakeDataSeed.SeedMembership(membership, _services);

            var member = new Member()
            {
                Id = new Random().Next(),
                FirstName = "FName",
                LastName = "LName",
                DateOfBirth = DateTimeOffset.Now.AddYears(-23),
                PhoneNumber = "+4812343548",
                UserId = _user.Id,
                MembershipId = membership.Id,
                Status = true
            };
            FakeDataSeed.SeedMember(member, _services);

            // Act
            var response = await _httpClient.GetAsync("/api/admin/members/" + member.Id);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Create_ForValidModel_ReturnOkResponse()
        {
            // Arrange
            var model = new AddMemberCommand()
            {
                FirstName = "FirstName1",
                LastName = "LastName1",
                DateOfBirth = DateTimeOffset.Now.AddYears(-25),
                PhoneNumber = "+48123456789",
                Price = 99,
                MembershipTypeId = _membershipType.Id,
                UserId = _user.Id
            };
            var httpContent = model.ToJsonHttpContent();

            // Act
            var response = await _httpClient.PostAsync("/api/admin/members", httpContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Create_ForInvalidModel_ReturnUnprocessableEntityResponse()
        {
            // Arrange
            var model = new AddMemberCommand()
            {
                FirstName = "FName1",
                LastName = "",
                DateOfBirth = DateTimeOffset.Now.AddYears(-23),
                PhoneNumber = "+48123456789",
                Price = 99.99,
                MembershipTypeId = _membershipType.Id,
                UserId = _user.Id
            };
            var httpContent = model.ToJsonHttpContent();

            // Act
            var response = await _httpClient.PostAsync("/api/admin/members", httpContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
        }

        [Fact]
        public async Task ChangeStatus_ForValidModel_ReturnNoContentResponse()
        {
            // Arrange
            var member = new Member()
            {
                Id = new Random().Next(),
                FirstName = "FName",
                LastName = "LName",
                DateOfBirth = DateTimeOffset.Now.AddYears(-23),
                PhoneNumber = "+4812343548",
                UserId = Guid.NewGuid(),
                MembershipId = new Random().Next(),
                Status = true
            };
            FakeDataSeed.SeedMember(member, _services);

            var model = new ChangeMemberStatusCommand()
            {
                Id = member.Id,
            };
            var httpContent = model.ToJsonHttpContent();

            // Act
            var response = await _httpClient.PutAsync("/api/admin/members/status", httpContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task ChangeStatus_ForNonExistingMember_ReturnNotFoundResponse()
        {
            // Arrange
            var model = new ChangeMemberStatusCommand()
            {
                Id = new Random().Next()
            };
            var httpContent = model.ToJsonHttpContent();

            // Act
            var response = await _httpClient.PutAsync("/api/admin/members/status", httpContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Update_ForValidModel_ReturnNoContentResponse()
        {
            // Arrange
            var member = new Member()
            {
                Id = new Random().Next(),
                FirstName = "FName",
                LastName = "LName",
                DateOfBirth = DateTimeOffset.Now.AddYears(-23),
                PhoneNumber = "+4812343548",
                UserId = Guid.NewGuid(),
                MembershipId = new Random().Next(),
                Status = true
            };
            FakeDataSeed.SeedMember(member, _services);

            var model = new UpdateMemberCommand()
            {
                Id = member.Id,
                FirstName = "FNameU",
                LastName = "LNameU",
                DateOfBirth = DateTimeOffset.Now.AddYears(-19),
                PhoneNumber = "+48987654321",
            };
            var httpContent = model.ToJsonHttpContent();

            // Act
            var response = await _httpClient.PutAsync("/api/admin/members", httpContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task Update_ForNonExistingMember_ReturnNotFoundResponse()
        {
            // Arrange
            var model = new UpdateMemberCommand()
            {
                Id = new Random().Next(),
                FirstName = "FNameU",
                LastName = "LNameU",
                DateOfBirth = DateTimeOffset.Now.AddYears(-19),
                PhoneNumber = "+48987654321",
            };
            var httpContent = model.ToJsonHttpContent();

            // Act
            var response = await _httpClient.PutAsync("/api/admin/members", httpContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task SetMembership_ForValidModel_ReturnNoContentResponse()
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
            FakeDataSeed.SeedMembership(membership, _services);

            var member = new Member()
            {
                Id = new Random().Next(),
                FirstName = "FName",
                LastName = "LName",
                DateOfBirth = DateTimeOffset.Now.AddYears(-23),
                PhoneNumber = "+4812343548",
                UserId = Guid.NewGuid(),
                Status = true
            };
            FakeDataSeed.SeedMember(member, _services);

            var model = new SetMembershipToMemberCommand()
            {
                MemberId = member.Id,
                MembershipId = membership.Id
            };
            var httpContent = model.ToJsonHttpContent();

            // Act
            var response = await _httpClient.PutAsync("/api/admin/members/membership", httpContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task SetMembership_ForNonExistingMembership_ReturnNotFoundResponse()
        {
            // Arrange
            var member = new Member()
            {
                Id = new Random().Next(),
                FirstName = "FName",
                LastName = "LName",
                DateOfBirth = DateTimeOffset.Now.AddYears(-23),
                PhoneNumber = "+4812343548",
                UserId = Guid.NewGuid(),
                Status = true
            };
            FakeDataSeed.SeedMember(member, _services);

            var model = new SetMembershipToMemberCommand()
            {
                MemberId = member.Id,
                MembershipId = new Random().Next()
            };
            var httpContent = model.ToJsonHttpContent();

            // Act
            var response = await _httpClient.PutAsync("/api/admin/members/membership", httpContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task SetMembership_ForNonExistingMember_ReturnNotFoundResponse()
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
            FakeDataSeed.SeedMembership(membership, _services);

            var model = new SetMembershipToMemberCommand()
            {
                MemberId = new Random().Next(),
                MembershipId = membership.Id
            };
            var httpContent = model.ToJsonHttpContent();

            // Act
            var response = await _httpClient.PutAsync("/api/admin/members/membership", httpContent);

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
            FakeDataSeed.SeedMembership(membership, _services);

            var member = new Member()
            {
                Id = new Random().Next(),
                FirstName = "FName",
                LastName = "LName",
                DateOfBirth = DateTimeOffset.Now.AddYears(-23),
                PhoneNumber = "+4812343548",
                UserId = Guid.NewGuid(),
                MembershipId = membership.Id,
                Status = true
            };
            FakeDataSeed.SeedMember(member, _services);

            // Act
            var response = await _httpClient.DeleteAsync("/api/admin/members/" + member.Id);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task Delete_ForNonExistingMember_ReturnNotFoundResponse()
        {
            // Act
            var response = await _httpClient.DeleteAsync("/api/admin/members/" + new Random().Next());

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}