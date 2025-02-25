using SimpleBookingSystemBE.Application.Interfaces.ResourceInterface;
using SimpleBookingSystemBE.Application.Services.CreateBooking.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBookingSystemBE.Application.Services.CreateBooking.Service
{
    public class BookingService : IBookingService
    {
        private readonly IResourceRepository _resourceRepository;

        public BookingService(IResourceRepository resourceRepository)
        {
            _resourceRepository = resourceRepository;
        }

        public async Task<bool> CanBookResourceAsync(int resourceId, DateTime dateFrom, DateTime dateTo, int requestedQuantity)
        {
            var existingBookings = await _resourceRepository.GetBookingsForResourceAsync(resourceId, dateFrom, dateTo);
            for (DateTime date = dateFrom; date <= dateTo; date = date.AddDays(1))
            {
                int totalBookedQuantityForDay = existingBookings
                    .Where(b => b.DateFrom <= date && b.DateTo >= date)
                    .Sum(b => b.BookedQuantity);

                int totalAvailableQuantity = _resourceRepository.GetTotalAvailableQuantity(resourceId);

                if (totalBookedQuantityForDay + requestedQuantity > totalAvailableQuantity)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
