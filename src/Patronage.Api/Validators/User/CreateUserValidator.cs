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
                .NotNull().WithMessage("'Email' can not be null.")
                .EmailAddress().WithMessage("'Email' is not a valid email address.");

            RuleFor(x => x.Password)
                .NotNull().WithMessage("'Password can not be null.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters")
                .Matches("[a-z]").WithMessage("Password must have at least one lowercase ('a'-'z')")
                .Matches("[A-Z]").WithMessage("Password must have at least one uppercase ('A'-'Z')")
                .Matches(@"\d").WithMessage("Password must have at least one digit ('0'-'9')")
                .Matches(@"[\W]").WithMessage("Password must have at least one non alphanumeric character");

            RuleFor(x => x.UserName)
                .NotNull().WithMessage("'UserName can not be null.")
                .MaximumLength(256).WithMessage("UserName can not exceed 256 characters.")
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
