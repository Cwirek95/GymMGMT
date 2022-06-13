using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.CQRS.Memberships.Commands.DeleteMembership;
using GymMGMT.Application.Tests.Mocks;

namespace GymMGMT.Application.Tests.CQRS.Memberships
{
    public class DeleteMembershipCommandHandlerTests
    {
        private Mock<IMembershipRepository> _membershipRepositoryMock;

        public DeleteMembershipCommandHandlerTests()
        {
            _membershipRepositoryMock = MembershipRepositoryMock.GetMembershipRepository();
        }

        [Fact()]
        public async Task Handle_ForValidCommand_ReturnSuccessResponse()
        {
            // Arrange
            var items = await _membershipRepositoryMock.Object.GetAllAsync();
            var handler = new DeleteMembershipCommandHandler(_membershipRepositoryMock.Object);
            var command = new DeleteMembershipCommand()
            {
                Id = items.ToList().ElementAt(5).Id
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            response.Success.Should().BeTrue();
        }

        [Fact()]
        public async Task Handle_MembershipDeleted_ReturnOneLessMemberships()
        {
            // Arrange
            var items = await _membershipRepositoryMock.Object.GetAllAsync();
            var handler = new DeleteMembershipCommandHandler(_membershipRepositoryMock.Object);
            var countBefore = (await _membershipRepositoryMock.Object.GetAllAsync()).Count();
            var command = new DeleteMembershipCommand()
            {
                Id = items.ToList().ElementAt(3).Id
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);
            var countAfter = (await _membershipRepositoryMock.Object.GetAllAsync()).Count();

            // Assert
            countAfter.Should().Be(countBefore - 1);
        }
    }
}