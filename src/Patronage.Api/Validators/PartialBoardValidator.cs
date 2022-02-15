using FluentValidation;
using FluentValidation.Results;
using Patronage.Contracts.ModelDtos;

namespace Patronage.Api.Validators
{
    public class PartialBoardValidator : AbstractValidator<PartialBoardDto>
    {
        public PartialBoardValidator()
        {
            RuleFor(x => x.Alias.Data)
                .NotNull().WithMessage("Can not be null.")
                .NotEmpty().WithMessage("Can not be empty.")
                .MaximumLength(256).WithMessage("Can not exceed 256 characters.")
                .When(y => y.Alias != null);

            RuleFor(x => x.Name.Data)
                .NotNull().WithMessage("Can not be null.")
                .NotEmpty().WithMessage("Can not be empty.")
                .MaximumLength(1024).WithMessage("Can not exceed 1024 characters.")
                .When(y => y.Name != null);

            RuleFor(x => x.Description.Data)
                .NotNull().WithMessage("Can not be null.")
                .NotEmpty().WithMessage("Can not be empty.")
                .When(y => y.Description != null);

            RuleFor(x => x.ProjectId.Data)
                .NotNull().WithMessage("Can not be null.");
        }
    }
}
