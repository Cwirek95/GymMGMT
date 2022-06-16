using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.CQRS.Memberships.Commands.SetDefaultPriceForCurrentMembers;
using GymMGMT.Application.Tests.Mocks;

namespace GymMGMT.Application.Tests.CQRS.Memberships
{
    public class SetDefaultPriceForCurrentMembersCommandHandlerTests
    {
        private Mock<IMembershipRepository> _membershipRepositoryMock;
        private Mock<IMembershipTypeRepository> _membershipTypeRepositoryMock;

        public SetDefaultPriceForCurrentMembersCommandHandlerTests()
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
            var handler = new SetDefaultPriceForCurrentMembersCommandHandler(_membershipRepositoryMock.Object, _membershipTypeRepositoryMock.Object);
            var command = new SetDefaultPriceForCurrentMembersCommand()
            {
                MembershipTypeId = membershipTypes.ToList().ElementAt(1).Id
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            response.Success.Should().BeTrue();
        }

        [Fact()]
        public async Task Handle_PriceSetToDefaultPrice_ReturnNewPrice()
        {
            // Arrange
            var memberships = await _membershipRepositoryMock.Object.GetAllAsync();
            var membershipTypes = await _membershipTypeRepositoryMock.Object.GetAllAsync();
            var priceBefore = (await _membershipRepositoryMock.Object.GetByIdAsync(memberships.ToList().ElementAt(1).Id)).Price;
            foreach (var membership in memberships)
            {
                membership.MembershipType = membershipTypes.ToList().ElementAt(1);
                membership.MembershipTypeId = membershipTypes.ToList().ElementAt(1).Id;
            }
            var handler = new SetDefaultPriceForCurrentMembersCommandHandler(_membershipRepositoryMock.Object, _membershipTypeRepositoryMock.Object);
            var command = new SetDefaultPriceForCurrentMembersCommand()
            {
                MembershipTypeId = membershipTypes.ToList().ElementAt(1).Id
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);
            var priceAfter = (await _membershipRepositoryMock.Object.GetByIdAsync(memberships.ToList().ElementAt(1).Id)).Price;

            // Assert
            priceAfter.Should().NotBe(priceBefore);
        }
    }
}