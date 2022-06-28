using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.CQRS;
using GymMGMT.Application.CQRS.Trainers.Commands.AddTrainer;
using GymMGMT.Application.Tests.Mocks;

namespace GymMGMT.Application.Tests.CQRS.Trainers
{
    public class AddTrainerCommandHandlerTests
    {
        private IMapper _mapper;
        private Mock<ITrainerRepository> _trainerRepositoryMock;
        private Mock<IUserRepository> _userRepositoryMock;

        public AddTrainerCommandHandlerTests()
        {
            var confProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = confProvider.CreateMapper();
            _trainerRepositoryMock = TrainerRepositoryMock.GetTrainerRepository();
            _userRepositoryMock = UserRepositoryServiceMock.GetUserRepository();
        }

        [Fact()]
        public async Task Handle_ForValidCommand_ReturnSuccessResponse()
        {
            // Arrange
            var handler = new AddTrainerCommandHandler(_trainerRepositoryMock.Object, _mapper, _userRepositoryMock.Object);
            var command = new AddTrainerCommand()
            {
                FirstName = "Fname",
                LastName = "LName",
                UserId = Guid.NewGuid(),
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            response.Success.Should().BeTrue();
        }

        [Fact()]
        public async Task Handle_ForValidCommand_ReturnOneMoreTrainers()
        {
            // Arrange
            var handler = new AddTrainerCommandHandler(_trainerRepositoryMock.Object, _mapper, _userRepositoryMock.Object);
            var countBefore = (await _trainerRepositoryMock.Object.GetAllAsync()).Count;
            var command = new AddTrainerCommand()
            {
                FirstName = "Fname",
                LastName = "LName",
                UserId = Guid.NewGuid()
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);
            var countAfter = (await _trainerRepositoryMock.Object.GetAllAsync()).Count;

            // Assert
            countAfter.Should().Be(countBefore + 1);
        }
    }
}