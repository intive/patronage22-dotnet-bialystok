using FluentValidation;
using Patronage.Api.MediatR.User.Commands.Password;
using Patronage.Models;

namespace Patronage.Api.Validators.User
{
    public class SendRecoverEmailPasswordCommandValidator : AbstractValidator<SendRecoverEmailPasswordCommand>
    {
        public SendRecoverEmailPasswordCommandValidator(TableContext tableContext)
        {
            RuleFor(x => x.recoverPasswordDto.Email!.Data)
                .NotNull()
                .EmailAddress()
                .Custom((value, context) =>
                {
                    var exist = tableContext.Users.Any(p => p.Email == value);
                    if (!exist)
                    {
                        context.AddFailure($"There's no user with email {value}");
                    }
                })
                .When(y => y.recoverPasswordDto.Email != null);

            RuleFor(x => x.recoverPasswordDto.Username!.Data)
                .NotNull()
                .Custom((value, context) =>
                {
                    var exist = tableContext.Users.Any(p => p.UserName == value);
                    if (!exist)
                    {
                        context.AddFailure($"There's no user with username {value}");
                    }
                })
                .When(y => y.recoverPasswordDto.Username != null);
        }
    }
}