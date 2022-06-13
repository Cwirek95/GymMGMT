using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.CQRS.MembershipTypes.Commands.ChangeDefaultPrice;
using GymMGMT.Application.Tests.Mocks;

namespace GymMGMT.Application.Tests.CQRS.MembershipTypes
{
    public class ChangeDefaultPriceCommandValidatorTests
    {
        private Mock<IMembershipTypeRepository> _membershipTypeRepositoryMock;

        public ChangeDefaultPriceCommandValidatorTests()
        {
            _membershipTypeRepositoryMock = MembershipTypeRepositoryMock.GetMembershipTypeRepository();
        }

        [Fact()]
        public async Task Validate_ForZeroPrice_ReturnInvalidValidation()
        {
            // Arrange
            var validator = new ChangeDefaultPriceCommandValidator();
            var items = await _membershipTypeRepositoryMock.Object.GetAllAsync();
            var command = new ChangeDefaultPriceCommand()
            {
                Id = items.ToList().ElementAt(5).Id,
                DefaultPrice = 0,
            };

            // Act
            var response = validator.Validate(command);

            // Assert
            response.IsValid.Should().BeFalse();
        }

        [Theory()]
        [InlineData(12.345)]
        [InlineData(126.34522)]
        [InlineData(119.33432)]
        [InlineData(-0.345)]
        public async Task Validate_ForWrongPriceFormat_ReturnInvalidValidationAsync(double price)
        {
            // Arrange
            var validator = new ChangeDefaultPriceCommandValidator();
            var items = await _membershipTypeRepositoryMock.Object.GetAllAsync();
            var command = new ChangeDefaultPriceCommand()
            {
                Id = items.ToList().ElementAt(5).Id,
                DefaultPrice = price,
            };

            // Act
            var response = validator.Validate(command);

            // Assert
            response.IsValid.Should().BeFalse();
        }
    }
}