using GymMGMT.Api.Services;
using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.CQRS.Trainings.Commands.ChangeTrainingType;
using GymMGMT.Application.Security.Contracts;
using GymMGMT.Application.Tests.Mocks;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace GymMGMT.Application.Tests.CQRS.Trainings
{
    public class ChangeTrainingTypeCommandHandlerTests
    {
        private Mock<ITrainingRepository> _trainingRepositoryMock;
        private ICurrentUserService _currentUserService;

        public ChangeTrainingTypeCommandHandlerTests()
        {
            var claimsPrincipal = new ClaimsPrincipal();
            claimsPrincipal.AddIdentity(new ClaimsIdentity(
                new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Email, "admin@email.com"),
                    new Claim(ClaimTypes.Role, "Admin")
                }));

            _trainingRepositoryMock = TrainingRepositoryMock.GetTrainingRepository();
            var _httpContextAccessor = new Mock<IHttpContextAccessor>();
            _httpContextAccessor.Setup(x => x.HttpContext.User).Returns(claimsPrincipal);
            _currentUserService = new CurrentUserService(_httpContextAccessor.Object);
        }

        [Fact()]
        public async Task Handle_ForValidCommand_ReturnSuccessResponse()
        {
            // Arrange
            var items = await _trainingRepositoryMock.Object.GetAllAsync();
            var handler = new ChangeTrainingTypeCommandHandler(_trainingRepositoryMock.Object, _currentUserService);
            var command = new ChangeTrainingTypeCommand()
            {
                Id = items.First().Id,
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            response.Success.Should().BeTrue();
        }

        [Fact()]
        public async Task Handle_TrainingTypeChanged_ReturnNewTrainingType()
        {
            // Arrange
            var items = await _trainingRepositoryMock.Object.GetAllAsync();
            var handler = new ChangeTrainingTypeCommandHandler(_trainingRepositoryMock.Object, _currentUserService);
            var typeBefore = (await _trainingRepositoryMock.Object.GetByIdAsync(items.Last().Id)).TrainingType;
            var command = new ChangeTrainingTypeCommand()
            {
                Id = items.Last().Id,
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);
            var typeAfter = (await _trainingRepositoryMock.Object.GetByIdAsync(items.Last().Id)).TrainingType;

            // Assert
            typeAfter.Should().NotBe(typeBefore);
        }
    }
}