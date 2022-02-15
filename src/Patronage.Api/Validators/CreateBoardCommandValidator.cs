using FluentValidation;
using Patronage.Api.Functions.Commands.Board.Create;

namespace Patronage.Api.Validators
{
    public class CreateBoardCommandValidator : AbstractValidator<CreateBoardCommand>
    {
        public CreateBoardCommandValidator(BoardDtoValidator boardValidator)
        {
            RuleFor(x => x.Data).SetValidator(boardValidator);
        }
    }
}
