using FluentValidation;
using Patronage.Api.MediatR.BoardStatus.Commands;
using Patronage.Models;

namespace Patronage.Api.Validators.BoardStatus
{
    public class CreateBoardStatusValidator : AbstractValidator<CreateBoardStatusCommand>
    {
        private readonly TableContext _context;

        public CreateBoardStatusValidator(TableContext dbContext)
        {
            _context = dbContext;

            RuleFor(s => s.Dto.BoardId)
                .NotNull()
                .NotEmpty()
                .GreaterThanOrEqualTo(1);

            RuleFor(s => s.Dto.StatusId)
                .NotNull()
                .NotEmpty()
                .GreaterThanOrEqualTo(1);
        }
    }
}