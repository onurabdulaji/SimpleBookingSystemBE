using MediatR;
using SimpleBookingSystemBE.Application.Features.Slice.Bookings.GetBooking.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBookingSystemBE.Application.Features.Slice.Bookings.GetBooking.Queries
{
    public class GetBookingsQuery : IRequest<ICollection<GetBookingsQueryResult>>
    {
    }
}
