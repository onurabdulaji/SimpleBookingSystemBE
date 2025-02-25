using SimpleBookingSystemBE.Application.Services.CreateBooking.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBookingSystemBE.Application.Services.CreateBooking.Service
{
    public class BookingEmailService : IBookingEmailService
    {
        public async Task SendBookingConfirmationConsoleEmailAsync(int bookingId)
        {
            Console.WriteLine($"EMAIL SENT TO admin@admin.com FOR CREATED BOOKING WITH ID {bookingId}");
            Console.WriteLine("Email sent successfully!");
            await Task.CompletedTask;
        }
    }
}
