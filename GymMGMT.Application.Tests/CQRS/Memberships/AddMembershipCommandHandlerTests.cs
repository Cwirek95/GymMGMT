using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.CQRS;
using GymMGMT.Application.CQRS.Memberships.Commands.AddMembership;
using GymMGMT.Application.Tests.Mocks;
using MediatR;

namespace GymMGMT.Application.Tests.CQRS.Memberships
{
    public class AddMembershipCommandHandlerTests
    {
        private IMapper _mapper;
        private Mock<IMemberRepository> _memberRepositoryMock;
        private Mock<IMembershipRepository> _membershipRepositoryMock;
        private Mock<IMembershipTypeRepository> _membershipTypeRepositoryMock;
        private Mock<IMediator> _mediator;

        public AddMembershipCommandHandlerTests()
        {
            var confProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = confProvider.CreateMapper();
            _mediator = new Mock<IMediator>();
            _memberRepositoryMock = MemberRepositoryMock.GetMemberRepository();
            _membershipRepositoryMock = MembershipRepositoryMock.GetMembershipRepository();
            _membershipTypeRepositoryMock = MembershipTypeRepositoryMock.GetMembershipTypeRepository();
        }

        [Fact()]
        public async Task Handle_ForValidCommand_ReturnSuccessResponse()
        {
            // Arrange
            var handler = new AddMembershipCommandHandler(_membershipRepositoryMock.Object, _membershipTypeRepositoryMock.Object,
                _memberRepositoryMock.Object, _mapper, _mediator.Object);
            var members = await _memberRepositoryMock.Object.GetAllAsync();
            var membershipTypes = await _membershipTypeRepositoryMock.Object.GetAllAsync();
            var command = new AddMembershipCommand()
            {
                MemberId = members.First().Id,
                MembershipTypeId = membershipTypes.First().Id,
                Price = 79.90
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            response.Success.Should().BeTrue();
        }

        [Fact()]
        public async Task Handle_ForValidCommand_ReturnOneMoreMemberships()
        {
            // Arrange
            var handler = new AddMembershipCommandHandler(_membershipRepositoryMock.Object, _membershipTypeRepositoryMock.Object,
                _memberRepositoryMock.Object, _mapper, _mediator.Object);
            var members = await _memberRepositoryMock.Object.GetAllAsync();
            var membershipTypes = await _membershipTypeRepositoryMock.Object.GetAllAsync();
            var countBefore = (await _membershipRepositoryMock.Object.GetAllAsync()).Count;
            var command = new AddMembershipCommand()
            {
                MemberId = members.First().Id,
                MembershipTypeId = membershipTypes.First().Id,
                Price = 79.90
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);
            var countAfter = (await _membershipRepositoryMock.Object.GetAllAsync()).Count;

            // Assert
            countAfter.Should().Be(countBefore + 1);
        }
    }
}