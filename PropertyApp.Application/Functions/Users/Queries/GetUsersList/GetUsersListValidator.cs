using FluentValidation;

namespace PropertyApp.Application.Functions.Users.Queries.GetUsersList
{
    public class GetUsersListValidator:AbstractValidator<GetUsersListQuery>
    {
        private readonly int[] allowedPageSize = new int[] { 50, 100, 150 };
         public GetUsersListValidator()
        {
            
                RuleFor(p => p.PageNumber).GreaterThanOrEqualTo(1);

                RuleFor(p => p.PageSize).Custom((value, context) =>
                {
                    if (!allowedPageSize.Contains(value))
                    {
                        context.AddFailure($"PageSize", $"PageSize must be in [{string.Join(",", allowedPageSize)}] ");
                    }
                });

            
        }
    }
}