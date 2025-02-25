using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleBookingSystemBE.Application.Interfaces;
using SimpleBookingSystemBE.Application.Interfaces.BookingInterface;
using SimpleBookingSystemBE.Application.Interfaces.ResourceInterface;
using SimpleBookingSystemBE.Persistence.Context;
using SimpleBookingSystemBE.Persistence.Repositories;
using SimpleBookingSystemBE.Persistence.Repositories.BookingRepositories;
using SimpleBookingSystemBE.Persistence.Repositories.ResourceRepositories;
using SimpleBookingSystemBE.Persistence.Seed;

namespace SimpleBookingSystemBE.Persistence
{
    public static class PersistenceExtensions
    {
        public static void AddPersistenceLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IResourceRepository, ResourceRepository>();
            services.AddScoped<IBookingRepository, BookingRepository>();

        }
        public static void UseDatabaseSeeder(this IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            DatabaseSeeder.Seed(context);
        }
    }
}
