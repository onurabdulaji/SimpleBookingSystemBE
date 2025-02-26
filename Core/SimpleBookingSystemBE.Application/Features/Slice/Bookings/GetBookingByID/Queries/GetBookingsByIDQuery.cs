using MediatR;
using SimpleBookingSystemBE.Application.Features.Slice.Bookings.GetBookingByID.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBookingSystemBE.Application.Features.Slice.Bookings.GetBookingByID.Queries
{
    public class GetBookingsByIDQuery : IRequest<ICollection<GetBookingsByIDQueryResult>>
    {
        public int Id { get; set; }

        public GetBookingsByIDQuery(int id)
        {
            Id = id;
        }
    }
}
