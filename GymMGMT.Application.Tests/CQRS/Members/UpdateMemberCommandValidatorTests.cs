using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.CQRS.Members.Commands.UpdateMember;
using GymMGMT.Application.Tests.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMGMT.Application.Tests.CQRS.Members
{
    public class UpdateMemberCommandValidatorTests
    {
        private Mock<IMemberRepository> _memberRepositoryMock;

        public UpdateMemberCommandValidatorTests()
        {
            _memberRepositoryMock = MemberRepositoryMock.GetMemberRepository();
        }

        [Fact()]
        public async Task Handle_ForEmptyFirstName_ReturnInvalidValidation()
        {
            // Arrange
            var items = await _memberRepositoryMock.Object.GetAllAsync();
            var validator = new UpdateMemberCommandValidator();
            var command = new UpdateMemberCommand()
            {
                Id = items.First().Id,
                FirstName = "",
                LastName = "LName",
                DateOfBirth = DateTimeOffset.Now.AddYears(-10),
                PhoneNumber = "+48123456987",
            };

            // Act
            var response = validator.Validate(command);

            // Assert
            response.IsValid.Should().BeFalse();
        }

        [Fact()]
        public async Task Handle_ForEmptyLastName_ReturnInvalidValidation()
        {
            // Arrange
            var items = await _memberRepositoryMock.Object.GetAllAsync();
            var validator = new UpdateMemberCommandValidator();
            var command = new UpdateMemberCommand()
            {
                Id = items.First().Id,
                FirstName = "FName",
                LastName = "",
                DateOfBirth = DateTimeOffset.Now.AddYears(-10),
                PhoneNumber = "+48123456987",
            };

            // Act
            var response = validator.Validate(command);

            // Assert
            response.IsValid.Should().BeFalse();
        }

        [Fact()]
        public async Task Handle_ForEmptyPhoneNumber_ReturnInvalidValidation()
        {
            // Arrange
            var items = await _memberRepositoryMock.Object.GetAllAsync();
            var validator = new UpdateMemberCommandValidator();
            var command = new UpdateMemberCommand()
            {
                Id = items.First().Id,
                FirstName = "",
                LastName = "LNameU",
                DateOfBirth = DateTimeOffset.Now.AddYears(-10),
                PhoneNumber = "",
            };

            // Act
            var response = validator.Validate(command);

            // Assert
            response.IsValid.Should().BeFalse();
        }

        [Fact()]
        public async Task Handle_ForTooLongFirstName_ReturnInvalidValidation()
        {
            // Arrange
            var items = await _memberRepositoryMock.Object.GetAllAsync();
            var validator = new UpdateMemberCommandValidator();
            var command = new UpdateMemberCommand()
            {
                Id = items.First().Id,
                FirstName = new string('A', 257),
                LastName = "LName",
                DateOfBirth = DateTimeOffset.Now.AddYears(-10),
                PhoneNumber = "+48123456987",
            };

            // Act
            var response = validator.Validate(command);

            // Assert
            response.IsValid.Should().BeFalse();
        }

        [Fact()]
        public async Task Handle_ForTooLongLastName_ReturnInvalidValidation()
        {
            // Arrange
            var items = await _memberRepositoryMock.Object.GetAllAsync();
            var validator = new UpdateMemberCommandValidator();
            var command = new UpdateMemberCommand()
            {
                Id = items.First().Id,
                FirstName = "FName",
                LastName = new string('A', 257),
                DateOfBirth = DateTimeOffset.Now.AddYears(-10),
                PhoneNumber = "+48123456987",
            };

            // Act
            var response = validator.Validate(command);

            // Assert
            response.IsValid.Should().BeFalse();
        }

        [Fact()]
        public async Task Handle_ForTooLongPhoneNumber_ReturnInvalidValidation()
        {
            // Arrange
            var items = await _memberRepositoryMock.Object.GetAllAsync();
            var validator = new UpdateMemberCommandValidator();
            var command = new UpdateMemberCommand()
            {
                Id = items.First().Id,
                FirstName = "FName",
                LastName = "LName",
                DateOfBirth = DateTimeOffset.Now.AddYears(-10),
                PhoneNumber = new string('1', 16),
            };

            // Act
            var response = validator.Validate(command);

            // Assert
            response.IsValid.Should().BeFalse();
        }

        [Fact()]
        public async Task Handle_WrongPhoneNumberFormat_ReturnInvalidValidation()
        {
            // Arrange
            var items = await _memberRepositoryMock.Object.GetAllAsync();
            var validator = new UpdateMemberCommandValidator();
            var command = new UpdateMemberCommand()
            {
                Id = items.First().Id,
                FirstName = "FName",
                LastName = "LName",
                DateOfBirth = DateTimeOffset.Now.AddYears(-10),
                PhoneNumber = "+48187assd8",
            };

            // Act
            var response = validator.Validate(command);

            // Assert
            response.IsValid.Should().BeFalse();
        }
    }
}