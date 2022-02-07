using AutoMapper;
using Patronage.Contracts.Interfaces;
using Patronage.Contracts.ModelDtos;
using Patronage.Models;

namespace Patronage.DataAccess.Services
{
    public class BoardService : IBoardService
    {
        public readonly TableContext tableContext;
        public readonly IMapper mapper;

        public BoardService(TableContext tableContext, IMapper mapper)
        {
            this.tableContext = tableContext ?? throw new ArgumentNullException(nameof(tableContext));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public bool CreateBoard(BoardDto request)
        {
            if (request is null)
                return false;

            var board = mapper.Map<Board>(request);
            board.OnCreate();
            board.IsActive = true;
            tableContext.Boards.Add(board);

            if(tableContext.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }

        public BoardDto GetBoardById(int id)
        {
            var board = tableContext.Boards.FirstOrDefault(x => x.Id == id);

            return mapper.Map<BoardDto>(board);
        }

        public IEnumerable<BoardDto> SearchBoard(BoardDto filter)
        {
            if (filter is null)
            {
                return mapper
                    .Map<IEnumerable<BoardDto>>(tableContext.Boards.AsEnumerable());
            }
                       
            var boards = tableContext.Boards
                .Where(x =>
                    x.Alias.Equals(filter.Alias ?? x.Alias) &&
                    x.Name.Equals(filter.Name ?? x.Name) &&
                    x.Description.Equals(filter.Description ?? x.Description) &&
                    DateTime.Compare(x.CreatedOn, filter.CreatedOn == DateTime.MinValue ? x.CreatedOn : filter.CreatedOn) == 0 &&
                    DateTime.Compare(x.ModifiedOn ?? DateTime.MinValue, filter.ModifiedOn == DateTime.MinValue ? (x.ModifiedOn ?? DateTime.MinValue) : filter.ModifiedOn) == 0)
                .ToArray();

            return mapper.Map<IEnumerable<BoardDto>>(boards);
        }

        public bool UpdateBoard(BoardDto request)
        {
            var board = tableContext.Boards.FirstOrDefault(x => x.Id == request.Id);

            mapper.Map(request, board);

            if(tableContext.SaveChanges() > 0)
            {
                return true;
            }

            return false;
        }
    }
}
