using FluentValidation;
using Patronage.Api.MediatR.Issues.Queries.GetAll;

namespace Patronage.Api.Validators.Issues
{
    public class IssueQueryValidator : AbstractValidator<GetIssuesListQuery>
    {
        private int[] allowedPageSizes = new[] { 5, 10, 15 };

        public IssueQueryValidator()
        {
            RuleFor(r => r.filter.PageNumber).GreaterThanOrEqualTo(1);
            RuleFor(r => r.filter.PageSize).Custom((value, context) =>
            {
                if (!allowedPageSizes.Contains(value))
                {
                    context.AddFailure("PageSize", $"PageSize must in [{string.Join(", ", allowedPageSizes)}]");
                }
            });
        }
    }
}
