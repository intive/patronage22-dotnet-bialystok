using FluentValidation;
using Patronage.Contracts.ModelDtos;

namespace Patronage.Api.Validators
{
    public class BoardDtoValidator : AbstractValidator<BoardDto>
    {
        public BoardDtoValidator()
        {
            RuleFor(x => x.Alias)
                .NotNull().WithMessage("Can not be null.")
                .NotEmpty().WithMessage("Can not be empty.")
                .MaximumLength(256).WithMessage("Can not exceed 256 characters.");

            RuleFor(x => x.Name)
                .NotNull().WithMessage("Can not be null.")
                .NotEmpty().WithMessage("Can not be empty.")
                .MaximumLength(1024).WithMessage("Can not exceed 1024 characters.");

            RuleFor(x => x.Description)
                .NotNull().WithMessage("Can not be null.")
                .NotEmpty().WithMessage("Can not be empty.");

            RuleFor(x => x.ProjectId)
                .NotNull().WithMessage("Can not be null.");

            RuleFor(x => x.IsActive)
                .NotNull().WithMessage("Can not be null.");
        }
    }
}
