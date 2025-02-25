using FluentValidation;
using SimpleBookingSystemBE.Application.Features.Slice.Bookings.CreateBooking.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBookingSystemBE.Application.Features.Slice.Bookings.CreateBooking.Validators
{
    public class CreateBookingCommandValidator : AbstractValidator<CreateBookingCommand>
    {
        public CreateBookingCommandValidator()
        {
            RuleFor(x => x.ResourceId)
                .GreaterThan(0).WithMessage("ResourceId must be greater than 0.");
            RuleFor(x => x.DateFrom)
                .LessThanOrEqualTo(x => x.DateTo).WithMessage("DateFrom must be earlier than or equal to DateTo.")
                .NotEmpty().WithMessage("DateFrom is required.");
            RuleFor(x => x.DateTo)
                .GreaterThanOrEqualTo(x => x.DateFrom).WithMessage("DateTo must be later than or equal to DateFrom.")
                .NotEmpty().WithMessage("DateTo is required.");
            RuleFor(x => x.BookedQuantity)
                .GreaterThan(0).WithMessage("BookedQuantity must be greater than 0.");
        }
    }
}
