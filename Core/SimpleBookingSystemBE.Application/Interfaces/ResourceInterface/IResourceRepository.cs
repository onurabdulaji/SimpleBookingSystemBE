using SimpleBookingSystemBE.Domain.Entities;

namespace SimpleBookingSystemBE.Application.Interfaces.ResourceInterface
{
    public interface IResourceRepository
    {
        Task<ICollection<Booking>> GetBookingsForResourceAsync(int resourceId, DateTime dateFrom, DateTime dateTo);
        int GetTotalAvailableQuantity(int resourceId);
    }
}
