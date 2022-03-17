using FluentValidation;
using Patronage.Api.MediatR.Status.Commands;
using Patronage.Models;

namespace Patronage.Api.Validators.Statuses
{
    public class UpdateStatusValidator : AbstractValidator<UpdateStatusCommand>
    {
        private readonly TableContext _dbContext;

        public UpdateStatusValidator(TableContext dbContext)
        {
            _dbContext = dbContext;
            RuleFor(s => s.Status).NotEmpty().WithMessage("Code field cannot be empty.")
                                .Custom((code, context) =>
                                {
                                    var statusTaken = _dbContext.Statuses.Any(p => p.Code == code);
                                    if (statusTaken)
                                    {
                                        context.AddFailure("Code", "Status code already exists");
                                    }
                                });
            RuleFor(s => s.Id).NotEmpty().WithMessage("Please, specify id");
        }
    }
}