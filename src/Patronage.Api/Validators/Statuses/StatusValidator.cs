using FluentValidation;
using Patronage.Contracts.ModelDtos;
using Patronage.Models;

namespace Patronage.Api.Validators.Statuses
{
    public class StatusValidator : AbstractValidator<StatusDto>
    {
        private readonly TableContext _dbContext;

        public StatusValidator(TableContext dbContext)
        {
            _dbContext = dbContext;
            RuleFor(s => s.Code).NotEmpty().WithMessage("Code field cannot be empty.")
                                .Custom((code, context) =>
                                {
                                    var statusTaken = _dbContext.Statuses.Any(p => p.Code == code);
                                    if (statusTaken)
                                    {
                                        context.AddFailure("Code", "Status code already exists");
                                    }
                                });
        }
    }
}
