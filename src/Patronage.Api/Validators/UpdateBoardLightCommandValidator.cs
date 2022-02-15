using FluentValidation;
using Patronage.Api.MediatR.Commands;

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
