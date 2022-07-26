using MediatR;

namespace PropertyApp.Application.Functions.Properties.Queries.GetPropertyDetail;

public class GetPropertyDetailQuery : IRequest<GetPropertyDetailDto>
{
    public int Id { get; set; }
}
