using FluentValidation;
using Patronage.Api.MediatR.Comment.Queries;

namespace Patronage.Api.Validators.Comments
{
    public class CommentQueryValidator : AbstractValidator<GetCommentsListQuery>
    {
        private int[] allowedPageSizes = new[] { 5, 10, 15 };

        public CommentQueryValidator()
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
