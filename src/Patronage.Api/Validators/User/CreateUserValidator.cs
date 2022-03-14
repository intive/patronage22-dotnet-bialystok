using FluentValidation;
using Patronage.Contracts.ModelDtos.User;
using Patronage.Models;

namespace Patronage.Api.Validators.User
{
    public class CreateUserValidator : AbstractValidator<CreateUserDto>
    {
        public CreateUserValidator(TableContext tableContext)
        {
            RuleFor(x => x.Email)
                .NotNull()
                .EmailAddress();

            RuleFor(x => x.Password)
                .NotNull()
                .MinimumLength(6).WithMessage("Password must be at least 6 characters")
                .Matches("[a-z]").WithMessage("Password must have at least one lowercase ('a'-'z')")
                .Matches("[A-Z]").WithMessage("Password must have at least one uppercase ('A'-'Z')")
                .Matches(@"\d").WithMessage("Password must have at least one digit ('0'-'9')")
                .Matches(@"[\W]").WithMessage("Password must have at least one non alphanumeric character");

            RuleFor(x => x.UserName)
                .NotNull()
                .MaximumLength(256)
                .Custom((value, context) =>
                {
                    var isNameAlreadyTaken = tableContext.Users.Any(p => p.UserName == value);
                    if (isNameAlreadyTaken)
                    {
                        context.AddFailure("UserName", "This username is already taken");
                    }
                });
        }
    }
}