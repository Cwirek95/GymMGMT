using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.CQRS.Memberships.Commands.ChangeMembershipStatus;
using GymMGMT.Application.Tests.Mocks;

namespace GymMGMT.Application.Tests.CQRS.Memberships
{
    public class ChangeMembershipStatusCommandHandlerTests
    {
        private Mock<IMembershipRepository> _membershipRepositoryMock;

        public ChangeMembershipStatusCommandHandlerTests()
        {
            _membershipRepositoryMock = MembershipRepositoryMock.GetMembershipRepository();
        }

        [Fact()]
        public async Task Handle_ForValidCommand_ReturnSuccessResponse()
        {
            // Arrange
            var memberships = await _membershipRepositoryMock.Object.GetAllAsync();
            var handler = new ChangeMembershipStatusCommandHandler(_membershipRepositoryMock.Object);
            var command = new ChangeMembershipStatusCommand()
            {
                Id = memberships.ToList().ElementAt(5).Id
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            response.Success.Should().BeTrue();
        }

        [Fact()]
        public async Task Handle_MembershipStatusChanged_ReturnOppositeStatus()
        {
            // Arrange
            var memberships = await _membershipRepositoryMock.Object.GetAllAsync();
            var statusBefore = (await _membershipRepositoryMock.Object.GetByIdAsync(memberships.ToList().ElementAt(1).Id)).Status;
            var handler = new ChangeMembershipStatusCommandHandler(_membershipRepositoryMock.Object);
            var command = new ChangeMembershipStatusCommand()
            {
                Id = memberships.ToList().ElementAt(1).Id
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);
            var statusAfter = (await _membershipRepositoryMock.Object.GetByIdAsync(memberships.ToList().ElementAt(1).Id)).Status;

            // Assert
            statusAfter.Should().NotBe(statusBefore);
        }
    }
}