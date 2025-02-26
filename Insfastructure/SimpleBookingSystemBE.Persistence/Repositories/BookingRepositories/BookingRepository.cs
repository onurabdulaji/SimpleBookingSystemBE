using Microsoft.EntityFrameworkCore;
using SimpleBookingSystemBE.Application.Interfaces.BookingInterface;
using SimpleBookingSystemBE.Domain.Entities;
using SimpleBookingSystemBE.Persistence.Context;

namespace SimpleBookingSystemBE.Persistence.Repositories.BookingRepositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly AppDbContext _appDbContext;

        public BookingRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<Booking>> GetExistingBookingsAsync(int resourceId, DateTime dateFrom, DateTime dateTo)
        {
            return await _appDbContext.Bookings
                .Where(b => b.ResourceId == resourceId
                            && b.DateFrom < dateTo
                            && b.DateTo > dateFrom)
                .ToListAsync();
        }
    }
}
