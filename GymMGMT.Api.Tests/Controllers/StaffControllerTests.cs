using GymMGMT.Api.Tests.Helpers;
using GymMGMT.Application.CQRS.Members.Commands.AddMember;
using GymMGMT.Application.CQRS.Members.Commands.SetMembershipToMember;
using GymMGMT.Application.CQRS.Memberships.Commands.AddMembership;
using GymMGMT.Application.CQRS.Memberships.Commands.ChangeMembershipType;
using GymMGMT.Application.CQRS.Memberships.Commands.ExtendMembership;
using GymMGMT.Application.CQRS.Trainings.Commands.AddMemberToTraining;
using GymMGMT.Application.CQRS.Trainings.Commands.AddTraining;
using GymMGMT.Application.CQRS.Trainings.Commands.ChangeTrainer;
using GymMGMT.Domain.Entities;
using GymMGMT.Persistence.EF;
using System.Net;

namespace GymMGMT.Api.Tests.Controllers
{
    public class StaffControllerTests : IDisposable, IClassFixture<ApiTestsServices>
    {
        private readonly ApiTestsServices _services;
        private readonly HttpClient _httpClient;
        private readonly MembershipType _membershipType;
        private readonly Trainer _trainer;
        private readonly User _user;

        public StaffControllerTests(ApiTestsServices services)
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

            _trainer = new Trainer()
            {
                Id = new Random().Next(),
                FirstName = "Trainer1",
                LastName = "LastName1",
                UserId = _user.Id,
                Status = true
            };
            SeedTrainer(_trainer);
        }

        public void Dispose()
        {
            RemoveMembershipType(_membershipType);
            RemoveTrainer(_trainer);
            RemoveUser(_user);
        }

        [Fact]
        public async Task GetMembersList_WithQueryParameters_ReturnOkResponse()
        {
            // Act
            var response = await _httpClient.GetAsync("/api/staff/members");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task MemberDetail_ForQueryParameters_ReturnOkResponse()
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
            SeedMember(member);

            // Act
            var response = await _httpClient.GetAsync("/api/staff/members/" + member.Id);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task AddMember_ForValidModel_ReturnOkResponse()
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
            var response = await _httpClient.PostAsync("/api/staff/members", httpContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task AddMember_ForInvalidModel_ReturnUnprocessableEntityResponse()
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
            var response = await _httpClient.PostAsync("/api/staff/members", httpContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
        }

        [Fact]
        public async Task GetMembershipsList_WithQueryParameters_ReturnOkResponse()
        {
            // Act
            var response = await _httpClient.GetAsync("/api/staff/memberships");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task MembershipDetail_ForQueryParameters_ReturnOkResponse()
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
            var response = await _httpClient.GetAsync("/api/staff/memberships/" + membership.Id);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task AddMembership_ForValidModel_ReturnOkResponse()
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
            var response = await _httpClient.PostAsync("/api/staff/memberships", httpContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
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
            SeedMembership(membership);

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
            SeedMember(member);

            var model = new SetMembershipToMemberCommand()
            {
                MemberId = member.Id,
                MembershipId = membership.Id
            };
            var httpContent = model.ToJsonHttpContent();

            // Act
            var response = await _httpClient.PutAsync("/api/staff/memberships/set", httpContent);

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
            SeedMember(member);

            var model = new SetMembershipToMemberCommand()
            {
                MemberId = member.Id,
                MembershipId = new Random().Next()
            };
            var httpContent = model.ToJsonHttpContent();

            // Act
            var response = await _httpClient.PutAsync("/api/staff/memberships/set", httpContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task ChangeMembershipType_ForValidModel_ReturnNoContentResponse()
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
            var response = await _httpClient.PutAsync("/api/staff/memberships/type", httpContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task ChangeMembershipType_ForNonExistingMember_ReturnNotFoundResponse()
        {
            // Arrange
            var model = new ChangeMembershipTypeCommand()
            {
                MemberId = new Random().Next(),
                NewMembershipTypeId = _membershipType.Id
            };
            var httpContent = model.ToJsonHttpContent();

            // Act
            var response = await _httpClient.PutAsync("/api/staff/memberships/type", httpContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task ExtendMembersip_ForValidModel_ReturnNoContentResponse()
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
            var response = await _httpClient.PutAsync("/api/staff/memberships/extend", httpContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task ExtendMembership_ForNonExistingMember_ReturnNotFoundResponse()
        {
            // Arrange
            var model = new ExtendMembershipCommand()
            {
                Id = new Random().Next()
            };
            var httpContent = model.ToJsonHttpContent();

            // Act
            var response = await _httpClient.PutAsync("/api/staff/memberships/extend", httpContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetTrainingsList_WithQueryParameters_ReturnOkResponse()
        {
            // Act
            var response = await _httpClient.GetAsync("/api/staff/trainings");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task TrainingDetail_ForQueryParameters_ReturnOkResponse()
        {
            // Arrange
            var training = new Training()
            {
                Id = new Random().Next(),
                StartDate = DateTimeOffset.Now.AddDays(8),
                EndDate = DateTimeOffset.Now.AddDays(9),
                Price = 12.99,
                TrainerId = _trainer.Id,
                TrainingType = Domain.Enums.TrainingType.GROUP,
                Status = true
            };
            SeedTraining(training);

            // Act
            var response = await _httpClient.GetAsync("/api/staff/trainings/" + training.Id);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task AddTraining_ForValidModel_ReturnOkResponse()
        {
            // Arrange
            var model = new AddTrainingCommand()
            {
                StartDate = DateTimeOffset.Now.AddDays(6),
                EndDate = DateTimeOffset.Now.AddDays(7),
                Price = 32.99,
                TrainerId = _trainer.Id,
                TrainingType = Domain.Enums.TrainingType.INDIVIDUAL,
            };
            var httpContent = model.ToJsonHttpContent();

            // Act
            var response = await _httpClient.PostAsync("/api/staff/trainings", httpContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task AddTraining_ForInvalidModel_ReturnUnprocessableEntityResponse()
        {
            // Arrange
            var model = new AddTrainingCommand()
            {
                StartDate = DateTimeOffset.Now.AddDays(6),
                EndDate = DateTimeOffset.Now.AddDays(3),
                Price = 32.99,
                TrainerId = _trainer.Id,
                TrainingType = Domain.Enums.TrainingType.INDIVIDUAL,
            };
            var httpContent = model.ToJsonHttpContent();

            // Act
            var response = await _httpClient.PostAsync("/api/staff/trainings", httpContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
        }

        [Fact]
        public async Task ChangeTrainingTrainer_ForValidModel_ReturnNoContentResponse()
        {
            // Arrange
            var training = new Training()
            {
                Id = new Random().Next(),
                StartDate = DateTimeOffset.Now.AddDays(8),
                EndDate = DateTimeOffset.Now.AddDays(9),
                Price = 12.99,
                TrainerId = _trainer.Id,
                TrainingType = Domain.Enums.TrainingType.GROUP,
                Status = true
            };
            SeedTraining(training);

            var newTrainer = new Trainer()
            {
                Id = new Random().Next(),
                FirstName = "Trainer1",
                LastName = "LastName1",
                UserId = _user.Id,
                Status = true
            };
            SeedTrainer(newTrainer);

            var model = new ChangeTrainerCommand()
            {
                Id = training.Id,
                NewTrainerId = newTrainer.Id,
            };
            var httpContent = model.ToJsonHttpContent();

            // Act
            var response = await _httpClient.PutAsync("/api/staff/trainings/trainer", httpContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task ChangeTrainingTrainer_ForNonExistingTraining_ReturnNotFoundResponse()
        {
            // Arrange
            var newTrainer = new Trainer()
            {
                Id = new Random().Next(),
                FirstName = "Trainer1",
                LastName = "LastName1",
                UserId = _user.Id,
                Status = true
            };
            SeedTrainer(newTrainer);

            var model = new ChangeTrainerCommand()
            {
                Id = new Random().Next(),
                NewTrainerId = newTrainer.Id,
            };
            var httpContent = model.ToJsonHttpContent();

            // Act
            var response = await _httpClient.PutAsync("/api/staff/trainings/trainer", httpContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task AddMemberToTraining_ForValidModel_ReturnNoContentResponse()
        {
            // Arrange
            var training = new Training()
            {
                Id = new Random().Next(),
                StartDate = DateTimeOffset.Now.AddDays(8),
                EndDate = DateTimeOffset.Now.AddDays(9),
                Price = 12.99,
                TrainerId = _trainer.Id,
                TrainingType = Domain.Enums.TrainingType.GROUP,
                Status = true
            };
            SeedTraining(training);

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

            var model = new AddMemberToTrainingCommand()
            {
                TrainingId = training.Id,
                MemberId = member.Id,
            };
            var httpContent = model.ToJsonHttpContent();

            // Act
            var response = await _httpClient.PutAsync("/api/staff/trainings/member", httpContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task AddMemberToTraining_ForNonExistingTraining_ReturnNotFoundResponse()
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

            var model = new AddMemberToTrainingCommand()
            {
                TrainingId = new Random().Next(),
                MemberId = member.Id,
            };
            var httpContent = model.ToJsonHttpContent();

            // Act
            var response = await _httpClient.PutAsync("/api/staff/trainings/member", httpContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        private void SeedMember(Member member)
        {
            var scopeFactory = _services.Services.GetService<IServiceScopeFactory>();
            using var scope = scopeFactory.CreateScope();
            var _dbContext = scope.ServiceProvider.GetService<AppDbContext>();

            _dbContext.Members.Add(member);
            _dbContext.SaveChanges();
        }

        private void SeedTraining(Training training)
        {
            var scopeFactory = _services.Services.GetService<IServiceScopeFactory>();
            using var scope = scopeFactory.CreateScope();
            var _dbContext = scope.ServiceProvider.GetService<AppDbContext>();

            _dbContext.Trainings.Add(training);
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

        private void SeedMembership(Membership membership)
        {
            var scopeFactory = _services.Services.GetService<IServiceScopeFactory>();
            using var scope = scopeFactory.CreateScope();
            var _dbContext = scope.ServiceProvider.GetService<AppDbContext>();

            _dbContext.Memberships.Add(membership);
            _dbContext.SaveChanges();
        }

        private void SeedTrainer(Trainer trainer)
        {
            var scopeFactory = _services.Services.GetService<IServiceScopeFactory>();
            using var scope = scopeFactory.CreateScope();
            var _dbContext = scope.ServiceProvider.GetService<AppDbContext>();

            _dbContext.Trainers.Add(trainer);
            _dbContext.SaveChanges();
        }

        private void RemoveTrainer(Trainer trainer)
        {
            var scopeFactory = _services.Services.GetService<IServiceScopeFactory>();
            using var scope = scopeFactory.CreateScope();
            var _dbContext = scope.ServiceProvider.GetService<AppDbContext>();

            _dbContext.Trainers.Remove(trainer);
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
