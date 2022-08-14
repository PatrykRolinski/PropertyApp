using FluentValidation;

namespace PropertyApp.Application.Functions.Properties.Queries.GetPropertiesList;

public class GetPropertiesListValidator : AbstractValidator<GetPropertiesListQuery>
{
    private readonly int[] allowedPageSize = new int[] { 5, 10, 15 };
    public GetPropertiesListValidator()
    {
        RuleFor(p=> p.PageNumber).GreaterThanOrEqualTo(1);

        RuleFor(p => p.PageSize).Custom((value, context) =>
         {
             if (!allowedPageSize.Contains(value))
             {
                 context.AddFailure($"PageSize", $"PageSize must be in [{string.Join(",", allowedPageSize)}] ");
             }
         });
       
    }
}
