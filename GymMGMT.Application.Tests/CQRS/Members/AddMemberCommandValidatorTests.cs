using GymMGMT.Application.CQRS.Members.Commands.AddMember;

namespace GymMGMT.Application.Tests.CQRS.Members
{
    public class AddMemberCommandValidatorTests
    {
        [Fact()]
        public void Validate_ForEmptyFirstName_ReturnInvalidValidation()
        {
            // Arrange
            var validator = new AddMemberCommandValidator();
            var command = new AddMemberCommand()
            {
                FirstName = "",
                LastName = "LName",
                DateOfBirth = DateTimeOffset.Now.AddYears(-20),
                PhoneNumber = "+48123456789",
                UserId = Guid.NewGuid(),
                MembershipId = new Random().Next(0, 100)
            };

            // Act
            var response = validator.Validate(command);

            // Assert
            response.IsValid.Should().BeFalse();
        }

        [Fact()]
        public void Validate_ForEmptyLastName_ReturnInvalidValidation()
        {
            // Arrange
            var validator = new AddMemberCommandValidator();
            var command = new AddMemberCommand()
            {
                FirstName = "FName",
                LastName = "",
                DateOfBirth = DateTimeOffset.Now.AddYears(-20),
                PhoneNumber = "+48123456789",
                UserId = Guid.NewGuid(),
                MembershipId = new Random().Next(0, 100)
            };

            // Act
            var response = validator.Validate(command);

            // Assert
            response.IsValid.Should().BeFalse();
        }

        [Fact()]
        public void Validate_ForTooLongFirstName_ReturnInvalidValidation()
        {
            // Arrange
            var validator = new AddMemberCommandValidator();
            var command = new AddMemberCommand()
            {
                FirstName = new string('A', 257),
                LastName = "LName",
                DateOfBirth = DateTimeOffset.Now.AddYears(-20),
                PhoneNumber = "+48123456789",
                UserId = Guid.NewGuid(),
                MembershipId = new Random().Next(0, 100)
            };

            // Act
            var response = validator.Validate(command);

            // Assert
            response.IsValid.Should().BeFalse();
        }

        [Fact()]
        public void Validate_ForEmptyPhoneNumber_ReturnInvalidValidation()
        {
            // Arrange
            var validator = new AddMemberCommandValidator();
            var command = new AddMemberCommand()
            {
                FirstName = "FName",
                LastName = "LName",
                DateOfBirth = DateTimeOffset.Now.AddYears(-20),
                PhoneNumber = "",
                UserId = Guid.NewGuid(),
                MembershipId = new Random().Next(0, 100)
            };

            // Act
            var response = validator.Validate(command);

            // Assert
            response.IsValid.Should().BeFalse();
        }

        [Fact()]
        public void Validate_ForTooLongPhoneNumber_ReturnInvalidValidation()
        {
            // Arrange
            var validator = new AddMemberCommandValidator();
            var command = new AddMemberCommand()
            {
                FirstName = "FName",
                LastName = "LName",
                DateOfBirth = DateTimeOffset.Now.AddYears(-20),
                PhoneNumber = new String('A', 16),
                UserId = Guid.NewGuid(),
                MembershipId = new Random().Next(0, 100)
            };

            // Act
            var response = validator.Validate(command);

            // Assert
            response.IsValid.Should().BeFalse();
        }

        [Fact()]
        public void Validate_ForWrongPhoneNumberFormat_ReturnInvalidValidation()
        {
            // Arrange
            var validator = new AddMemberCommandValidator();
            var command = new AddMemberCommand()
            {
                FirstName = "FName",
                LastName = "LName",
                DateOfBirth = DateTimeOffset.Now.AddYears(-20),
                PhoneNumber = "+48la99328a",
                UserId = Guid.NewGuid(),
                MembershipId = new Random().Next(0, 100)
            };

            // Act
            var response = validator.Validate(command);

            // Assert
            response.IsValid.Should().BeFalse();
        }
    }
}