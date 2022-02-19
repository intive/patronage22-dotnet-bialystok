using FluentValidation;
using Patronage.Api.MediatR.Issues.Queries.GetIssues;

namespace Patronage.Api.Validators
{
    public class IssueQueryValidator : AbstractValidator<GetIssuesListQuery>
    {
        private int[] allowedPageSizes = new[] { 5, 10, 15 };

        public IssueQueryValidator()
        {
            RuleFor(r => r.PageNumber).GreaterThanOrEqualTo(1);
            RuleFor(r => r.PageSize).Custom((value, context) =>
            {
                if (!allowedPageSizes.Contains(value))
                {
                    context.AddFailure("PageSize", $"PageSize must in [{string.Join(",", allowedPageSizes)}]");
                }
            });
        }
    }
}
