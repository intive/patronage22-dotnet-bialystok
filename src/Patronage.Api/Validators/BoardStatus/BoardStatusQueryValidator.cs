using FluentValidation;
using Patronage.Api.MediatR.BoardStatus.Queries;
using Patronage.Contracts.Helpers;

namespace Patronage.Api.Validators.Comments
{
    public class BoardStatusQueryValidator : AbstractValidator<GetAllBoardStatusQuery>
    {
        public BoardStatusQueryValidator()
        {
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