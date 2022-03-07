using FluentValidation;
using Patronage.Contracts.ModelDtos.User;

namespace Patronage.Api.Validators.User
{
    public class NewUserPasswordValidator : AbstractValidator<NewUserPasswordDto>
    {
        public NewUserPasswordValidator()
        {
            RuleFor(x => x.Id)
                .NotNull().WithMessage("Id can not be null.");

            RuleFor(x => x.Password)
                .NotNull().WithMessage("'Password can not be null.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters")
                .Matches("[a-z]").WithMessage("Password must have at least one lowercase ('a'-'z')")
                .Matches("[A-Z]").WithMessage("Password must have at least one uppercase ('A'-'Z')")
                .Matches(@"\d").WithMessage("Password must have at least one digit ('0'-'9')")
                .Matches(@"[\W]").WithMessage("Password must have at least one non alphanumeric character");

            RuleFor(x => x.Token)
                .NotNull().WithMessage("Token can not be null.");
        }
    }
}
