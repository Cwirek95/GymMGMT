using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.CQRS.Members.Commands.DeleteMember;
using GymMGMT.Application.Tests.Mocks;
using MediatR;

namespace GymMGMT.Application.Tests.CQRS.Members
{
    public class DeleteMemberCommandHandlerTests
    {
        private Mock<IMemberRepository> _memberRepositoryMock;
        private Mock<IMediator> _mediator;

        public DeleteMemberCommandHandlerTests()
        {
            _memberRepositoryMock = MemberRepositoryMock.GetMemberRepository();
            _mediator = new Mock<IMediator>();
        }

        [Fact()]
        public async Task Handle_ForValidCommand_ReturnSuccessResponse()
        {
            // Arrange
            var items = await _memberRepositoryMock.Object.GetAllAsync();
            var handler = new DeleteMemberCommandHandler(_memberRepositoryMock.Object, _mediator.Object);
            var command = new DeleteMemberCommand()
            {
                Id = items.ToList().ElementAt(5).Id
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            response.Success.Should().BeTrue();
        }

        [Fact()]
        public async Task Handle_MemberDeleted_ReturnOneLessMembers()
        {
            // Arrange
            var items = await _memberRepositoryMock.Object.GetAllAsync();
            var handler = new DeleteMemberCommandHandler(_memberRepositoryMock.Object, _mediator.Object);
            var countBefore = (await _memberRepositoryMock.Object.GetAllAsync()).Count();
            var command = new DeleteMemberCommand()
            {
                Id = items.ToList().ElementAt(5).Id
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);
            var countAfter = (await _memberRepositoryMock.Object.GetAllAsync()).Count();

            // Assert
            countAfter.Should().Be(countBefore - 1);
        }
    }
}
