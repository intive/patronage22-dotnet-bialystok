using FluentValidation;
using Patronage.Contracts.ModelDtos.Issues;
using Patronage.Models;

namespace Patronage.Api.Validators.Issues
{
    public class PartialIssueValidator : AbstractValidator<PartialIssueDto>
    {
        public PartialIssueValidator(TableContext dbContext)
        {
            RuleFor(x => x.Alias!.Data)
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
                })
                .When(y => y.Alias != null);

            RuleFor(x => x.Name!.Data)
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
                })
                .When(y => y.Name != null);

            RuleFor(x => x.ProjectId!.Data)
                .NotNull().WithMessage("Can not be null.")
                .When(y => y.ProjectId != null)
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
