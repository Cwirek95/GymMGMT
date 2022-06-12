using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.CQRS.Memberships.Commands.ChangeMembershipPrice;
using GymMGMT.Application.Tests.Mocks;

namespace GymMGMT.Application.Tests.CQRS.Memberships
{
    public class ChangeMembershipPriceCommandValidatorTests
    {
        private Mock<IMembershipRepository> _membershipRepositoryMock;

        public ChangeMembershipPriceCommandValidatorTests()
        {
            _membershipRepositoryMock = MembershipRepositoryMock.GetMembershipRepository();
        }

        [Theory()]
        [InlineData(0)]
        [InlineData(-8.98)]
        [InlineData(-29.99)]
        public async Task Validate_ForPriceLessOrEqualZero_ReturnInvalidValidation(double price)
        {
            // Arrange
            var validator = new ChangeMembershipPriceCommandValidator();
            var items = await _membershipRepositoryMock.Object.GetAllAsync();
            var command = new ChangeMembershipPriceCommand()
            {
                Id = items.ToList().ElementAt(5).Id,
                Price = price
            };

            // Act
            var response = validator.Validate(command);

            // Assert
            response.IsValid.Should().BeFalse();
        }
    }
}