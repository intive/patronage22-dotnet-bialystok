using FluentValidation;
using Patronage.Api.MediatR.Board.Commands.Create;

namespace Patronage.Api.Validators.Board
{
    public class CreateBoardCommandValidator : AbstractValidator<CreateBoardCommand>
    {
        public CreateBoardCommandValidator(BoardDtoValidator boardValidator)
        {
            RuleFor(x => x.Data).SetValidator(boardValidator);
        }
    }
}
