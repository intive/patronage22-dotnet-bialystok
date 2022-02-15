using FluentValidation;
using FluentValidation.Results;
using Patronage.Contracts.ModelDtos;
using Patronage.Models;

namespace Patronage.Api.Validators
{
    public class PartialBoardValidator : AbstractValidator<PartialBoardDto>
    {
        public PartialBoardValidator(TableContext tableContext)
        {
            RuleFor(x => x.Alias.Data)
                .NotNull().WithMessage("Can not be null.")
                .NotEmpty().WithMessage("Can not be empty.")
                .MaximumLength(256).WithMessage("Can not exceed 256 characters.")
                .Custom((value, context) =>
                {
                    var isAliasAlreadyTaken = tableContext.Boards.Any(p => p.Alias == value);
                    if (isAliasAlreadyTaken)
                    {
                        context.AddFailure("Alias", "This board's alias has been already taken");
                    }
                })
                .When(y => y.Alias != null);

            RuleFor(x => x.Name.Data)
                .NotNull().WithMessage("Can not be null.")
                .NotEmpty().WithMessage("Can not be empty.")
                .MaximumLength(1024).WithMessage("Can not exceed 1024 characters.")              
                .Custom((value, context) =>
                {
                    var isNameAlreadyTaken = tableContext.Boards.Any(p => p.Name == value);
                    if (isNameAlreadyTaken)
                    {
                        context.AddFailure("Name", "This board's name has been already taken");
                    }
                })
                .When(y => y.Name != null);

            RuleFor(x => x.Description.Data)
                .NotNull().WithMessage("Can not be null.")
                .NotEmpty().WithMessage("Can not be empty.")
                .When(y => y.Description != null);

            RuleFor(x => x.ProjectId.Data)
                .NotNull().WithMessage("Can not be null.")
                .When(y => y.ProjectId != null);
        }
    }
}
