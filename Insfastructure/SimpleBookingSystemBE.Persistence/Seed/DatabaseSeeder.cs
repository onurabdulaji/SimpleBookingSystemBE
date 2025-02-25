using SimpleBookingSystemBE.Domain.Entities;
using SimpleBookingSystemBE.Persistence.Context;

namespace SimpleBookingSystemBE.Persistence.Seed
{
    public class DatabaseSeeder
    {
        public static void Seed(AppDbContext context)
        {
            context.Database.EnsureCreated();

            if (!context.Resources.Any())
            {
                context.Resources.AddRange(
                    new Resource
                    {
                        Name = "Resource 1",
                        Quantity = 1000
                    },
                    new Resource
                    {
                        Name = "Resource 2",
                        Quantity = 2000
                    },
                    new Resource
                    {
                        Name = "Resource 3",
                        Quantity = 3000
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
