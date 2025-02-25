using MediatR;
using SimpleBookingSystemBE.Application.Features.Slice.Bookings.GetBooking.Queries;
using SimpleBookingSystemBE.Application.Features.Slice.Bookings.GetBooking.Results;
using SimpleBookingSystemBE.Application.Interfaces;
using SimpleBookingSystemBE.Domain.Entities;

namespace SimpleBookingSystemBE.Application.Features.Slice.Bookings.GetBooking.Handlers
{
    public class GetBookingsQueryHandler : IRequestHandler<GetBookingsQuery, ICollection<GetBookingsQueryResult>>
    {
        private readonly IRepository<Booking> _repository;

        public GetBookingsQueryHandler(IRepository<Booking> repository)
        {
            _repository = repository;
        }

        public async Task<ICollection<GetBookingsQueryResult>> Handle(GetBookingsQuery request, CancellationToken cancellationToken)
        {
            var values = await _repository.GetAllAsync();
            return values.Select(x => new GetBookingsQueryResult
            {
                Id = x.Id,
                ResourceId = x.ResourceId,
                DateFrom = x.DateFrom,
                DateTo = x.DateTo,
                BookedQuantity = x.BookedQuantity,
            }).ToList();
        }
    }
}
