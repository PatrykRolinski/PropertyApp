using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PropertyApp.Application.Functions.Photos.Commands.AddPhoto;
using PropertyApp.Application.Functions.Photos.Commands.DeletePropertyPhoto;
using PropertyApp.Application.Functions.Photos.Commands.SetMainPhoto;
using PropertyApp.Application.Functions.Photos.Queries;

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
        [HttpPost]
        public async Task<ActionResult> AddPhotos([FromRoute] int propertyId, [FromForm] ICollection<IFormFile> formFiles)
        {
            await _mediator.Send(new AddPhotoCommand { PropertyId = propertyId, PhotoFiles = formFiles });
            return Created($"/api/property/{propertyId}/photo", null);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetPhotosListForPropertyDto>>> GetAllPhotosForProperty([FromRoute] int propertyId)
        {
           var photosList= await _mediator.Send(new GetPhotosListForPropertyQuery() { PropertyId= propertyId });
           return Ok(photosList);
        }
        [HttpDelete("{photoId}")]
        public async Task<ActionResult> DeletePhoto([FromRoute]int propertyId, [FromRoute] int photoId)
        {
            await _mediator.Send(new DeletePropertyPhotoCommand() { PropertyId = propertyId, PhotoId = photoId });
            return NoContent();
        }
        
    }
}
