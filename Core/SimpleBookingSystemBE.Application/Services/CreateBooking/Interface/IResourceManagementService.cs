using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBookingSystemBE.Application.Services.CreateBooking.Interface
{
    public interface IResourceManagementService
    {
        Task<bool> DecreaseQuantityAsync(int resourceId, int quantity);

    }
}
