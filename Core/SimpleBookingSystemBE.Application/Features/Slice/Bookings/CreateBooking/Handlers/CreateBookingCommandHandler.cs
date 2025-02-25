﻿using MediatR;
using SimpleBookingSystemBE.Application.Features.Slice.Bookings.CreateBooking.Commands;
using SimpleBookingSystemBE.Application.Interfaces;
using SimpleBookingSystemBE.Application.Services.CreateBooking.Interface;
using SimpleBookingSystemBE.Domain.Entities;

namespace SimpleBookingSystemBE.Application.Features.Slice.Bookings.CreateBooking.Handlers
{
    public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand>
    {
        private readonly IBookingService _bookingService;
        private readonly IRepository<Booking> _repository;
        private readonly IBookingConflictChecker _bookingConflictChecker;
        private readonly IBookingEmailService _bookingEmailService;

        public CreateBookingCommandHandler(IRepository<Booking> repository, IBookingService bookingService, IBookingConflictChecker bookingConflictChecker, IBookingEmailService bookingEmailService)
        {
            _repository = repository;
            _bookingService = bookingService;
            _bookingConflictChecker = bookingConflictChecker;
            _bookingEmailService = bookingEmailService;
        }

        public async Task Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
            // Gün bazlı rezervasyon kontrolü
            bool canBook = await _bookingService.CanBookResourceAsync(request.ResourceId, request.DateFrom, request.DateTo, request.BookedQuantity);
            if (!canBook)
            {
                throw new InvalidOperationException("Requested resource is not available for the given dates and quantity.");
            }

            // Genel çakışma kontrolü
            var conflictResult = await _bookingConflictChecker.IsBookingConflictAsync(request.ResourceId, request.DateFrom, request.DateTo, request.BookedQuantity);
            if (conflictResult.IsConflict)
            {
                throw new InvalidOperationException("Requested resource is not available due to an existing booking.");
            }

            // Rezervasyon işlemi
            var booking = new Booking
            {
                ResourceId = request.ResourceId,
                DateFrom = request.DateFrom,
                DateTo = request.DateTo,
                BookedQuantity = request.BookedQuantity
            };

            await _repository.CreateAsync(booking);

            await _bookingEmailService.SendBookingConfirmationConsoleEmailAsync(booking.Id);

        }
    }
}
