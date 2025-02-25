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
            try
            {
                // Fetch existing bookings within the requested time frame
                var existingBookings = await _resourceRepository.GetBookingsForResourceAsync(resourceId, startTime, endTime);

                // Calculate the total booked quantity for the requested period
                int totalBookedQuantity = existingBookings.Sum(b => b.BookedQuantity);

                // Fetch the total available quantity for the resource
                int totalAvailableQuantity = _resourceRepository.GetTotalAvailableQuantity(resourceId);

                // Determine if the new booking would exceed the available quantity
                bool isConflict = totalBookedQuantity + requestedQuantity > totalAvailableQuantity;

                return new BookingConflictResult
                {
                    IsConflict = isConflict,
                    OverCapacityAmount = isConflict ? (totalBookedQuantity + requestedQuantity - totalAvailableQuantity) : 0
                };
            }
            catch (Exception ex)
            {
                // Handle exceptions, possibly logging them and rethrowing or returning an error result
                throw new BookingConflictCheckerException("An error occurred while checking for booking conflicts.", ex);
            }
        }
    }
}

public class BookingConflictResult
{
    public bool IsConflict { get; set; }
    public int OverCapacityAmount { get; set; }
}

public class BookingConflictCheckerException : Exception
{
    public BookingConflictCheckerException(string message, Exception innerException) : base(message, innerException) { }
}