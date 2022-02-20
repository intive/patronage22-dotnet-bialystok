using FluentValidation;
using Patronage.Api.MediatR.Projects.Commands;
using Patronage.Models;

namespace Patronage.Api.Validators.Projects
{
    public class UpdateProjectCommandValidator : AbstractValidator<UpdateProjectCommand>
    {
        public UpdateProjectCommandValidator(TableContext tableContext)
        {
            RuleFor(p => p.dto.Alias)
                .MaximumLength(256)
                .NotEmpty()
                .NotNull()
                .Custom((value, context) =>
                {
                    var isAliasAlreadyTaken = tableContext.Projects.Any(p => p.Alias == value);
                    if (isAliasAlreadyTaken)
                    {
                        context.AddFailure("Alias", "This project's alias has been already taken");
                    }
                });


            RuleFor(p => p.dto.Name)
                .MaximumLength(1024)
                .NotEmpty()
                .NotNull()
                .Custom((value, context) =>
                {
                    var isNameAlreadyTaken = tableContext.Projects.Any(p => p.Name == value);
                    if (isNameAlreadyTaken)
                    {
                        context.AddFailure("Name", "This project's name has been already taken");
                    }
                });
        }
    }
}
