using MediatR;
using SimpleBookingSystemBE.Application.Features.Slice.Resources.GetResource.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBookingSystemBE.Application.Features.Slice.Resources.GetResource.Queries
{
    public class GetResourceQuery : IRequest<ICollection<GetResourcesQueryResult>>
    {
    }
}
