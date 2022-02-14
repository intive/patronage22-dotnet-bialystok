using FluentValidation;
using Patronage.Contracts.ModelDtos;

namespace Patronage.Api.Validators
{
    public class PartialBoardValidator : AbstractValidator<PartialBoardDto>
    {
        public PartialBoardValidator()
        {
            RuleFor(x => x.Name.Data).MaximumLength(1024).WithMessage("Can contain to 1024 characters.");
        }
    }
}
