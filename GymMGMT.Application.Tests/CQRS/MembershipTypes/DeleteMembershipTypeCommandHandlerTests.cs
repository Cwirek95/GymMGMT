using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.CQRS.MembershipTypes.Commands.DeleteMembershipType;
using GymMGMT.Application.Tests.Mocks;

namespace GymMGMT.Application.Tests.CQRS.MembershipTypes
{
    public class DeleteMembershipTypeCommandHandlerTests
    {
        private Mock<IMembershipTypeRepository> _membershipTypeRepositoryMock;

        public DeleteMembershipTypeCommandHandlerTests()
        {
            _membershipTypeRepositoryMock = MembershipTypeRepositoryMock.GetMembershipTypeRepository();
        }

        [Fact()]
        public async Task Handle_ForValidCommand_ReturnSuccessResponse()
        {
            // Arrange
            var items = await _membershipTypeRepositoryMock.Object.GetAllAsync();
            var handler = new DeleteMembershipTypeCommandHandler(_membershipTypeRepositoryMock.Object);
            var command = new DeleteMembershipTypeCommand()
            {
                Id = items.ToList().ElementAt(5).Id
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            response.Success.Should().BeTrue();
        }

        [Fact()]
        public async Task Handle_MembershipTypeDeleted_ReturnOneLessMembershipTypes()
        {
            // Arrange
            var items = await _membershipTypeRepositoryMock.Object.GetAllAsync();
            var handler = new DeleteMembershipTypeCommandHandler(_membershipTypeRepositoryMock.Object);
            var countBefore = (await _membershipTypeRepositoryMock.Object.GetAllAsync()).Count;
            var command = new DeleteMembershipTypeCommand()
            {
                Id = items.ToList().ElementAt(5).Id
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);
            var countAfter = (await _membershipTypeRepositoryMock.Object.GetAllAsync()).Count;

            // Assert
            countAfter.Should().Be(countBefore - 1);
        }
    }
}