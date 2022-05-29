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

            return services;
        }
    }
}
