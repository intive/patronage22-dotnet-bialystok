using FluentValidation;
using Patronage.Api.MediatR.Commands;

namespace Patronage.Api.Validators
{
    public class UpdateBoardLightCommandValidator : AbstractValidator<UpdateBoardCommand>
    {
        public UpdateBoardLightCommandValidator()
        {
            RuleFor(x => x.Data.Name).MaximumLength(1024).WithMessage("Can contain to 1024 characters.");
        }
    }
}
