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
                (role) =>
                {
                    roles.RemoveAll(x => x.Id == role.Id);
                    roles.Add(role);
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
            var roles = new List<Role>();

            var role1 = new Role()
            {
                Id = Guid.NewGuid(),
                Name = "RoleName1",
                Status = true
            };

            var role2 = new Role()
            {
                Id = Guid.NewGuid(),
                Name = "RoleName2",
                Status = true
            };

            var role3 = new Role()
            {
                Id = Guid.NewGuid(),
                Name = "RoleName3",
                Status = true
            };

            Fixture fixture = new Fixture();
            var roles2 = fixture.Build<Role>().Without(x => x.Users).CreateMany().ToList();

            return roles2;
        }
    }
}
