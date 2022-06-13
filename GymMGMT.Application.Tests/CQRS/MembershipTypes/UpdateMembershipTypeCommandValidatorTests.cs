using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.CQRS.MembershipTypes.Commands.UpdateMembershipType;
using GymMGMT.Application.Tests.Mocks;

namespace GymMGMT.Application.Tests.CQRS.MembershipTypes
{
    public class UpdateMembershipTypeCommandValidatorTests
    {
        private Mock<IMembershipTypeRepository> _membershipTypeRepositoryMock;

        public UpdateMembershipTypeCommandValidatorTests()
        {
            _membershipTypeRepositoryMock = MembershipTypeRepositoryMock.GetMembershipTypeRepository();
        }

        [Fact()]
        public async Task Validate_ForEmptyName_ReturnInvalidValidationAsync()
        {
            // Arrange
            var validator = new UpdateMembershipTypeCommandValidator();
            var items = await _membershipTypeRepositoryMock.Object.GetAllAsync();
            var command = new UpdateMembershipTypeCommand()
            {
                Id = items.ToList().ElementAt(5).Id,
                Name = "",
            };

            // Act
            var response = validator.Validate(command);

            // Assert
            response.IsValid.Should().BeFalse();
        }

        [Fact()]
        public async Task Validate_ForTooLongName_ReturnInvalidValidationAsync()
        {
            // Arrange
            var validator = new UpdateMembershipTypeCommandValidator();
            var items = await _membershipTypeRepositoryMock.Object.GetAllAsync();
            var command = new UpdateMembershipTypeCommand()
            {
                Id = items.ToList().ElementAt(5).Id,
                Name = new string('A', 129),
            };

            // Act
            var response = validator.Validate(command);

            // Assert
            response.IsValid.Should().BeFalse();
        }
    }
}
