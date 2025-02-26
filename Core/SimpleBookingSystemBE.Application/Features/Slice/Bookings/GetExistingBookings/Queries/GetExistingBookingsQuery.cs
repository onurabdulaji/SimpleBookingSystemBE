using MediatR;
using SimpleBookingSystemBE.Application.Features.Slice.Bookings.GetExistingBookings.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBookingSystemBE.Application.Features.Slice.Bookings.GetExistingBookings.Queries
{
    public class GetExistingBookingsQuery : IRequest<ICollection<GetExistingBookingsResults>>
    {
        public int Id { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public GetExistingBookingsQuery(int id, DateTime dateFrom, DateTime dateTo)
        {
            Id = id;
            DateFrom = dateFrom;
            DateTo = dateTo;
        }
    }
}
