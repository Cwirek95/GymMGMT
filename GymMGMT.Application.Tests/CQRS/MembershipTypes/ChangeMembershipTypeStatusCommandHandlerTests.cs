using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.CQRS.MembershipTypes.Commands.ChangeMembershipTypeStatus;
using GymMGMT.Application.Tests.Mocks;

namespace GymMGMT.Application.Tests.CQRS.MembershipTypes
{
    public class ChangeMembershipTypeStatusCommandHandlerTests
    {
        private Mock<IMembershipTypeRepository> _membershipTypeRepositoryMock;

        public ChangeMembershipTypeStatusCommandHandlerTests()
        {
            _membershipTypeRepositoryMock = MembershipTypeRepositoryMock.GetMembershipTypeRepository();
        }

        [Fact()]
        public async Task Handle_ForValidCommand_ReturnSuccessResponse()
        {
            // Arrange
            var items = await _membershipTypeRepositoryMock.Object.GetAllAsync();
            var handler = new ChangeMembershipTypeStatusCommandHandler(_membershipTypeRepositoryMock.Object);
            var command = new ChangeMembershipTypeStatusCommand()
            {
                Id = items.ToList().ElementAt(5).Id
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
            var items = await _membershipTypeRepositoryMock.Object.GetAllAsync();
            var handler = new ChangeMembershipTypeStatusCommandHandler(_membershipTypeRepositoryMock.Object);
            var statusBefore = (await _membershipTypeRepositoryMock.Object.GetByIdAsync(items.First().Id)).Status;
            var command = new ChangeMembershipTypeStatusCommand()
            {
                Id = items.First().Id,
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);
            var statusAfter = (await _membershipTypeRepositoryMock.Object.GetByIdAsync(items.First().Id)).Status;


            // Assert
            statusAfter.Should().NotBe(statusBefore);
        }
    }
}