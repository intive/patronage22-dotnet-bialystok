using FluentValidation;
using Patronage.Api.MediatR.Issues.Commands.LightUpdateIssue;
using Patronage.Api.Validators.Issues;

namespace Patronage.Api.Validators
{
    public class UpdateLightIssueCommandValidator : AbstractValidator<UpdateLightIssueCommand>
    {
        public UpdateLightIssueCommandValidator(PartialIssueValidator partialIssueValidator)
        {
            RuleFor(x => x.Dto).SetValidator(partialIssueValidator);
        }
    }
}
