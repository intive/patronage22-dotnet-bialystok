using FluentValidation;
using Patronage.Contracts.ModelDtos;
using Patronage.Models;

namespace Patronage.Api.Validators
{
    public class BoardDtoValidator : AbstractValidator<BoardDto>
    {
        public BoardDtoValidator(TableContext tableContext)
        {
            RuleFor(x => x.Alias)
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
                }); ;

            RuleFor(x => x.Name)
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
                }); ;

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
