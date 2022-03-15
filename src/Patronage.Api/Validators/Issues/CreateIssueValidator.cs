using FluentValidation;
using Patronage.Api.MediatR.Issues.Commands;

namespace Patronage.Api.Validators.Issues
{
    public class CreateIssueValidator : AbstractValidator<CreateIssueCommand>
    {
        public CreateIssueValidator(IssueDtoValidator issueValidator)
        {
            RuleFor(x => x.Data).SetValidator(issueValidator);
        }
    }
}
