using MediatR;
using Microsoft.AspNetCore.Mvc;
using SimpleBookingSystemBE.Application.Features.Slice.Bookings.CreateBooking.Commands;
using SimpleBookingSystemBE.Application.Features.Slice.Bookings.GetBooking.Queries;

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

        [HttpPost("CreateBooking")]
        public async Task<IActionResult> CreateBooking(CreateBookingCommand createBookingCommand)
        {
            await _mediator.Send(createBookingCommand);
            return Ok("Booking created successfully.");
        }
    }
}
