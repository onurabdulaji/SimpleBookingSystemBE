using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBookingSystemBE.Application.Services.CreateBooking.Interface
{
    public interface IBookingService
    {
        Task<bool> CanBookResourceAsync(int resourceId, DateTime dateFrom, DateTime dateTo, int requestedQuantity);

    }
}
