using Microsoft.EntityFrameworkCore;
using SimpleBookingSystemBE.Application.Interfaces.ResourceInterface;
using SimpleBookingSystemBE.Domain.Entities;
using SimpleBookingSystemBE.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBookingSystemBE.Persistence.Repositories.ResourceRepositories
{
    public class ResourceRepository : IResourceRepository
    {
        private readonly AppDbContext _context;

        public ResourceRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DecreaseQuantityAsync(int resourceId, int quantity)
        {
            var resource = await _context.Resources.FindAsync(resourceId);
            if (resource == null || resource.Quantity < quantity)
            {
                return false;
            }

            resource.Quantity -= quantity;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<ICollection<Booking>> GetBookingsForResourceAsync(int resourceId, DateTime dateFrom, DateTime dateTo)
        {
            return await _context.Bookings
            .Where(b => b.ResourceId == resourceId && b.DateFrom <= dateTo && b.DateTo >= dateFrom)
            .ToListAsync();
        }

        public int GetTotalAvailableQuantity(int resourceId)
        {
            var resource = _context.Resources.Find(resourceId);
            return resource?.Quantity ?? 0;
        }
    }
}
