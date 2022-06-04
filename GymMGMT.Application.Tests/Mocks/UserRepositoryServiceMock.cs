using AutoFixture;
using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.Security.Contracts;
using GymMGMT.Domain.Entities;

namespace GymMGMT.Application.Tests.Mocks
{
    public class UserRepositoryServiceMock
    {
        private static List<User> users = GetUsers();

        public static Mock<IUserRepository> GetUserRepository()
        {
            var mockUserRepository = new Mock<IUserRepository>();

            mockUserRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(users);
            mockUserRepository.Setup(x => x.GetAllWithDetailsAsync()).ReturnsAsync(users);
            mockUserRepository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(
                (Guid id) =>
                {
                    var user = users.FirstOrDefault(x => x.Id == id);
                    return user;
                });
            mockUserRepository.Setup(x => x.GetByIdWithDetailsAsync(It.IsAny<Guid>())).ReturnsAsync(
                (Guid id) =>
                {
                    var user = users.FirstOrDefault(x => x.Id == id);
                    return user;
                });
            mockUserRepository.Setup(x => x.AddAsync(It.IsAny<User>())).ReturnsAsync(
                (User user) =>
                {
                    users.Add(user);
                    return user;
                });
            mockUserRepository.Setup(x => x.UpdateAsync(It.IsAny<User>())).Callback<User>(
                (User user) =>
                {
                    var existUser = users.FirstOrDefault(x => x.Id == user.Id);
                    existUser.Id = user.Id;
                    existUser.Email = user.Email;
                    existUser.Password = user.Password;
                    existUser.Status = user.Status;
                    existUser.RegisteredAt = user.RegisteredAt;
                    existUser.RoleId = user.RoleId;
                });
            mockUserRepository.Setup(x => x.DeleteAsync(It.IsAny<User>())).Callback<User>(
                (User user) =>
                {
                    users.Remove(user);
                });

            return mockUserRepository;
        }

        public static Mock<IAuthenticationService> GetAuthService()
        {
            var mockAuthService = new Mock<IAuthenticationService>();

            mockAuthService.Setup(x => x.CreateUserAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(
                (string email, string password) =>
                {
                    var user = new User()
                    {
                        Id = Guid.NewGuid(),
                        Email = email,
                        Password = password,
                        RegisteredAt = DateTimeOffset.Now,
                        RoleId = Guid.Empty,
                        Status = true
                    };
                    users.Add(user);

                    return user;
                });
            mockAuthService.Setup(x => x.ChangeUserPasswordAsync(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<string>())).Callback(
                (Guid userId, string oldPassword, string newPassword) =>
                {
                    var user = users.FirstOrDefault(x => x.Id == userId);
                    user.Password = newPassword;
                });
            mockAuthService.Setup(x => x.ChangeUserRoleAsync(It.IsAny<Guid>(), It.IsAny<Guid>())).Callback(
                (Guid userId, Guid roleId) =>
                {
                    var user = users.FirstOrDefault(x => x.Id == userId);
                    user.RoleId = roleId;
                });
            mockAuthService.Setup(x => x.DeleteUserAsync(It.IsAny<Guid>())).Callback(
                (Guid userId) =>
                {
                    var user = users.FirstOrDefault(x => x.Id == userId);
                    users.Remove(user);
                });

            return mockAuthService;
        }

        private static List<User> GetUsers()
        {
            Fixture fixture = new Fixture();
            var users = fixture.Build<User>().Without(x => x.Role).CreateMany(10).ToList();

            return users;
        }
    }
}
