using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.CQRS.Auth.Commands.CreateRole;
using GymMGMT.Application.CQRS;
using GymMGMT.Application.Tests.Mocks;

namespace GymMGMT.Application.Tests.CQRS.Auth.Role
{
    public class CreateRoleCommandHandlerTests
    {
        private IMapper _mapper;
        private Mock<IRoleRepository> _roleRepositoryMock;

        public CreateRoleCommandHandlerTests()
        {
            var confProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>(); 
            });

            _mapper = confProvider.CreateMapper();
            _roleRepositoryMock = RoleRepositoryMock.GetRoleRepository();
        }

        [Fact()]
        public async Task Handle_ForValidCommand_ReturnSuccessResponse()
        {
            // Arrange
            var handler = new CreateRoleCommandHandler(_roleRepositoryMock.Object, _mapper);
            var command = new CreateRoleCommand()
            {
                Name = "RoleName"
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            response.Success.Should().BeTrue();
        }

        [Fact()]
        public async Task Handle_ForValidCommand_ReturnOneMoreRoles()
        {
            // Arrange
            var handler = new CreateRoleCommandHandler(_roleRepositoryMock.Object, _mapper);
            var countBefore = (await _roleRepositoryMock.Object.GetAllAsync()).Count;
            var command = new CreateRoleCommand()
            {
                Name = "RoleName"
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);
            var countAfter = (await _roleRepositoryMock.Object.GetAllAsync()).Count;

            // Assert
            countAfter.Should().Be(countBefore + 1);
        }
    }
}
