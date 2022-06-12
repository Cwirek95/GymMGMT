using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.CQRS.Memberships.Commands.ExtendMembership;
using GymMGMT.Application.Tests.Mocks;

namespace GymMGMT.Application.Tests.CQRS.Memberships
{
    public class ExtendMembershipCommandHandlerTests
    {
        private Mock<IMembershipRepository> _membershipRepositoryMock;
        private Mock<IMembershipTypeRepository> _membershipTypeRepositoryMock;

        public ExtendMembershipCommandHandlerTests()
        {
            _membershipRepositoryMock = MembershipRepositoryMock.GetMembershipRepository();
            _membershipTypeRepositoryMock = MembershipTypeRepositoryMock.GetMembershipTypeRepository();
        }

        [Fact()]
        public async Task Handle_ForValidCommand_ReturnSuccessResponse()
        {
            // Arrange
            var items = await _membershipRepositoryMock.Object.GetAllAsync();
            var membershipTypes = await _membershipTypeRepositoryMock.Object.GetAllAsync();
            items.ToList().ElementAt(5).MembershipType = membershipTypes.First();
            var handler = new ExtendMembershipCommandHandler(_membershipRepositoryMock.Object);
            var command = new ExtendMembershipCommand()
            {
                Id = items.ToList().ElementAt(5).Id
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            response.Success.Should().BeTrue();
        }

        [Fact()]
        public async Task Handle_MembershipExtended_ReturnNewLastExtensionDate()
        {
            // Arrange
            var items = await _membershipRepositoryMock.Object.GetAllAsync();
            var membershipTypes = await _membershipTypeRepositoryMock.Object.GetAllAsync();
            items.First().MembershipType = membershipTypes.First();
            var handler = new ExtendMembershipCommandHandler(_membershipRepositoryMock.Object);
            var extensionDateBefore = (await _membershipRepositoryMock.Object.GetByIdAsync(items.First().Id)).LastExtension;
            var command = new ExtendMembershipCommand()
            {
                Id = items.First().Id
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);
            var extensionDateAfter = (await _membershipRepositoryMock.Object.GetByIdAsync(items.First().Id)).LastExtension;

            // Assert
            extensionDateAfter.Should().NotBe(extensionDateBefore);
        }

        [Fact()]
        public async Task Handle_MembershipExtended_ReturnNewEndDate()
        {
            // Arrange
            var items = await _membershipRepositoryMock.Object.GetAllAsync();
            var membershipTypes = await _membershipTypeRepositoryMock.Object.GetAllAsync();
            items.Last().MembershipType = membershipTypes.First();
            var handler = new ExtendMembershipCommandHandler(_membershipRepositoryMock.Object);
            var extensionDateBefore = (await _membershipRepositoryMock.Object.GetByIdAsync(items.Last().Id)).EndDate;
            var command = new ExtendMembershipCommand()
            {
                Id = items.Last().Id
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);
            var extensionDateAfter = (await _membershipRepositoryMock.Object.GetByIdAsync(items.Last().Id)).EndDate;

            // Assert
            extensionDateAfter.Should().NotBe(extensionDateBefore);
        }
    }
}