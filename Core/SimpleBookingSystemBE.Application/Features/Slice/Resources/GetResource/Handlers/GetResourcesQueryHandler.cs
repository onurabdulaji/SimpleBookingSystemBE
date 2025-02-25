using MediatR;
using SimpleBookingSystemBE.Application.Features.Slice.Resources.GetResource.Queries;
using SimpleBookingSystemBE.Application.Features.Slice.Resources.GetResource.Results;
using SimpleBookingSystemBE.Application.Interfaces;
using SimpleBookingSystemBE.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBookingSystemBE.Application.Features.Slice.Resources.GetResource.Handlers
{
    public class GetResourcesQueryHandler : IRequestHandler<GetResourceQuery, ICollection<GetResourcesQueryResult>>
    {
        private readonly IRepository<Resource> _repository;

        public GetResourcesQueryHandler(IRepository<Resource> repository)
        {
            _repository = repository;
        }

        public async Task<ICollection<GetResourcesQueryResult>> Handle(GetResourceQuery request, CancellationToken cancellationToken)
        {
            var values = await _repository.GetAllAsync();
            return values.Select(x => new GetResourcesQueryResult
            {
                Id = x.Id,
                Name = x.Name,
                Quantity = x.Quantity,
            }).ToList();
        }
    }
}
