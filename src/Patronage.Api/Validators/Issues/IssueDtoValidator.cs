using FluentValidation;
using Patronage.Contracts.ModelDtos.Issues;
using Patronage.Models;

namespace Patronage.Api.Validators.Issues
{
    public class IssueDtoValidator : AbstractValidator<BaseIssueDto>
    {
        public IssueDtoValidator(TableContext dbContext)
        {
            RuleFor(x => x.Alias)
                .NotNull()
                .NotEmpty()
                .MaximumLength(256)
                .Custom((value, context) =>
                {
                    var isAliasAlreadyTaken = dbContext.Issues.Any(p => p.Alias == value);
                    if (isAliasAlreadyTaken)
                    {
                        context.AddFailure("Alias", "This issue's alias has been already taken");
                    }
                });

            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(1024)
                .Custom((value, context) =>
                {
                    var isNameAlreadyTaken = dbContext.Issues.Any(p => p.Name == value);
                    if (isNameAlreadyTaken)
                    {
                        context.AddFailure("Name", "This issue's name has been already taken");
                    }
                });

            RuleFor(x => x.ProjectId)
                .NotNull()
                .NotEmpty()
                .GreaterThanOrEqualTo(1);

            RuleFor(x => x.StatusId)
                .NotNull()
                .NotEmpty()
                .GreaterThanOrEqualTo(1);
        }
    }
}
