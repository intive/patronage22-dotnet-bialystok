using FluentValidation;
using Patronage.Api.MediatR.User.Commands.Password;

namespace Patronage.Api.Validators.User
{
    public class RecoverPasswordCommandValidator : AbstractValidator<RecoverPasswordCommand>
    {
        public RecoverPasswordCommandValidator(NewUserPasswordValidator validator)
        {
            RuleFor(x => x.NewUserPassword).SetValidator(validator);
        }
    }
}