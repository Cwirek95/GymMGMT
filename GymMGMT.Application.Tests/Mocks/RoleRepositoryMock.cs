using AutoFixture;
using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Domain.Entities;
using Moq;

namespace GymMGMT.Application.Tests.Mocks
{
    public class RoleRepositoryMock
    {
        public static Mock<IRoleRepository> GetRoleRepository()
        {
            var roles = GetRoles();
            var mockRoleRepository = new Mock<IRoleRepository>();

            mockRoleRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(roles);
            mockRoleRepository.Setup(x => x.GetAllWithDetailsAsync()).ReturnsAsync(roles);
            mockRoleRepository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(
                (Guid id) =>
                {
                    var role = roles.FirstOrDefault(x => x.Id == id);
                    return role;
                });
            mockRoleRepository.Setup(x => x.AddAsync(It.IsAny<Role>())).ReturnsAsync(
                (Role role) =>
                {
                    roles.Add(role);
                    return role;
                });
            mockRoleRepository.Setup(x => x.UpdateAsync(It.IsAny<Role>())).Callback<Role>(
                (Role role) =>
                {
                    var existRole = roles.FirstOrDefault(x => x.Id == role.Id);
                    existRole.Id = role.Id;
                    existRole.Name = role.Name;
                    existRole.Status = role.Status;
                });
            mockRoleRepository.Setup(x => x.DeleteAsync(It.IsAny<Role>())).Callback<Role>(
                (role) =>
                {
                    roles.Remove(role);
                });

            return mockRoleRepository;
        }

        private static List<Role> GetRoles()
        {
            Fixture fixture = new Fixture();
            var roles = fixture.Build<Role>().Without(x => x.Users).CreateMany(10).ToList();

            return roles;
        }
    }
}
