using FluentValidation;
using Patronage.Api.MediatR.Commands;

namespace Patronage.Api.Validators
{
    public class CreateBoardCommandValidator : AbstractValidator<CreateBoardCommand>
    {
        public CreateBoardCommandValidator(BoardDtoValidator boardValidator)
        {
            RuleFor(x => x.Data).SetValidator(boardValidator);
        }
    }
}
