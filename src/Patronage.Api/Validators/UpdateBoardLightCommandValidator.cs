using FluentValidation;
using Patronage.Api.Functions.Commands.Board.UpdateLight;

namespace Patronage.Api.Validators
{
    public class UpdateBoardLightCommandValidator : AbstractValidator<UpdateBoardLightCommand>
    {
        public UpdateBoardLightCommandValidator(PartialBoardValidator partialBoardValidator)
        {
            RuleFor(x => x.Data).SetValidator(partialBoardValidator);
        }
    }
}
