using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.CQRS.Trainings.Commands.AddMemberToTraining;
using GymMGMT.Application.Tests.Mocks;

namespace GymMGMT.Application.Tests.CQRS.Trainings
{
    public class AddMemberToTrainingCommandHandlerTests
    {
        private Mock<ITrainingRepository> _trainingRepositoryMock;
        private Mock<IMemberRepository> _memberRepositoryMock;

        public AddMemberToTrainingCommandHandlerTests()
        {
            _trainingRepositoryMock = TrainingRepositoryMock.GetTrainingRepository();
            _memberRepositoryMock = MemberRepositoryMock.GetMemberRepository();
        }

        [Fact()]
        public async Task Handle_ForValidCommand_ReturnSuccessResponse()
        {
            // Arrange
            var trainings = await _trainingRepositoryMock.Object.GetAllAsync();
            var members = await _memberRepositoryMock.Object.GetAllAsync();
            var handler = new AddMemberToTrainingCommandHandler(_memberRepositoryMock.Object, _trainingRepositoryMock.Object);
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