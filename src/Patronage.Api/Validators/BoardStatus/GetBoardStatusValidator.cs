using FluentValidation;
using Patronage.Api.MediatR.BoardStatus.Queries;
using Patronage.Models;

namespace Patronage.Api.Validators.BoardStatus
{
    public class GetBoardStatusValidator : AbstractValidator<GetByIdBoardStatusQuery>
    {
        private readonly TableContext _context;

        public GetBoardStatusValidator(TableContext dbContext)
        {
            _context = dbContext;
            CascadeMode = CascadeMode.Stop;
            RuleFor(s => s.BoardId).Must(NotLessThan).WithMessage("BoardId must be greater than 0").Must(ExistsBoardId).WithMessage("BoardId does not exist"); ;
            RuleFor(s => s.StatusId).Must(NotLessThan).WithMessage("StatusId must be greater than 0").Must(ExistsStatusId).WithMessage("StatusId does not exist");
        }

        private bool ExistsBoardId(int boardId)
        {
            var _boardId = _context
                .Boards
                .Where(b => b.Id.Equals(boardId));
            if (_boardId.Any())
            {
                return true;
            }
            else { return false; }
        }

        private bool ExistsStatusId(int statusId)
        {
            var _statusId = _context
                .Statuses
                .Where(b => b.Id.Equals(statusId));
            if (_statusId.Any())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool NotLessThan(int id)
        {
            if (id > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}