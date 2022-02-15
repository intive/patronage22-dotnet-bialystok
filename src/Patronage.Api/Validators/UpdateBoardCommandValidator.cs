using FluentValidation;
using Patronage.Api.MediatR.Board.Commands.Update;

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
