using FluentValidation;
using Patronage.Api.MediatR.Commands;

namespace Patronage.Api.Validators
{
    public class UpdateBoardCommandValidator : AbstractValidator<UpdateBoardCommand>
    {
        public UpdateBoardCommandValidator()
        {
            RuleFor(x => x.Data).SetValidator(new BoardDtoValidator());
        }
    }
}
