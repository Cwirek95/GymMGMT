using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.CQRS.Memberships.Commands.ChangeMembershipPrice;
using GymMGMT.Application.Tests.Mocks;

namespace GymMGMT.Application.Tests.CQRS.Memberships
{
    public class ChangeMembershipPriceCommandHandlerTests
    {
        private Mock<IMembershipRepository> _membershipRepositoryMock;

        public ChangeMembershipPriceCommandHandlerTests()
        {
            _membershipRepositoryMock = MembershipRepositoryMock.GetMembershipRepository();
        }

        [Fact()]
        public async Task Handle_ForValidCommand_ReturnSuccessResponse()
        {
            // Arrange
            var items = await _membershipRepositoryMock.Object.GetAllAsync();
            var handler = new ChangeMembershipPriceCommandHandler(_membershipRepositoryMock.Object);
            var command = new ChangeMembershipPriceCommand()
            {
                Id = items.ToList().ElementAt(5).Id,
                Price = 89.90
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            response.Success.Should().BeTrue();
        }

        [Fact()]
        public async Task Handle_PriceChanged_ReturnNewPrice()
        {
            // Arrange
            var items = await _membershipRepositoryMock.Object.GetAllAsync();
            var handler = new ChangeMembershipPriceCommandHandler(_membershipRepositoryMock.Object);
            var priceBefore = (await _membershipRepositoryMock.Object.GetByIdAsync(items.First().Id)).Price;
            var command = new ChangeMembershipPriceCommand()
            {
                Id = items.First().Id,
                Price = 89.90
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);
            var priceAfter = (await _membershipRepositoryMock.Object.GetByIdAsync(items.First().Id)).Price;

            // Assert
            priceAfter.Should().NotBe(priceBefore);
        }
    }
}