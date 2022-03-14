using FluentValidation;
using Patronage.Contracts.ModelDtos.Board;
using Patronage.Models;

namespace Patronage.Api.Validators.Board
{
    public class UpdateBoardValidator : AbstractValidator<UpdateBoardDto>
    {
        public UpdateBoardValidator(TableContext tableContext)
        {
            RuleFor(x => x.Alias)
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
                }); ;

            RuleFor(x => x.Name)
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
                }); ;

            RuleFor(x => x.Description)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.ProjectId)
                .NotNull();

            RuleFor(x => x.StatusId)
                .NotNull();
        }
    }
}