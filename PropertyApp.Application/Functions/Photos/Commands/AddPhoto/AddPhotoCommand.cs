using MediatR;
using Microsoft.AspNetCore.Http;

namespace PropertyApp.Application.Functions.Photos.Commands.AddPhoto;

public class AddPhotoCommand:IRequest
{
   public int PropertyId { get; set; }
   public ICollection<IFormFile>? PhotoFiles { get; set; }
}
