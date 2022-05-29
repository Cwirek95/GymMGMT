using GymMGMT.Persistence.EF;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;

namespace GymMGMT.Api.Tests
{
    public class ApiTestsServices : WebApplicationFactory<Program>
    {
        protected override IHost CreateHost(IHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var dbContextOptions = services.SingleOrDefault(service =>
                    service.ServiceType == typeof(DbContextOptions<AppDbContext>));
                services.Remove(dbContextOptions);
                services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("AppInMemoryDb"));
            });

            return base.CreateHost(builder);
        }
    }
}
