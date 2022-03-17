using FluentValidation;
using Patronage.Api.MediatR.Projects.Commands;
using Patronage.Models;

namespace Patronage.Api.Validators.Projects
{
    public class LightUpdateProjectCommandValidator : AbstractValidator<LightUpdateProjectCommand>
    {
        public LightUpdateProjectCommandValidator(TableContext tableContext)
        {
            When(p => p.dto.Alias != null, () =>
            {
                RuleFor(p => p.dto.Alias!.Data)
                .NotEmpty().WithMessage("Field Alias can not be empty")
                .NotNull().WithMessage("Field Alias can not be null")
                .MaximumLength(256).WithMessage("Field name can have maximum 256 charakters")
                .Custom((value, context) =>
                {
                    var isAliasAlreadyTaken = tableContext.Projects.Any(p => p.Alias == value);
                    if (isAliasAlreadyTaken)
                    {
                        context.AddFailure("Alias", "This project's alias has been already taken");
                    }
                });
            });

            When(p => p.dto.Name != null, () =>
            {
                RuleFor(p => p.dto.Name!.Data)
                    .NotEmpty().WithMessage("Field Name can not be empty")
                    .NotNull().WithMessage("Field Name can not be null")
                    .MaximumLength(1024).WithMessage("Field alias can have maximum 1024 charakters")
                    .Custom((value, context) =>
                    {
                        var isNameAlreadyTaken = tableContext.Projects.Any(p => p.Name == value);
                        if (isNameAlreadyTaken)
                        {
                            context.AddFailure("Name", "This project's name has been already taken");
                        }
                    });
            });

            When(p => p.dto.IsActive != null, () =>
            {
                RuleFor(p => p.dto.IsActive!.Data)
                    .NotNull().WithMessage("Field IsActive can not be null");
            });
        }
    }
}