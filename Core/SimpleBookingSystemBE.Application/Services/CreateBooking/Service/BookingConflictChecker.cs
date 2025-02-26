using SimpleBookingSystemBE.Application.Interfaces.ResourceInterface;
using SimpleBookingSystemBE.Application.Services.CreateBooking.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBookingSystemBE.Application.Services.CreateBooking.Service
{
    public class BookingConflictChecker : IBookingConflictChecker
    {
        private readonly IResourceRepository _resourceRepository;

        public BookingConflictChecker(IResourceRepository resourceRepository)
        {
            _resourceRepository = resourceRepository;
        }

        public async Task<BookingConflictResult> IsBookingConflictAsync(int resourceId, DateTime startTime, DateTime endTime, int requestedQuantity)
        {
            var existingBookings = await _resourceRepository.GetBookingsForResourceAsync(resourceId, startTime.Date, endTime.Date);

            foreach (var booking in existingBookings)
            {
                if (booking.DateFrom.Date == startTime.Date &&
                    booking.DateFrom < endTime &&
                    startTime < booking.DateTo)
                {
                    return new BookingConflictResult
                    {
                        IsConflict = true,
                        OverCapacityAmount = requestedQuantity
                    };
                }
            }
            int totalAvailableQuantity = await _resourceRepository.GetTotalAvailableQuantity(resourceId);
            int totalBookedQuantity = existingBookings.Sum(b => b.BookedQuantity);

            bool isCapacityExceeded = totalBookedQuantity + requestedQuantity > totalAvailableQuantity;

            return new BookingConflictResult
            {
                IsConflict = isCapacityExceeded,
                OverCapacityAmount = isCapacityExceeded ? (totalBookedQuantity + requestedQuantity - totalAvailableQuantity) : 0
            };
        }

    }
}

public class BookingConflictResult
{
    public bool IsConflict { get; set; }
    public int OverCapacityAmount { get; set; }
}

