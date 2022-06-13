using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.CQRS.Memberships.Commands.ChangeMembershipType;
using GymMGMT.Application.Tests.Mocks;

namespace GymMGMT.Application.Tests.CQRS.Memberships
{
    public class ChangeMembershipTypeCommandHandlerTests
    {
        private Mock<IMembershipRepository> _membershipRepositoryMock;
        private Mock<IMembershipTypeRepository> _membershipTypeRepositoryMock;

        public ChangeMembershipTypeCommandHandlerTests()
        {
            _membershipRepositoryMock = MembershipRepositoryMock.GetMembershipRepository();
            _membershipTypeRepositoryMock = MembershipTypeRepositoryMock.GetMembershipTypeRepository();
        }

        [Fact()]
        public async Task Handle_ForValidCommand_ReturnSuccessResponse()
        {
            // Arrange
            var memberships = await _membershipRepositoryMock.Object.GetAllAsync();
            var membershipTypes = await _membershipTypeRepositoryMock.Object.GetAllAsync();
            foreach (var membership in memberships)
            {
                membership.MembershipType = membershipTypes.ToList().ElementAt(1);
                membership.MembershipTypeId = membershipTypes.ToList().ElementAt(1).Id;
            }
            var handler = new ChangeMembershipTypeCommandHandler(_membershipTypeRepositoryMock.Object,
                _membershipRepositoryMock.Object);
            var command = new ChangeMembershipTypeCommand()
            {
                MemberId = memberships.ToList().ElementAt(1).MemberId,
                NewMembershipTypeId = membershipTypes.ToList().ElementAt(4).Id,
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            response.Success.Should().BeTrue();
        }

        [Fact()]
        public async Task Handle_MembershipTypeChanged_ReturnNewMembershipType()
        {
            // Arrange
            var memberships = await _membershipRepositoryMock.Object.GetAllAsync();
            var membershipTypes = await _membershipTypeRepositoryMock.Object.GetAllAsync();
            var typeBefore = (await _membershipRepositoryMock.Object.GetByIdAsync(memberships.ToList().ElementAt(1).Id)).MembershipTypeId;
            foreach (var membership in memberships)
            {
                membership.MembershipType = membershipTypes.ToList().ElementAt(1);
                membership.MembershipTypeId = membershipTypes.ToList().ElementAt(1).Id;
            }
            var handler = new ChangeMembershipTypeCommandHandler(_membershipTypeRepositoryMock.Object,
                _membershipRepositoryMock.Object);
            var command = new ChangeMembershipTypeCommand()
            {
                MemberId = memberships.ToList().ElementAt(1).MemberId,
                NewMembershipTypeId = membershipTypes.ToList().ElementAt(4).Id,
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);
            var typeAfter = (await _membershipRepositoryMock.Object.GetByIdAsync(memberships.ToList().ElementAt(1).Id)).MembershipTypeId;

            // Assert
            typeAfter.Should().NotBe(typeBefore);
        }
    }
}