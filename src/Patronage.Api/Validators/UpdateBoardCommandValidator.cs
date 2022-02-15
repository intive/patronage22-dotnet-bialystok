using FluentValidation;
using Patronage.Api.Functions.Commands.Board.Update;

namespace Patronage.Api.Validators
{
    public class UpdateBoardCommandValidator : AbstractValidator<UpdateBoardCommand>
    {
        public UpdateBoardCommandValidator(BoardDtoValidator boardValidator)
        {
            RuleFor(x => x.Data).SetValidator(boardValidator);
        }
    }
}
