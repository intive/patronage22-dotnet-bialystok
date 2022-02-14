using FluentValidation;
using Patronage.Api.MediatR.Commands;

namespace Patronage.Api.Validators
{
    public class CreateBoardCommandValidator : AbstractValidator<CreateBoardCommand>
    {
        public CreateBoardCommandValidator()
        {
            RuleFor(x => x.Data).SetValidator(new BoardDtoValidator());
        }
    }
}
