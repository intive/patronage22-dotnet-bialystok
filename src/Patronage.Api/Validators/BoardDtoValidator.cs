using FluentValidation;
using Patronage.Contracts.ModelDtos;

namespace Patronage.Api.Validators
{
    public class BoardDtoValidator : AbstractValidator<BoardDto>
    {
        public BoardDtoValidator()
        {
            RuleFor(x => x.Name).MaximumLength(1024).WithMessage("Can contain to 1024 characters.");
            RuleFor(x => x.Alias).MaximumLength(256).WithMessage("Can contain to 256 characters.");
            RuleFor(x => x.ProjectId).NotNull().WithMessage("Can not be null.");
        }
    }
}
