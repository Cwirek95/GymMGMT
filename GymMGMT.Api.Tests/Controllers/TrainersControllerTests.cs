using GymMGMT.Api.Tests.Helpers;
using GymMGMT.Application.CQRS.Trainers.Commands.AddTrainer;
using GymMGMT.Application.CQRS.Trainers.Commands.ChangeTrainerStatus;
using GymMGMT.Application.CQRS.Trainers.Commands.UpdateTrainer;
using GymMGMT.Application.CQRS.Trainings.Commands.DeleteMemberFromTraining;
using GymMGMT.Domain.Entities;
using GymMGMT.Persistence.EF;
using System.Net;

namespace GymMGMT.Api.Tests.Controllers
{
    public class TrainersControllerTests : IDisposable, IClassFixture<ApiTestsServices>
    {
        private readonly ApiTestsServices _services;
        private readonly HttpClient _httpClient;
        private readonly User _user;

        public TrainersControllerTests(ApiTestsServices services)
        {
            _services = services;
            _httpClient = _services.CreateClient();

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
            RemoveUser(_user);
        }

        [Fact]
        public async Task GetAll_WithQueryParameters_ReturnOkResponse()
        {
            // Act
            var response = await _httpClient.GetAsync("/api/admin/trainers");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Detail_ForQueryParameters_ReturnOkResponse()
        {
            // Arrange
            var trainer = new Trainer()
            {
                Id = new Random().Next(),
                FirstName = "FName",
                LastName = "LName",
                UserId = _user.Id,
                Status = true
            };
            SeedTrainer(trainer);

            // Act
            var response = await _httpClient.GetAsync("/api/admin/trainers/" + trainer.Id);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Create_ForValidModel_ReturnOkResponse()
        {
            // Arrange
            var model = new AddTrainerCommand()
            {
                FirstName = "FirstName1",
                LastName = "LastName1",
                UserId = _user.Id
            };
            var httpContent = model.ToJsonHttpContent();

            // Act
            var response = await _httpClient.PostAsync("/api/admin/trainers", httpContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Create_ForInvalidModel_ReturnUnprocessableEntityResponse()
        {
            // Arrange
            var model = new AddTrainerCommand()
            {
                FirstName = "FirstName1",
                LastName = "",
                UserId = _user.Id
            };
            var httpContent = model.ToJsonHttpContent();

            // Act
            var response = await _httpClient.PostAsync("/api/admin/trainers", httpContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
        }

        [Fact]
        public async Task Update_ForValidModel_ReturnNoContentResponse()
        {
            // Arrange
            var trainer = new Trainer()
            {
                Id = new Random().Next(),
                FirstName = "FName",
                LastName = "LName",
                UserId = _user.Id,
                Status = true
            };
            SeedTrainer(trainer);

            var model = new UpdateTrainerCommand()
            {
                Id = trainer.Id,
                FirstName = "FNameUp",
                LastName = "LNameUp"
            };
            var httpContent = model.ToJsonHttpContent();

            // Act
            var response = await _httpClient.PutAsync("/api/admin/trainers", httpContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task Update_ForNonExistingTrainer_ReturnNotFoundResponse()
        {
            // Arrange
            var model = new UpdateTrainerCommand()
            {
                Id = new Random().Next(),
                FirstName = "FNameUp",
                LastName = "LNameUp"
            };
            var httpContent = model.ToJsonHttpContent();

            // Act
            var response = await _httpClient.PutAsync("/api/admin/trainers", httpContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task ChangeStatus_ForValidModel_ReturnNoContentResponse()
        {
            // Arrange
            var trainer = new Trainer()
            {
                Id = new Random().Next(),
                FirstName = "FName",
                LastName = "LName",
                UserId = _user.Id,
                Status = true
            };
            SeedTrainer(trainer);

            var model = new ChangeTrainerStatusCommand()
            {
                Id = trainer.Id,
            };
            var httpContent = model.ToJsonHttpContent();

            // Act
            var response = await _httpClient.PutAsync("/api/admin/trainers/status", httpContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task ChangeStatus_ForNonExistingTrainer_ReturnNotFoundResponse()
        {
            // Arrange
            var model = new ChangeTrainerStatusCommand()
            {
                Id = new Random().Next(),
            };
            var httpContent = model.ToJsonHttpContent();

            // Act
            var response = await _httpClient.PutAsync("/api/admin/trainers/status", httpContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task DeleteMember_ForValidModel_ReturnNoContentResponse()
        {
            // Arrange
            var trainer = new Trainer()
            {
                Id = new Random().Next(),
                FirstName = "FName",
                LastName = "LName",
                UserId = _user.Id,
                Status = true
            };
            SeedTrainer(trainer);

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

            var training = new Training()
            {
                Id = new Random().Next(),
                StartDate = DateTimeOffset.Now.AddDays(8),
                EndDate = DateTimeOffset.Now.AddDays(9),
                Price = 12.99,
                TrainerId = trainer.Id,
                TrainingType = Domain.Enums.TrainingType.GROUP,
                Status = true
            };
            SeedTraining(training);

            training.Members.Add(member);

            var model = new DeleteMemberFromTrainingCommand()
            {
                TrainingId = training.Id,
                MemberId = member.Id
            };
            var httpContent = model.ToJsonHttpContent();

            // Act
            var response = await _httpClient.PutAsync("/api/admin/trainings/member/delete", httpContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task Delete_ForValidModel_ReturnNoContentResponse()
        {
            // Arrange
            var trainer = new Trainer()
            {
                Id = new Random().Next(),
                FirstName = "FName",
                LastName = "LName",
                UserId = _user.Id,
                Status = true
            };
            SeedTrainer(trainer);

            // Act
            var response = await _httpClient.DeleteAsync("/api/admin/trainers/" + trainer.Id);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task Delete_ForNonExistingTrainer_ReturnNotFoundResponse()
        {
            // Act
            var response = await _httpClient.DeleteAsync("/api/admin/trainers/" + new Random().Next());

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        private void SeedTrainer(Trainer trainer)
        {
            var scopeFactory = _services.Services.GetService<IServiceScopeFactory>();
            using var scope = scopeFactory.CreateScope();
            var _dbContext = scope.ServiceProvider.GetService<AppDbContext>();

            _dbContext.Trainers.Add(trainer);
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
    }
}