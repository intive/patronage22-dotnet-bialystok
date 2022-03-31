using FluentValidation;
using Patronage.Api.MediatR.Board.Queries.GetAll;
using Patronage.Contracts.Helpers;

namespace Patronage.Api.Validators.Board
{
    public class BoardQueryValidator : AbstractValidator<GetBoardsQuery>
    {
        public BoardQueryValidator()
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
