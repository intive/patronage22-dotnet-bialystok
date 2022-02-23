using FluentValidation;
using Patronage.Api.MediatR.BoardStatus;
using Patronage.Api.MediatR.BoardStatus.Commands;
using Patronage.Contracts.ModelDtos;
using Patronage.Models;

namespace Patronage.Api.Validators.BoardStatus
{
    public class CreateBoardStatusValidator : AbstractValidator<CreateBoardStatusCommand>
    {
        private readonly TableContext _context;

        public CreateBoardStatusValidator(TableContext dbContext)
        {
            _context = dbContext;
            this.CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(s => s.Dto.BoardId).Must(NotLessThan).WithMessage("BoardId must be greater than 0").Must(ExistsBoardId).WithMessage("BoardId does not exist"); ;
            RuleFor(s => s.Dto.StatusId).Must(NotLessThan).WithMessage("StatusId must be greater than 0").Must(ExistsStatusId).WithMessage("StatusId does not exist");

            RuleFor(x => new { x.Dto.BoardId, x.Dto.StatusId })
                .Must(m => ExistsBoardStatus(m.BoardId, m.StatusId))
                .WithMessage($"BoardStatus with specified boardId and statusId already exists");
        }

        public bool ExistsBoardStatus(int boardId, int statusId)
        {
            var boardStatus = _context
                .BoardsStatus
                .Where(b => b.BoardId.Equals(boardId))
                .Where(b => b.StatusId.Equals(statusId));
            if (boardStatus.Any())
            {
                return false;
            }
            else
            {
                return true;
            }
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
