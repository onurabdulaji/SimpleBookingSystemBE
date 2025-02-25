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
                        Quantity = 999
                    },
                    new Resource
                    {
                        Name = "Resource 2",
                        Quantity = 999
                    },
                    new Resource
                    {
                        Name = "Resource 3",
                        Quantity = 999
                    }
                );
                context.SaveChanges();
            }

            if (!context.Bookings.Any())
            {
                context.Bookings.AddRange(
                    new Booking
                    {
                        ResourceId = 1,
                        BookedQuantity = 1,
                        DateFrom = DateTime.Now.AddDays(1),
                        DateTo = DateTime.Now.AddDays(2)
                    }, new Booking
                    {
                        ResourceId = 2,
                        BookedQuantity = 1,
                        DateFrom = DateTime.Now.AddDays(1),
                        DateTo = DateTime.Now.AddDays(2)
                    }, new Booking
                    {
                        ResourceId = 3,
                        BookedQuantity = 1,
                        DateFrom = DateTime.Now.AddDays(1),
                        DateTo = DateTime.Now.AddDays(2)
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
