using FluentValidation;
using Patronage.Contracts.ModelDtos.Board;
using Patronage.Models;

namespace Patronage.Api.Validators.Board
{
    public class PartialBoardValidator : AbstractValidator<PartialBoardDto>
    {
        public PartialBoardValidator(TableContext tableContext)
        {
            RuleFor(x => x.Alias!.Data)
                .NotNull()
                .NotEmpty()
                .MaximumLength(256)
                .Custom((value, context) =>
                {
                    var isAliasAlreadyTaken = tableContext.Boards.Any(p => p.Alias == value);
                    if (isAliasAlreadyTaken)
                    {
                        context.AddFailure("Alias", "This board's alias has been already taken");
                    }
                })
                .When(y => y.Alias != null);

            RuleFor(x => x.Name!.Data)
                .NotNull()
                .NotEmpty()
                .MaximumLength(1024)
                .Custom((value, context) =>
                {
                    var isNameAlreadyTaken = tableContext.Boards.Any(p => p.Name == value);
                    if (isNameAlreadyTaken)
                    {
                        context.AddFailure("Name", "This board's name has been already taken");
                    }
                })
                .When(y => y.Name != null);

            RuleFor(x => x.Description!.Data)
                .NotNull()
                .NotEmpty()
                .When(y => y.Description != null);

            RuleFor(x => x.ProjectId!.Data)
                .NotNull()
                .When(y => y.ProjectId != null);
        }
    }
}