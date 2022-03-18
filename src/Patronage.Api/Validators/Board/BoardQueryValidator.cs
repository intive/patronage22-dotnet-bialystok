using FluentValidation;
using Patronage.Api.MediatR.Board.Queries.GetAll;

namespace Patronage.Api.Validators.Board
{
    public class BoardQueryValidator : AbstractValidator<GetBoardsQuery>
    {
        private int[] allowedPageSizes = new[] { 5, 10, 15 };

        public BoardQueryValidator()
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
