using MediatR;

namespace PropertyApp.Application.Functions.Photos.Commands.DeletePropertyPhoto;

public class DeletePropertyPhotoCommand: IRequest
{
    public int PropertyId { get; set; }
    public int PhotoId { get; set; }
}
