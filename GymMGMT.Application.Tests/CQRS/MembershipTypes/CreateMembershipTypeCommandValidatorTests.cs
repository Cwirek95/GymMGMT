using GymMGMT.Application.CQRS.MembershipTypes.Commands.CreateMembershipType;

namespace GymMGMT.Application.Tests.CQRS.MembershipTypes
{
    public class CreateMembershipTypeCommandValidatorTests
    {
        [Fact()]
        public void Validate_ForEmptyName_ReturnInvalidValidation()
        {
            // Arrange
            var validator = new CreateMembershipTypeCommandValidator();
            var command = new CreateMembershipTypeCommand()
            {
                Name = "",
                DefaultPrice = 12.45,
                DurationInDays = 30
            };

            // Act
            var response = validator.Validate(command);

            // Assert
            response.IsValid.Should().BeFalse();
        }

        [Fact()]
        public void Validate_ForZeroPrice_ReturnInvalidValidation()
        {
            // Arrange
            var validator = new CreateMembershipTypeCommandValidator();
            var command = new CreateMembershipTypeCommand()
            {
                Name = "MTName",
                DefaultPrice = 0,
                DurationInDays = 30
            };

            // Act
            var response = validator.Validate(command);

            // Assert
            response.IsValid.Should().BeFalse();
        }

        [Fact()]
        public void Validate_ForZeroDuration_ReturnInvalidValidation()
        {
            // Arrange
            var validator = new CreateMembershipTypeCommandValidator();
            var command = new CreateMembershipTypeCommand()
            {
                Name = "MTName",
                DefaultPrice = 100.50,
                DurationInDays = 0
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
        public void Validate_ForWrongPriceFormat_ReturnInvalidValidation(double price)
        {
            // Arrange
            var validator = new CreateMembershipTypeCommandValidator();
            var command = new CreateMembershipTypeCommand()
            {
                Name = "MTName",
                DefaultPrice = price,
                DurationInDays = 30
            };

            // Act
            var response = validator.Validate(command);

            // Assert
            response.IsValid.Should().BeFalse();
        }
    }
}