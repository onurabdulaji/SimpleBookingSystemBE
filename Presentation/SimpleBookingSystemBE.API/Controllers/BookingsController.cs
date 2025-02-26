using MediatR;
using Microsoft.AspNetCore.Mvc;
using SimpleBookingSystemBE.Application.Features.Slice.Bookings.CreateBooking.Commands;
using SimpleBookingSystemBE.Application.Features.Slice.Bookings.GetBooking.Queries;
using SimpleBookingSystemBE.Application.Features.Slice.Bookings.GetBookingByID.Queries;
using SimpleBookingSystemBE.Application.Features.Slice.Bookings.GetExistingBookings.Queries;

namespace SimpleBookingSystemBE.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public BookingsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetBookings()
        {
            var values = await _mediator.Send(new GetBookingsQuery());
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookingsByID(int id)
        {
            var values = await _mediator.Send(new GetBookingsByIDQuery(id));
            return Ok(values);
        }
        [HttpGet("existing")]
        public async Task<IActionResult> GetExistingBookings(int resourceId, DateTime dateFrom, DateTime dateTo)
        {
            var query = new GetExistingBookingsQuery(resourceId, dateFrom, dateTo);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost("CreateBooking")]
        public async Task<IActionResult> CreateBooking(CreateBookingCommand createBookingCommand)
        {
            await _mediator.Send(createBookingCommand);
            return Ok("Booking created successfully.");
        }
    }
}
