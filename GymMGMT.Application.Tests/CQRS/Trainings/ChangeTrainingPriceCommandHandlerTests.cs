using GymMGMT.Api.Services;
using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.CQRS.Trainings.Commands.ChangeTrainingPrice;
using GymMGMT.Application.Security.Contracts;
using GymMGMT.Application.Tests.Mocks;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace GymMGMT.Application.Tests.CQRS.Trainings
{
    public class ChangeTrainingPriceCommandHandlerTests
    {
        private Mock<ITrainingRepository> _trainingRepositoryMock;
        private ICurrentUserService _currentUserService;

        public ChangeTrainingPriceCommandHandlerTests()
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
            var handler = new ChangeTrainingPriceCommandHandler(_trainingRepositoryMock.Object, _currentUserService);
            var command = new ChangeTrainingPriceCommand()
            {
                Id = items.First().Id,
                Price = 39.99
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            response.Success.Should().BeTrue();
        }

        [Fact()]
        public async Task Handle_PriceChanged_ReturnNewPrice()
        {
            // Arrange
            var items = await _trainingRepositoryMock.Object.GetAllAsync();
            var handler = new ChangeTrainingPriceCommandHandler(_trainingRepositoryMock.Object, _currentUserService);
            var priceBefore = (await _trainingRepositoryMock.Object.GetByIdAsync(items.Last().Id)).Price;
            var command = new ChangeTrainingPriceCommand()
            {
                Id = items.Last().Id,
                Price = 99.99
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);
            var priceAfter = (await _trainingRepositoryMock.Object.GetByIdAsync(items.Last().Id)).Price;

            // Assert
            priceAfter.Should().NotBe(priceBefore);
        }
    }
}