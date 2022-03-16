using FluentValidation;
using Patronage.Api.MediatR.Board.Commands.Update;

namespace Patronage.Api.Validators.Board
{
    public class UpdateBoardCommandValidator : AbstractValidator<UpdateBoardCommand>
    {
        public UpdateBoardCommandValidator(UpdateBoardValidator boardValidator)
        {
            RuleFor(x => x.Data).SetValidator(boardValidator);
        }
    }
}
