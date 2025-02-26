using MediatR;
using SimpleBookingSystemBE.Application.Features.Slice.Bookings.GetExistingBookings.Queries;
using SimpleBookingSystemBE.Application.Features.Slice.Bookings.GetExistingBookings.Results;
using SimpleBookingSystemBE.Application.Interfaces.BookingInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBookingSystemBE.Application.Features.Slice.Bookings.GetExistingBookings.Handlers
{
    public class GetExistingBookingsQueryHandler : IRequestHandler<GetExistingBookingsQuery, ICollection<GetExistingBookingsResults>>
    {
        private readonly IBookingRepository _bookingRepository;

        public GetExistingBookingsQueryHandler(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<ICollection<GetExistingBookingsResults>> Handle(GetExistingBookingsQuery request, CancellationToken cancellationToken)
        {
            var resourceId = request.Id;
            var dateFrom = request.DateFrom;
            var dateTo = request.DateTo;

            var existingBookings = await _bookingRepository.GetExistingBookingsAsync(resourceId, dateFrom, dateTo);

            var result = existingBookings.Select(booking => new GetExistingBookingsResults
            {
                Id = booking.Id,
                ResourceId = booking.ResourceId,
                DateFrom = booking.DateFrom,
                DateTo = booking.DateTo,
                BookedQuantity = booking.BookedQuantity
            }).ToList();

            return result;
        }
    }
}
