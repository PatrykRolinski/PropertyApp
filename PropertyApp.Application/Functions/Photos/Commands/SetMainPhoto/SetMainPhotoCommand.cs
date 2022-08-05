using MediatR;

namespace PropertyApp.Application.Functions.Photos.Commands.SetMainPhoto;

public class SetMainPhotoCommand: IRequest
{
    public int PropertyId { get; set; }
    public int PhotoId { get; set; }
}
