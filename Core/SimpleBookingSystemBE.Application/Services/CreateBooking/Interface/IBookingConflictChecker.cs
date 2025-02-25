using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBookingSystemBE.Application.Services.CreateBooking.Interface
{
    public interface IBookingConflictChecker
    {
        Task<BookingConflictResult> IsBookingConflictAsync(int resourceId, DateTime startTime, DateTime endTime, int requestedQuantity);
    }
}
