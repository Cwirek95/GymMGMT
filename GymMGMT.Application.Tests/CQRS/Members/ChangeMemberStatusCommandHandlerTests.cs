using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.CQRS.Members.Commands.ChangeMemberStatus;
using GymMGMT.Application.Tests.Mocks;

namespace GymMGMT.Application.Tests.CQRS.Members
{
    public class ChangeMemberStatusCommandHandlerTests
    {
        private Mock<IMemberRepository> _memberRepositoryMock;

        public ChangeMemberStatusCommandHandlerTests()
        {
            _memberRepositoryMock = MemberRepositoryMock.GetMemberRepository();
        }

        [Fact()]
        public async Task Handle_ForValidCommand_ReturnSuccessResponse()
        {
            // Arrange
            var items = await _memberRepositoryMock.Object.GetAllAsync();
            var handler = new ChangeMemberStatusCommandHandler(_memberRepositoryMock.Object);
            var command = new ChangeMemberStatusCommand()
            {
                Id = items.First().Id,
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            response.Success.Should().BeTrue();
        }

        [Fact()]
        public async Task Handle_StatusChanged_ReturnOppositeStatus()
        {
            // Arrange
            var items = await _memberRepositoryMock.Object.GetAllAsync();
            var handler = new ChangeMemberStatusCommandHandler(_memberRepositoryMock.Object);
            var statusBefore = (await _memberRepositoryMock.Object.GetByIdAsync(items.First().Id)).Status;
            var command = new ChangeMemberStatusCommand()
            {
                Id = items.First().Id,
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);
            var statusAfter = (await _memberRepositoryMock.Object.GetByIdAsync(items.First().Id)).Status;

            // Assert
            statusAfter.Should().NotBe(statusBefore);
        }
    }
}