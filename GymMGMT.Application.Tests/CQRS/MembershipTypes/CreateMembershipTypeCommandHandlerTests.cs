using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.CQRS;
using GymMGMT.Application.CQRS.MembershipTypes.Commands.CreateMembershipType;
using GymMGMT.Application.Tests.Mocks;

namespace GymMGMT.Application.Tests.CQRS.MembershipTypes
{
    public class CreateMembershipTypeCommandHandlerTests
    {
        private IMapper _mapper;
        private Mock<IMembershipTypeRepository> _membershipTypeRepositoryMock;

        public CreateMembershipTypeCommandHandlerTests()
        {
            var confProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = confProvider.CreateMapper();
            _membershipTypeRepositoryMock = MembershipTypeRepositoryMock.GetMembershipTypeRepository();
        }

        [Fact()]
        public async Task Handle_ForValidCommand_ReturnSuccessResponse()
        {
            // Arrange
            var handler = new CreateMembershipTypeCommandHandler(_membershipTypeRepositoryMock.Object, _mapper);
            var command = new CreateMembershipTypeCommand()
            {
                Name = "MTName",
                DefaultPrice = 12.45,
                DurationInDays = 30
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            response.Success.Should().BeTrue();
        }

        [Fact()]
        public async Task Handle_ForValidCommand_ReturnOneMoreMembershipTypes()
        {
            // Arrange
            var handler = new CreateMembershipTypeCommandHandler(_membershipTypeRepositoryMock.Object, _mapper);
            var countBefore = (await _membershipTypeRepositoryMock.Object.GetAllAsync()).Count;
            var command = new CreateMembershipTypeCommand()
            {
                Name = "MTName",
                DefaultPrice = 12.45,
                DurationInDays = 30
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);
            var countAfter = (await _membershipTypeRepositoryMock.Object.GetAllAsync()).Count;

            // Assert
            countAfter.Should().Be(countBefore + 1);
        }
    }
}
