using SimpleBookingSystemBE.Application.Interfaces.ResourceInterface;
using SimpleBookingSystemBE.Application.Services.CreateBooking.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBookingSystemBE.Application.Services.CreateBooking.Service
{
    public class ResourceManagementService : IResourceManagementService
    {
        private readonly IResourceRepository _resourceRepository;

        public ResourceManagementService(IResourceRepository resourceRepository)
        {
            _resourceRepository = resourceRepository;
        }

        public async Task<bool> DecreaseQuantityAsync(int resourceId, int quantity)
        {
            return await _resourceRepository.DecreaseQuantityAsync(resourceId, quantity);
        }
    }
}
