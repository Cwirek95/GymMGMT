using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.CQRS.Memberships.Commands.CheckMembershipValidity;
using GymMGMT.Application.Tests.Mocks;

namespace GymMGMT.Application.Tests.CQRS.Memberships
{
    public class CheckMembershipValidityCommandHandlerTests
    {
        private Mock<IMembershipRepository> _membershipRepositoryMock;

        public CheckMembershipValidityCommandHandlerTests()
        {
            _membershipRepositoryMock = MembershipRepositoryMock.GetMembershipRepository();
        }

        [Fact()]
        public async Task Handle_ForValidCommand_ReturnSuccessResponse()
        {
            // Arrange
            var items = await _membershipRepositoryMock.Object.GetAllAsync();
            var handler = new CheckMembershipValidityCommandHandler(_membershipRepositoryMock.Object);
            var command = new CheckMembershipValidityCommand()
            {
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            response.Success.Should().BeTrue();
        }
    }
}