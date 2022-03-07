using FluentValidation;
using Patronage.Api.MediatR.User.Commands.Create;

namespace Patronage.Api.Validators.User
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator(CreateUserValidator validator)
        {
            RuleFor(x => x.CreateUserDto).SetValidator(validator);
        }
    }
}
