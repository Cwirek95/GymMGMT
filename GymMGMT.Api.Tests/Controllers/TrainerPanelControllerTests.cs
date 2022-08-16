using GymMGMT.Api.Tests.Fakes;
using GymMGMT.Api.Tests.Helpers;
using GymMGMT.Application.CQRS.Trainings.Commands.AddMemberToTraining;
using GymMGMT.Application.CQRS.Trainings.Commands.AddTraining;
using GymMGMT.Application.CQRS.Trainings.Commands.ChangeTrainingDate;
using GymMGMT.Application.CQRS.Trainings.Commands.ChangeTrainingPrice;
using GymMGMT.Application.CQRS.Trainings.Commands.ChangeTrainingStatus;
using GymMGMT.Application.CQRS.Trainings.Commands.ChangeTrainingType;
using GymMGMT.Domain.Entities;
using System.Net;

namespace GymMGMT.Api.Tests.Controllers
{
    public class TrainerPanelControllerTests : IDisposable, IClassFixture<ApiTestsServices>
    {
        private readonly ApiTestsServices _services;
        private readonly HttpClient _httpClient;
        private readonly Trainer _trainer;
        private readonly User _user;

        public TrainerPanelControllerTests(ApiTestsServices services)
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
            FakeDataSeed.SeedUser(_user, _services);

            _trainer = new Trainer()
            {
                Id = new Random().Next(),
                FirstName = "Trainer1",
                LastName = "LastName1",
                UserId = _user.Id,
                Status = true
            };
            FakeDataSeed.SeedTrainer(_trainer, _services);
        }

        public void Dispose()
        {
            FakeDataSeed.RemoveUser(_user, _services);
            FakeDataSeed.RemoveTrainer(_trainer, _services);
        }

        [Fact]
        public async Task GetTrainingsList_WithQueryParameters_ReturnOkResponse()
        {
            // Act
            var response = await _httpClient.GetAsync("/api/trainer/trainings");

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
            FakeDataSeed.SeedTraining(training, _services);

            // Act
            var response = await _httpClient.GetAsync("/api/trainer/trainings/" + training.Id);

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
            var response = await _httpClient.PostAsync("/api/trainer/trainings", httpContent);

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
            var response = await _httpClient.PostAsync("/api/trainer/trainings", httpContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
        }

        [Fact]
        public async Task ChangeTrainingType_ForValidModel_ReturnNoContentResponse()
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
            FakeDataSeed.SeedTraining(training, _services);

            var model = new ChangeTrainingTypeCommand()
            {
                Id = training.Id,
            };
            var httpContent = model.ToJsonHttpContent();

            // Act
            var response = await _httpClient.PutAsync("/api/trainer/trainings/type", httpContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task ChangeTrainingType_ForNonExistingTraining_ReturnNotFoundResponse()
        {
            // Arrange
            var model = new ChangeTrainingStatusCommand()
            {
                Id = new Random().Next(),
            };
            var httpContent = model.ToJsonHttpContent();

            // Act
            var response = await _httpClient.PutAsync("/api/trainer/trainings/type", httpContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task ChangeTrainingPrice_ForValidModel_ReturnNoContentResponse()
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
            FakeDataSeed.SeedTraining(training, _services);

            var model = new ChangeTrainingPriceCommand()
            {
                Id = training.Id,
                Price = 92.99
            };
            var httpContent = model.ToJsonHttpContent();

            // Act
            var response = await _httpClient.PutAsync("/api/trainer/trainings/price", httpContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task ChangeTrainingPrice_ForNonExistingTraining_ReturnNotFoundResponse()
        {
            // Arrange
            var model = new ChangeTrainingStatusCommand()
            {
                Id = new Random().Next(),
            };
            var httpContent = model.ToJsonHttpContent();

            // Act
            var response = await _httpClient.PutAsync("/api/trainer/trainings/price", httpContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task ChangeTrainingDate_ForValidModel_ReturnNoContentResponse()
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
            FakeDataSeed.SeedTraining(training, _services);

            var model = new ChangeTrainingDateCommand()
            {
                Id = training.Id,
                StartDate = DateTimeOffset.Now.AddDays(12),
                EndDate = DateTimeOffset.Now.AddDays(13)
            };
            var httpContent = model.ToJsonHttpContent();

            // Act
            var response = await _httpClient.PutAsync("/api/trainer/trainings/date", httpContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task ChangeTrainingDate_ForNonExistingTraining_ReturnNotFoundResponse()
        {
            // Arrange
            var model = new ChangeTrainingDateCommand()
            {
                Id = new Random().Next(),
                StartDate = DateTimeOffset.Now.AddDays(12),
                EndDate = DateTimeOffset.Now.AddDays(13)
            };
            var httpContent = model.ToJsonHttpContent();

            // Act
            var response = await _httpClient.PutAsync("/api/trainer/trainings/date", httpContent);

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
            FakeDataSeed.SeedTraining(training, _services);

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
            FakeDataSeed.SeedMember(member, _services);

            var model = new AddMemberToTrainingCommand()
            {
                TrainingId = training.Id,
                MemberId = member.Id,
            };
            var httpContent = model.ToJsonHttpContent();

            // Act
            var response = await _httpClient.PutAsync("/api/trainer/trainings/member", httpContent);

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
            FakeDataSeed.SeedMember(member, _services);

            var model = new AddMemberToTrainingCommand()
            {
                TrainingId = new Random().Next(),
                MemberId = member.Id,
            };
            var httpContent = model.ToJsonHttpContent();

            // Act
            var response = await _httpClient.PutAsync("/api/trainer/trainings/member", httpContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task DeleteTraining_ForValidModel_ReturnNoContentResponse()
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
            FakeDataSeed.SeedTraining(training, _services);

            // Act
            var response = await _httpClient.DeleteAsync("/api/trainer/trainings/" + training.Id);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task DeleteTraining_ForNonExistingTraining_ReturnNotFoundResponse()
        {
            // Act
            var response = await _httpClient.DeleteAsync("/api/trainer/trainings/" + new Random().Next());

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
