using FluentValidation;
using Patronage.Contracts.ModelDtos.Issues;
using Patronage.Models;

namespace Patronage.Api.Validators.Issues
{
    public class IssueDtoValidator : AbstractValidator<BaseIssueDto>
    {
        private readonly TableContext _dbContext;

        public IssueDtoValidator(TableContext dbContext)
        {
            _dbContext = dbContext;

            RuleFor(x => x.Alias)
                .NotNull()
                .NotEmpty()
                .MaximumLength(256)
                .Custom((value, context) =>
                {
                    var isAliasAlreadyTaken = dbContext.Issues.Any(p => p.Alias == value);
                    if (isAliasAlreadyTaken)
                    {
                        context.AddFailure("Alias", "This issue's alias has been already taken");
                    }
                });

            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(1024)
                .Custom((value, context) =>
                {
                    var isNameAlreadyTaken = dbContext.Issues.Any(p => p.Name == value);
                    if (isNameAlreadyTaken)
                    {
                        context.AddFailure("Name", "This issue's name has been already taken");
                    }
                });

            RuleFor(x => x.ProjectId)
                .NotNull()
                .NotEmpty()
                .GreaterThanOrEqualTo(1)
                .Must(ExistsProjectId).WithMessage("ProjectId does not exist.");

            RuleFor(x => x.StatusId)
                .NotNull()
                .NotEmpty()
                .GreaterThanOrEqualTo(1)
                .Must(ExistsStatusId).WithMessage("StatusId does not exist.");

            RuleFor(x => x.AssignUserId)
                .Must(ExistsUserId).WithMessage("UserId does not exist.");
        }

        private bool ExistsProjectId(int projectId)
        {
            var _boardId = _dbContext
                .Boards
                .Where(b => b.Id.Equals(projectId));
            if (_boardId.Any())
            {
                return true;
            }
            return false;
        }

        private bool ExistsStatusId(int statusId)
        {
            var _statusId = _dbContext
                .Statuses
                .Where(b => b.Id.Equals(statusId));
            if (_statusId.Any())
            {
                return true;
            }
            return false;
        }

        private bool ExistsUserId(string? userId)
        {
            if (userId is null)
            {
                return true;
            }
            var _userId = _dbContext
                .Users
                .Where(b => b.Id.Equals(userId));
            if (_userId.Any())
            {
                return true;
            }
            return false;
        }
    }
}