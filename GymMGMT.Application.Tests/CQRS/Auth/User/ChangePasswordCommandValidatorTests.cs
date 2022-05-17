using GymMGMT.Application.CQRS.Auth.Commands.ChangePassword;
using GymMGMT.Application.Tests.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMGMT.Application.Tests.CQRS.Auth.User
{
    public class ChangePasswordCommandValidatorTests
    {
        [Fact()]
        public void Validate_ForEmptyOldPassword_ReturnInvalidValidation()
        {
            // Arrange
            var validator = new ChangePasswordCommandValidator();
            var command = new ChangePasswordCommand()
            {
                Id = Guid.NewGuid(),
                OldPassword = "",
                NewPassword = "12345678"
            };

            // Act
            var response = validator.Validate(command);

            // Assert
            response.IsValid.Should().BeFalse();
        }

        [Fact()]
        public void Validate_ForEmptyNewPassword_ReturnInvalidValidation()
        {
            // Arrange
            var validator = new ChangePasswordCommandValidator();
            var command = new ChangePasswordCommand()
            {
                Id = Guid.NewGuid(),
                OldPassword = "12345678",
                NewPassword = ""
            };

            // Act
            var response = validator.Validate(command);

            // Assert
            response.IsValid.Should().BeFalse();
        }

        [Fact()]
        public void Validate_ForTooShortOldPassword_ReturnInvalidValidation()
        {
            // Arrange
            var validator = new ChangePasswordCommandValidator();
            var command = new ChangePasswordCommand()
            {
                Id = Guid.NewGuid(),
                OldPassword = new string('A', 4),
                NewPassword = "12345678"
            };

            // Act
            var response = validator.Validate(command);

            // Assert
            response.IsValid.Should().BeFalse();
        }

        [Fact()]
        public void Validate_ForTooShortNewPassword_ReturnInvalidValidation()
        {
            // Arrange
            var validator = new ChangePasswordCommandValidator();
            var command = new ChangePasswordCommand()
            {
                Id = Guid.NewGuid(),
                OldPassword = "12345678",
                NewPassword = new string('A', 4)
            };

            // Act
            var response = validator.Validate(command);

            // Assert
            response.IsValid.Should().BeFalse();
        }

        [Fact()]
        public void Validate_ForTooLongOldPassword_ReturnInvalidValidation()
        {
            // Arrange
            var validator = new ChangePasswordCommandValidator();
            var command = new ChangePasswordCommand()
            {
                Id = Guid.NewGuid(),
                OldPassword = new string('A', 1025),
                NewPassword = "12345678"
            };

            // Act
            var response = validator.Validate(command);

            // Assert
            response.IsValid.Should().BeFalse();
        }

        [Fact()]
        public void Validate_ForTooLongNewPassword_ReturnInvalidValidation()
        {
            // Arrange
            var validator = new ChangePasswordCommandValidator();
            var command = new ChangePasswordCommand()
            {
                Id = Guid.NewGuid(),
                OldPassword = "12345678",
                NewPassword = new string('A', 1025)
            };

            // Act
            var response = validator.Validate(command);

            // Assert
            response.IsValid.Should().BeFalse();
        }
    }
}