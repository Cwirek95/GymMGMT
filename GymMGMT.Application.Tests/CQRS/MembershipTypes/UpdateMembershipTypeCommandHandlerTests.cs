using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.CQRS.MembershipTypes.Commands.UpdateMembershipType;
using GymMGMT.Application.Tests.Mocks;

namespace GymMGMT.Application.Tests.CQRS.MembershipTypes
{
    public class UpdateMembershipTypeCommandHandlerTests
    {
        private Mock<IMembershipTypeRepository> _membershipTypeRepositoryMock;

        public UpdateMembershipTypeCommandHandlerTests()
        {
            _membershipTypeRepositoryMock = MembershipTypeRepositoryMock.GetMembershipTypeRepository();
        }

        [Fact()]
        public async Task Handle_ForValidCommand_ReturnSuccessResponse()
        {
            // Arrange
            var items = await _membershipTypeRepositoryMock.Object.GetAllAsync();
            var handler = new UpdateMembershipTypeCommandHandler(_membershipTypeRepositoryMock.Object);
            var command = new UpdateMembershipTypeCommand()
            {
                Id = items.ToList().ElementAt(5).Id,
                Name = "UpdatedName"
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            response.Success.Should().BeTrue();
        }

        [Fact()]
        public async Task Handle_NameChanged_ReturnNewName()
        {
            // Arrange
            var items = await _membershipTypeRepositoryMock.Object.GetAllAsync();
            var handler = new UpdateMembershipTypeCommandHandler(_membershipTypeRepositoryMock.Object);
            var nameBefore = (await _membershipTypeRepositoryMock.Object.GetByIdAsync(items.First().Id)).Name;
            var command = new UpdateMembershipTypeCommand()
            {
                Id = items.First().Id,
                Name = "UpdatedName"
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);
            var nameAfter = (await _membershipTypeRepositoryMock.Object.GetByIdAsync(items.First().Id)).Name;


            // Assert
            nameAfter.Should().NotBe(nameBefore);
        }
    }
}