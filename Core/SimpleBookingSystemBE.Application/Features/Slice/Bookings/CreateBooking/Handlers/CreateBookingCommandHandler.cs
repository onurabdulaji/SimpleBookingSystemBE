using MediatR;
using SimpleBookingSystemBE.Application.Features.Slice.Bookings.CreateBooking.Commands;
using SimpleBookingSystemBE.Application.Features.Slice.Bookings.CreateBooking.Validators;
using SimpleBookingSystemBE.Application.Interfaces;
using SimpleBookingSystemBE.Application.Interfaces.BookingInterface;
using SimpleBookingSystemBE.Application.Interfaces.ResourceInterface;
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
        private readonly IResourceManagementService _resourceManagementService;
        private readonly CreateBookingCommandValidator _createBookingCommandValidator;
        private readonly IBookingRepository _bookingRepository;
        private readonly IResourceRepository _resourceRepository;


        public CreateBookingCommandHandler(IRepository<Booking> repository, IBookingService bookingService, IBookingConflictChecker bookingConflictChecker, IBookingEmailService bookingEmailService, IResourceManagementService resourceManagementService, CreateBookingCommandValidator createBookingCommandValidator, IBookingRepository bookingRepository, IResourceRepository resourceRepository)
        {
            _repository = repository;
            _bookingService = bookingService;
            _bookingConflictChecker = bookingConflictChecker;
            _bookingEmailService = bookingEmailService;
            _resourceManagementService = resourceManagementService;
            _createBookingCommandValidator = createBookingCommandValidator;
            _bookingRepository = bookingRepository;
            _resourceRepository = resourceRepository;
        }

        public async Task Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _createBookingCommandValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                throw new InvalidOperationException($"Validation Error: {string.Join(", ", validationResult.Errors)}");
            }

            int availableQuantity = await _resourceRepository.GetTotalAvailableQuantity(request.ResourceId);

            var existingBookings = await _bookingRepository.GetExistingBookingsAsync(request.ResourceId, request.DateFrom, request.DateTo);
            int totalBookedQuantity = existingBookings.Sum(b => b.BookedQuantity);

            if (request.BookedQuantity > availableQuantity)
            {
                throw new InvalidOperationException("Booking failed: Not enough resources available.");
            }

            var conflictResult = await _bookingConflictChecker.IsBookingConflictAsync(
                request.ResourceId, request.DateFrom, request.DateTo, request.BookedQuantity
            );

            if (conflictResult.IsConflict)
            {
                throw new InvalidOperationException("Booking failed: The selected resource is already booked during this period.");
            }

            var booking = new Booking
            {
                ResourceId = request.ResourceId,
                DateFrom = request.DateFrom,
                DateTo = request.DateTo,
                BookedQuantity = request.BookedQuantity
            };

            await _repository.CreateAsync(booking);

            bool quantityDecreased = await _resourceManagementService.DecreaseQuantityAsync(request.ResourceId, request.BookedQuantity);
            if (!quantityDecreased)
            {
                throw new InvalidOperationException("Booking failed: Could not update resource quantity.");
            }

            await _bookingEmailService.SendBookingConfirmationConsoleEmailAsync(booking.Id);

        }
    }
}
