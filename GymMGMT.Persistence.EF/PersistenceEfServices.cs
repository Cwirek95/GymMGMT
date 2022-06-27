using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Persistence.EF.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GymMGMT.Persistence.EF
{
    public static class PersistenceEfServices
    {
        public static IServiceCollection AddPersistenceEfServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("GymMGMTTestDbConnectionString")));

            services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));

            // Repositories DI
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IMemberRepository, MemberRepository>();
            services.AddScoped<IMembershipRepository, MembershipRepository>();
            services.AddScoped<IMembershipTypeRepository, MembershipTypeRepository>();
            services.AddScoped<ITrainerRepository, TrainerRepository>();
            services.AddScoped<ITrainingRepository, TrainingRepository>();

            return services;
        }
    }
}
