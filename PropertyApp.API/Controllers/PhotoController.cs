using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PropertyApp.Application.Functions.Photos.Commands.SetMainPhoto;

namespace PropertyApp.API.Controllers
{
    [Route("api/property/{propertyId}/photo")]
    [ApiController]
    public class PhotoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PhotoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPut("set-main-photo/{photoId}")]
        public async Task<ActionResult> SetMainPhoto([FromRoute] int propertyId, [FromRoute] int photoId )
        {
           await _mediator.Send(new SetMainPhotoCommand { PropertyId = propertyId, PhotoId = photoId });
           return NoContent();
        }
    }
}
