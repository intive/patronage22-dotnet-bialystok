using FluentValidation;
using Patronage.Api.MediatR.Comment.Queries;
using Patronage.Contracts.Helpers;

namespace Patronage.Api.Validators.Comments
{
    public class CommentQueryValidator : AbstractValidator<GetCommentsListQuery>
    {
        public CommentQueryValidator()
        {
            RuleFor(r => r.filter.IssueId).GreaterThanOrEqualTo(1);
            RuleFor(r => r.filter.PageNumber).GreaterThanOrEqualTo(1);
            RuleFor(r => r.filter.PageSize).Custom((value, context) =>
            {
                if (!PropertyForQuery.AllowedPageSizes.Contains(value))
                {
                    context.AddFailure("PageSize", $"PageSize must in [{string.Join(", ", PropertyForQuery.AllowedPageSizes)}]");
                }
            });
        }
    }
}