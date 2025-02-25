using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleBookingSystemBE.Domain.Entities;

namespace SimpleBookingSystemBE.Persistence.Configurations
{
    public class BookingConfiguration : BaseConfiguration<Booking>
    {
        public override void Configure(EntityTypeBuilder<Booking> builder)
        {
            base.Configure(builder);

            builder.HasKey(b => b.Id);
            builder.Property(b => b.BookedQuantity).IsRequired();
            builder.Property(b => b.DateFrom).IsRequired();
            builder.Property(b => b.DateTo).IsRequired();

            builder.HasOne(b => b.Resource)
                   .WithMany(r => r.Bookings)
                   .HasForeignKey(b => b.ResourceId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
