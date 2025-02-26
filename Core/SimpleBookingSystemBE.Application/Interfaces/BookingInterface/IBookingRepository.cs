using SimpleBookingSystemBE.Domain.Entities;

namespace SimpleBookingSystemBE.Application.Interfaces.BookingInterface
{
    public interface IBookingRepository
    {
        Task<List<Booking>> GetExistingBookingsAsync(int resourceId, DateTime dateFrom, DateTime dateTo);

    }
}
