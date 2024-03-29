﻿using GymMGMT.Api.Services;
using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.CQRS.Trainings.Commands.ChangeTrainingDate;
using GymMGMT.Application.Security.Contracts;
using GymMGMT.Application.Tests.Mocks;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace GymMGMT.Application.Tests.CQRS.Trainings
{
    public class ChangeTrainingDateCommandHandlerTests
    {
        private Mock<ITrainingRepository> _trainingRepositoryMock;
        private ICurrentUserService _currentUserService;

        public ChangeTrainingDateCommandHandlerTests()
        {
            var claimsPrincipal = new ClaimsPrincipal();
            claimsPrincipal.AddIdentity(new ClaimsIdentity(
                new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Email, "admin@email.com"),
                    new Claim(ClaimTypes.Role, "Admin")
                }));

            var _httpContextAccessor = new Mock<IHttpContextAccessor>();
            _httpContextAccessor.Setup(x => x.HttpContext.User).Returns(claimsPrincipal);
            _currentUserService = new CurrentUserService(_httpContextAccessor.Object);
            _trainingRepositoryMock = TrainingRepositoryMock.GetTrainingRepository();
        }

        [Fact()]
        public async Task Handle_ForValidCommand_ReturnSuccessResponse()
        {
            // Arrange
            var items = await _trainingRepositoryMock.Object.GetAllAsync();
            var handler = new ChangeTrainingDateCommandHandler(_trainingRepositoryMock.Object, _currentUserService);
            var command = new ChangeTrainingDateCommand()
            {
                Id = items.First().Id,
                StartDate = DateTimeOffset.Now.AddDays(-5),
                EndDate = DateTimeOffset.Now.AddDays(-3),
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            response.Success.Should().BeTrue();
        }

        [Fact()]
        public async Task Handle_StartDateChanged_ReturnNewStartDate()
        {
            // Arrange
            var items = await _trainingRepositoryMock.Object.GetAllAsync();
            var handler = new ChangeTrainingDateCommandHandler(_trainingRepositoryMock.Object, _currentUserService);
            var startDateBefore = (await _trainingRepositoryMock.Object.GetByIdAsync(items.Last().Id)).StartDate;
            var command = new ChangeTrainingDateCommand()
            {
                Id = items.Last().Id,
                StartDate = DateTimeOffset.Now.AddDays(-2),
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);
            var startDateAfter = (await _trainingRepositoryMock.Object.GetByIdAsync(items.Last().Id)).StartDate;

            // Assert
            startDateAfter.Should().NotBe(startDateBefore);
        }

        [Fact()]
        public async Task Handle_EndDateChanged_ReturnNewStartDate()
        {
            // Arrange
            var items = await _trainingRepositoryMock.Object.GetAllAsync();
            var handler = new ChangeTrainingDateCommandHandler(_trainingRepositoryMock.Object, _currentUserService);
            var startDateBefore = (await _trainingRepositoryMock.Object.GetByIdAsync(items.Last().Id)).EndDate;
            var command = new ChangeTrainingDateCommand()
            {
                Id = items.Last().Id,
                EndDate = DateTimeOffset.Now.AddDays(-1)
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);
            var startDateAfter = (await _trainingRepositoryMock.Object.GetByIdAsync(items.Last().Id)).EndDate;

            // Assert
            startDateAfter.Should().NotBe(startDateBefore);
        }
    }
}