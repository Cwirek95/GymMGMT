using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.CQRS.MembershipTypes.Commands.ChangeDefaultPrice;
using GymMGMT.Application.Tests.Mocks;

namespace GymMGMT.Application.Tests.CQRS.MembershipTypes
{
    public class ChangeDefaultPriceCommandHandlerTests
    {
        private Mock<IMembershipTypeRepository> _membershipTypeRepositoryMock;

        public ChangeDefaultPriceCommandHandlerTests()
        {
            _membershipTypeRepositoryMock = MembershipTypeRepositoryMock.GetMembershipTypeRepository();
        }

        [Fact()]
        public async Task Handle_ForValidCommand_ReturnSuccessResponse()
        {
            // Arrange
            var items = await _membershipTypeRepositoryMock.Object.GetAllAsync();
            var handler = new ChangeDefaultPriceCommandHandler(_membershipTypeRepositoryMock.Object);
            var command = new ChangeDefaultPriceCommand()
            {
                Id = items.ToList().ElementAt(5).Id,
                DefaultPrice = 199.92
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            response.Success.Should().BeTrue();
        }

        [Fact()]
        public async Task Handle_DefaultPriceChanged_ReturnNewDefaultPrice()
        {
            // Arrange
            var items = await _membershipTypeRepositoryMock.Object.GetAllAsync();
            var handler = new ChangeDefaultPriceCommandHandler(_membershipTypeRepositoryMock.Object);
            var priceBefore = (await _membershipTypeRepositoryMock.Object.GetByIdAsync(items.First().Id)).DefaultPrice;
            var command = new ChangeDefaultPriceCommand()
            {
                Id = items.First().Id,
                DefaultPrice = 121.12
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);
            var priceAfter = (await _membershipTypeRepositoryMock.Object.GetByIdAsync(items.First().Id)).DefaultPrice;


            // Assert
            priceAfter.Should().NotBe(priceBefore);
        }
    }
}