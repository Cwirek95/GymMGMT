using GymMGMT.Domain.Entities;
using GymMGMT.Persistence.EF;

namespace GymMGMT.Api.Tests.Fakes
{
    public static class FakeDataSeed
    {
        public static void SeedUser(User user, ApiTestsServices services)
        {
            var scopeFactory = services.Services.GetService<IServiceScopeFactory>();
            using var scope = scopeFactory.CreateScope();
            var _dbContext = scope.ServiceProvider.GetService<AppDbContext>();

            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }

        public static void RemoveUser(User user, ApiTestsServices services)
        {
            var scopeFactory = services.Services.GetService<IServiceScopeFactory>();
            using var scope = scopeFactory.CreateScope();
            var _dbContext = scope.ServiceProvider.GetService<AppDbContext>();

            _dbContext.Users.Remove(user);
            _dbContext.SaveChanges();
        }

        public static void SeedMembershipType(MembershipType membershipType, ApiTestsServices services)
        {
            var scopeFactory = services.Services.GetService<IServiceScopeFactory>();
            using var scope = scopeFactory.CreateScope();
            var _dbContext = scope.ServiceProvider.GetService<AppDbContext>();

            _dbContext.MembershipTypes.Add(membershipType);
            _dbContext.SaveChanges();
        }

        public static void RemoveMembershipType(MembershipType membershipType, ApiTestsServices services)
        {
            var scopeFactory = services.Services.GetService<IServiceScopeFactory>();
            using var scope = scopeFactory.CreateScope();
            var _dbContext = scope.ServiceProvider.GetService<AppDbContext>();

            _dbContext.MembershipTypes.Remove(membershipType);
            _dbContext.SaveChanges();
        }

        public static void SeedRole(Role role, ApiTestsServices services)
        {
            var scopeFactory = services.Services.GetService<IServiceScopeFactory>();
            using var scope = scopeFactory.CreateScope();
            var _dbContext = scope.ServiceProvider.GetService<AppDbContext>();

            _dbContext.Roles.Add(role);
            _dbContext.SaveChanges();
        }

        public static void RemoveRole(Role role, ApiTestsServices services)
        {
            var scopeFactory = services.Services.GetService<IServiceScopeFactory>();
            using var scope = scopeFactory.CreateScope();
            var _dbContext = scope.ServiceProvider.GetService<AppDbContext>();

            _dbContext.Roles.Remove(role);
            _dbContext.SaveChanges();
        }

        public static void SeedMember(Member member, ApiTestsServices services)
        {
            var scopeFactory = services.Services.GetService<IServiceScopeFactory>();
            using var scope = scopeFactory.CreateScope();
            var _dbContext = scope.ServiceProvider.GetService<AppDbContext>();

            _dbContext.Members.Add(member);
            _dbContext.SaveChanges();
        }

        public static void RemoveMember(Member member, ApiTestsServices services)
        {
            var scopeFactory = services.Services.GetService<IServiceScopeFactory>();
            using var scope = scopeFactory.CreateScope();
            var _dbContext = scope.ServiceProvider.GetService<AppDbContext>();

            _dbContext.Members.Remove(member);
            _dbContext.SaveChanges();
        }

        public static void SeedTraining(Training training, ApiTestsServices services)
        {
            var scopeFactory = services.Services.GetService<IServiceScopeFactory>();
            using var scope = scopeFactory.CreateScope();
            var _dbContext = scope.ServiceProvider.GetService<AppDbContext>();

            _dbContext.Trainings.Add(training);
            _dbContext.SaveChanges();
        }

        public static void RemoveTraining(Training training, ApiTestsServices services)
        {
            var scopeFactory = services.Services.GetService<IServiceScopeFactory>();
            using var scope = scopeFactory.CreateScope();
            var _dbContext = scope.ServiceProvider.GetService<AppDbContext>();

            _dbContext.Trainings.Remove(training);
            _dbContext.SaveChanges();
        }

        public static void SeedMembership(Membership membership, ApiTestsServices services)
        {
            var scopeFactory = services.Services.GetService<IServiceScopeFactory>();
            using var scope = scopeFactory.CreateScope();
            var _dbContext = scope.ServiceProvider.GetService<AppDbContext>();

            _dbContext.Memberships.Add(membership);
            _dbContext.SaveChanges();
        }

        public static void RemoveMembership(Membership membership, ApiTestsServices services)
        {
            var scopeFactory = services.Services.GetService<IServiceScopeFactory>();
            using var scope = scopeFactory.CreateScope();
            var _dbContext = scope.ServiceProvider.GetService<AppDbContext>();

            _dbContext.Memberships.Remove(membership);
            _dbContext.SaveChanges();
        }

        public static void SeedTrainer(Trainer trainer, ApiTestsServices services)
        {
            var scopeFactory = services.Services.GetService<IServiceScopeFactory>();
            using var scope = scopeFactory.CreateScope();
            var _dbContext = scope.ServiceProvider.GetService<AppDbContext>();

            _dbContext.Trainers.Add(trainer);
            _dbContext.SaveChanges();
        }

        public static void RemoveTrainer(Trainer trainer, ApiTestsServices services)
        {
            var scopeFactory = services.Services.GetService<IServiceScopeFactory>();
            using var scope = scopeFactory.CreateScope();
            var _dbContext = scope.ServiceProvider.GetService<AppDbContext>();

            _dbContext.Trainers.Remove(trainer);
            _dbContext.SaveChanges();
        }
    }
}