using FluentValidation;
using Patronage.Api.MediatR.BoardStatus;
using Patronage.Api.MediatR.BoardStatus.Commands;
using Patronage.Contracts.ModelDtos;
using Patronage.Models;

namespace Patronage.Api.Validators
{
    public class BoardStatusValidator : AbstractValidator<CreateBoardStatusCommand>
    {
        private readonly TableContext _context;

        public BoardStatusValidator(TableContext dbContext)
        {
            _context = dbContext;
            this.CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(b => b.Dto.BoardId).Must(ExistsBoardId).WithMessage("BoardId does not exist");
              
            RuleFor(s => s.Dto.BoardId).Must(ExistsStatusId).WithMessage("StatusId does not exist");

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

        public bool ExistsBoardId(int boardId)
        {
            var _boardId = _context
                .BoardsStatus
                .Where(b => b.BoardId.Equals(boardId));
            if (_boardId.Any())
            {
                return true;
            }
            else { return false; }
        }
        public bool ExistsStatusId(int statusId)
        {
            var _statusId = _context
                .BoardsStatus
                .Where(b => b.BoardId.Equals(statusId));
            if (_statusId.Any())
            {
                return true;
            }
            else { return false; }
        }
    }
}
