using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.CQRS.Trainers.Commands.UpdateTrainer;
using GymMGMT.Application.Tests.Mocks;

namespace GymMGMT.Application.Tests.CQRS.Trainers
{
    public class UpdateTrainerCommandHandlerTests
    {
        private Mock<ITrainerRepository> _trainerRepositoryMock;

        public UpdateTrainerCommandHandlerTests()
        {
            _trainerRepositoryMock = TrainerRepositoryMock.GetTrainerRepository();
        }

        [Fact()]
        public async Task Handle_ForValidCommand_ReturnSuccessResponse()
        {
            // Arrange
            var items = await _trainerRepositoryMock.Object.GetAllAsync();
            var handler = new UpdateTrainerCommandHandler(_trainerRepositoryMock.Object);
            var command = new UpdateTrainerCommand()
            {
                Id = items.First().Id,
                FirstName = "FnameU",
                LastName = "LNameU"
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            response.Success.Should().BeTrue();
        }

        [Fact()]
        public async Task Handle_ChangeFirstName_ReturnMemberWithNewFirstName()
        {
            // Arrange
            var items = await _trainerRepositoryMock.Object.GetAllAsync();
            var handler = new UpdateTrainerCommandHandler(_trainerRepositoryMock.Object);
            var fNameBefore = (await _trainerRepositoryMock.Object.GetByIdAsync(items.Last().Id)).FirstName;
            var command = new UpdateTrainerCommand()
            {
                Id = items.Last().Id,
                FirstName = "FnameU",
                LastName = "LName",
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);
            var fNameAfter = (await _trainerRepositoryMock.Object.GetByIdAsync(items.Last().Id)).FirstName;

            // Assert
            fNameAfter.Should().NotBe(fNameBefore);
        }
    }
}