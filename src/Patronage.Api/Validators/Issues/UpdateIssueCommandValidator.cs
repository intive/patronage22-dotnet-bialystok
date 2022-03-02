using FluentValidation;
using Patronage.Api.MediatR.Issues.Commands.UpdateIssue;
using Patronage.Api.Validators.Issues;

namespace Patronage.Api.Validators
{
    public class UpdateIssueCommandValidator : AbstractValidator<UpdateIssueCommand>
    {
        public UpdateIssueCommandValidator(IssueDtoValidator issueValidator)
        {
            RuleFor(x => x.Dto).SetValidator(issueValidator);
        }
    }
}
