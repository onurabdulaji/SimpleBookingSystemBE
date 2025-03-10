﻿using SimpleBookingSystemBE.Domain.Entities;

namespace SimpleBookingSystemBE.Application.Interfaces.ResourceInterface
{
    public interface IResourceRepository
    {
        Task<ICollection<Booking>> GetBookingsForResourceAsync(int resourceId, DateTime dateFrom, DateTime dateTo);
        Task<int> GetTotalAvailableQuantity(int resourceId);
        Task<bool> DecreaseQuantityAsync(int resourceId, int quantity);

    }
}
