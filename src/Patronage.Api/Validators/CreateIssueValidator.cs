using FluentValidation;
using Patronage.Api.MediatR.Issues.Commands.CreateIssue;

namespace Patronage.Api.Validators
{
    public class CreateIssueValidator : AbstractValidator<CreateIssueCommand>
    {
        public CreateIssueValidator()
        {
            RuleFor(r => r.ProjectId).GreaterThanOrEqualTo(1);
        }
    }
}
