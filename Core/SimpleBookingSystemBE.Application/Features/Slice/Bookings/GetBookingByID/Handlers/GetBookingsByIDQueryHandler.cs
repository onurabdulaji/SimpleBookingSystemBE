using MediatR;
using SimpleBookingSystemBE.Application.Features.Slice.Bookings.GetBookingByID.Queries;
using SimpleBookingSystemBE.Application.Features.Slice.Bookings.GetBookingByID.Results;
using SimpleBookingSystemBE.Application.Interfaces;
using SimpleBookingSystemBE.Domain.Entities;

namespace SimpleBookingSystemBE.Application.Features.Slice.Bookings.GetBookingByID.Handlers
{
    public class GetBookingsByIDQueryHandler : IRequestHandler<GetBookingsByIDQuery, ICollection<GetBookingsByIDQueryResult>>
    {
        private readonly IRepository<Booking> _repository;

        public GetBookingsByIDQueryHandler(IRepository<Booking> repository)
        {
            _repository = repository;
        }

        public async Task<ICollection<GetBookingsByIDQueryResult>> Handle(GetBookingsByIDQuery request, CancellationToken cancellationToken)
        {
            var booking = await _repository.GetByIdAsync(request.Id);
            var result = new List<GetBookingsByIDQueryResult>();
            if (booking != null)
            {
                result.Add(new GetBookingsByIDQueryResult
                {
                    Id = booking.Id,
                    ResourceId = booking.ResourceId,
                    DateFrom = booking.DateFrom,
                    DateTo = booking.DateTo,
                    BookedQuantity = booking.BookedQuantity
                });
            }
            return result;
        }
    }
}
