using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleBookingSystemBE.Application.Features.Slice.Resources.GetResource.Queries;

namespace SimpleBookingSystemBE.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourcesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ResourcesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetResources()
        {
            var values = await _mediator.Send(new GetResourceQuery());
            return Ok(values);
        }
    }
}
