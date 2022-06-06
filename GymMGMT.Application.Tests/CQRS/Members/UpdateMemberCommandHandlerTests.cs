using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.CQRS.Members.Commands.UpdateMember;
using GymMGMT.Application.Tests.Mocks;

namespace GymMGMT.Application.Tests.CQRS.Members
{
    public class UpdateMemberCommandHandlerTests
    {
        private Mock<IMemberRepository> _memberRepositoryMock;

        public UpdateMemberCommandHandlerTests()
        {
            _memberRepositoryMock = MemberRepositoryMock.GetMemberRepository();
        }

        [Fact()]
        public async Task Handle_ForValidCommand_ReturnSuccessResponse()
        {
            // Arrange
            var items = await _memberRepositoryMock.Object.GetAllAsync();
            var handler = new UpdateMemberCommandHandler(_memberRepositoryMock.Object);
            var command = new UpdateMemberCommand()
            {
                Id = items.First().Id,
                FirstName = "FnameU",
                LastName = "LNameU",
                DateOfBirth = DateTimeOffset.Now.AddYears(-10),
                PhoneNumber = "+48123456987",
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            response.Success.Should().BeTrue();
        }

        [Fact()]
        public async Task Handle_ChangeFirstName_ReturnMemberWithNewFirstName()
        {
            // Arrange
            var items = await _memberRepositoryMock.Object.GetAllAsync();
            var handler = new UpdateMemberCommandHandler(_memberRepositoryMock.Object);
            var fNameBefore = (await _memberRepositoryMock.Object.GetByIdAsync(items.First().Id)).FirstName;
            var command = new UpdateMemberCommand()
            {
                Id = items.First().Id,
                FirstName = "FnameU",
                LastName = "LName",
                DateOfBirth = DateTimeOffset.Now.AddYears(-20),
                PhoneNumber = "+48123456789",
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);
            var fNameAfter = (await _memberRepositoryMock.Object.GetByIdAsync(items.First().Id)).FirstName;

            // Assert
            fNameAfter.Should().NotBe(fNameBefore);
        }
    }
}
