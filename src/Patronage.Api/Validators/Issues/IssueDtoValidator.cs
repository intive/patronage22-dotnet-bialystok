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
                .NotNull().WithMessage("Can not be null.")
                .NotEmpty().WithMessage("Can not be empty.")
                .MaximumLength(256).WithMessage("Can not exceed 256 characters.")
                .Custom((value, context) =>
                {
                    var isAliasAlreadyTaken = dbContext.Issues.Any(p => p.Alias == value);
                    if (isAliasAlreadyTaken)
                    {
                        context.AddFailure("Alias", "This issue's alias has been already taken");
                    }
                });

            RuleFor(x => x.Name)
                .NotNull().WithMessage("Can not be null.")
                .NotEmpty().WithMessage("Can not be empty.")
                .MaximumLength(1024).WithMessage("Can not exceed 1024 characters.")
                .Custom((value, context) =>
                {
                    var isNameAlreadyTaken = dbContext.Issues.Any(p => p.Name == value);
                    if (isNameAlreadyTaken)
                    {
                        context.AddFailure("Name", "This issue's name has been already taken");
                    }
                });

            RuleFor(x => x.ProjectId)
                .NotNull().WithMessage("Can not be null.")
                .NotEmpty().WithMessage("Can not be empty.")
                .GreaterThanOrEqualTo(1)
                .Custom((value, context) =>
                {
                    var isExistProject = dbContext.Issues.Any(p => p.ProjectId == value);
                    if (!isExistProject)
                    {
                        context.AddFailure("ProjectId", "This project id has not been already exist");
                    }
                });
        }
    }
}
