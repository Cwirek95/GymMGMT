using GymMGMT.Api.Services;
using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.CQRS.Trainings.Commands.AddMemberToTraining;
using GymMGMT.Application.Security.Contracts;
using GymMGMT.Application.Tests.Mocks;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace GymMGMT.Application.Tests.CQRS.Trainings
{
    public class AddMemberToTrainingCommandHandlerTests
    {
        private Mock<ITrainingRepository> _trainingRepositoryMock;
        private Mock<IMemberRepository> _memberRepositoryMock;
        private ICurrentUserService _currentUserService;

        public AddMemberToTrainingCommandHandlerTests()
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
            _memberRepositoryMock = MemberRepositoryMock.GetMemberRepository();
        }

        [Fact()]
        public async Task Handle_ForValidCommand_ReturnSuccessResponse()
        {
            // Arrange
            var trainings = await _trainingRepositoryMock.Object.GetAllAsync();
            var members = await _memberRepositoryMock.Object.GetAllAsync();
            var handler = new AddMemberToTrainingCommandHandler(_memberRepositoryMock.Object, 
                _trainingRepositoryMock.Object, _currentUserService);
            var command = new AddMemberToTrainingCommand()
            {
                TrainingId = trainings.First().Id,
                MemberId = members.Last().Id
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            response.Success.Should().BeTrue();
        }
    }
}