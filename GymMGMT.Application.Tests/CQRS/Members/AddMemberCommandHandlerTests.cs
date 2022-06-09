using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.CQRS;
using GymMGMT.Application.CQRS.Members.Commands.AddMember;
using GymMGMT.Application.Tests.Mocks;
using MediatR;

namespace GymMGMT.Application.Tests.CQRS.Members
{
    public class AddMemberCommandHandlerTests
    {
        private IMapper _mapper;
        private Mock<IMediator> _mediator;
        private Mock<IMemberRepository> _memberRepositoryMock;
        private Mock<IMembershipTypeRepository> _membershipTypeRepositoryMock;
        private Mock<IUserRepository> _userRepositoryMock;

        public AddMemberCommandHandlerTests()
        {
            var confProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = confProvider.CreateMapper();
            _memberRepositoryMock = MemberRepositoryMock.GetMemberRepository();
            _membershipTypeRepositoryMock = MembershipTypeRepositoryMock.GetMembershipTypeRepository();
            _userRepositoryMock = UserRepositoryServiceMock.GetUserRepository();
            _mediator = new Mock<IMediator>();
        }

        [Fact()]
        public async Task Handle_ForValidCommand_ReturnSuccessResponse()
        {
            // Arrange
            var handler = new AddMemberCommandHandler(_memberRepositoryMock.Object, _mapper,
                _mediator.Object, _membershipTypeRepositoryMock.Object, _userRepositoryMock.Object);
            var membershipTypes = await _membershipTypeRepositoryMock.Object.GetAllAsync();
            var command = new AddMemberCommand()
            {
                FirstName = "Fname",
                LastName = "LName",
                DateOfBirth = DateTimeOffset.Now.AddYears(-20),
                PhoneNumber = "+48123456789",
                Price = 99,
                MembershipTypeId = membershipTypes.First().Id,
                UserId = Guid.NewGuid(),
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            response.Success.Should().BeTrue();
        }

        [Fact()]
        public async Task Handle_ForValidCommand_ReturnOneMoreMembers()
        {
            // Arrange
            var handler = new AddMemberCommandHandler(_memberRepositoryMock.Object, _mapper,
                _mediator.Object, _membershipTypeRepositoryMock.Object, _userRepositoryMock.Object);
            var countBefore = (await _memberRepositoryMock.Object.GetAllAsync()).Count;
            var membershipTypes = await _membershipTypeRepositoryMock.Object.GetAllAsync();
            var command = new AddMemberCommand()
            {
                FirstName = "Fname",
                LastName = "LName",
                DateOfBirth = DateTimeOffset.Now.AddYears(-20),
                PhoneNumber = "+48123456789",
                Price = 50,
                MembershipTypeId = membershipTypes.First().Id,
                UserId = Guid.NewGuid()
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);
            var countAfter = (await _memberRepositoryMock.Object.GetAllAsync()).Count;

            // Assert
            countAfter.Should().Be(countBefore + 1);
        }
    }
}