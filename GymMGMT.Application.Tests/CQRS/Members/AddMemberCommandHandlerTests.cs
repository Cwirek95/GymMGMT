using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.CQRS;
using GymMGMT.Application.CQRS.Members.Commands.AddMember;
using GymMGMT.Application.Tests.Mocks;

namespace GymMGMT.Application.Tests.CQRS.Members
{
    public class AddMemberCommandHandlerTests
    {
        private IMapper _mapper;
        private Mock<IMemberRepository> _memberRepositoryMock;

        public AddMemberCommandHandlerTests()
        {
            var confProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = confProvider.CreateMapper();
            _memberRepositoryMock = MemberRepositoryMock.GetMemberRepository();
        }

        [Fact()]
        public async Task Handle_ForValidCommand_ReturnSuccessResponse()
        {
            // Arrange
            var handler = new AddMemberCommandHandler(_memberRepositoryMock.Object, _mapper);
            var command = new AddMemberCommand()
            {
                FirstName = "Fname",
                LastName = "LName",
                DateOfBirth = DateTimeOffset.Now.AddYears(-20),
                PhoneNumber = "+48123456789",
                UserId = Guid.NewGuid(),
                MembershipId = new Random().Next(0, 100)
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
            var handler = new AddMemberCommandHandler(_memberRepositoryMock.Object, _mapper);
            var countBefore = (await _memberRepositoryMock.Object.GetAllAsync()).Count;
            var command = new AddMemberCommand()
            {
                FirstName = "Fname",
                LastName = "LName",
                DateOfBirth = DateTimeOffset.Now.AddYears(-20),
                PhoneNumber = "+48123456789",
                UserId = Guid.NewGuid(),
                MembershipId = new Random().Next(0, 100)
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);
            var countAfter = (await _memberRepositoryMock.Object.GetAllAsync()).Count;

            // Assert
            countAfter.Should().Be(countBefore + 1);
        }
    }
}