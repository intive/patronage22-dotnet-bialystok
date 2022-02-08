using FluentValidation;
using Patronage.Models;

namespace Patronage.Contracts.ModelDtos
{
    public class CreateOrUpdateProjectDtoValidator : AbstractValidator<CreateOrUpdateProjectDto>
    {
        public CreateOrUpdateProjectDtoValidator(TableContext tableContext)
        {
            RuleFor(p => p.Alias)
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


            RuleFor(p => p.Name)
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
