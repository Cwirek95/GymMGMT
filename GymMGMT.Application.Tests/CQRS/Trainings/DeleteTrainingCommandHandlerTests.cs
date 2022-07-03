using GymMGMT.Api.Services;
using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.CQRS.Trainings.Commands.DeleteTraining;
using GymMGMT.Application.Security.Contracts;
using GymMGMT.Application.Tests.Mocks;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace GymMGMT.Application.Tests.CQRS.Trainings
{
    public class DeleteTrainingCommandHandlerTests
    {
        private Mock<ITrainingRepository> _trainingRepositoryMock;
        private ICurrentUserService _currentUserService;

        public DeleteTrainingCommandHandlerTests()
        {
            var claimsPrincipal = new ClaimsPrincipal();
            claimsPrincipal.AddIdentity(new ClaimsIdentity(
                new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Email, "admin@email.com"),
                    new Claim(ClaimTypes.Role, "Admin")
                }));

            var _httpContextAccessor = new Mock<IHttpContextAccessor>();
            _httpContextAccessor.Setup(x => x.HttpContext.User).Returns(claimsPrincipal);
            _currentUserService = new CurrentUserService(_httpContextAccessor.Object);
            _trainingRepositoryMock = TrainingRepositoryMock.GetTrainingRepository();
        }

        [Fact()]
        public async Task Handle_ForValidCommand_ReturnSuccessResponse()
        {
            // Arrange
            var items = await _trainingRepositoryMock.Object.GetAllAsync();
            var handler = new DeleteTrainingCommandHandler(_trainingRepositoryMock.Object, _currentUserService);
            var command = new DeleteTrainingCommand()
            {
                Id = items.ToList().ElementAt(5).Id
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            response.Success.Should().BeTrue();
        }

        [Fact()]
        public async Task Handle_TrainingDeleted_ReturnOneLessTrainings()
        {
            // Arrange
            var items = await _trainingRepositoryMock.Object.GetAllAsync();
            var handler = new DeleteTrainingCommandHandler(_trainingRepositoryMock.Object, _currentUserService);
            var countBefore = (await _trainingRepositoryMock.Object.GetAllAsync()).Count();
            var command = new DeleteTrainingCommand()
            {
                Id = items.ToList().ElementAt(3).Id
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);
            var countAfter = (await _trainingRepositoryMock.Object.GetAllAsync()).Count();

            // Assert
            countAfter.Should().Be(countBefore - 1);
        }
    }
}