using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Patronage.Contracts.Helpers;
using Patronage.Contracts.Interfaces;
using Patronage.Contracts.ModelDtos.Boards;
using Patronage.Models;

namespace Patronage.DataAccess.Services
{
    public class BoardService : IBoardService
    {
        public readonly TableContext _tableContext;
        public readonly IMapper _mapper;

        public BoardService(TableContext tableContext, IMapper mapper)
        {
            _tableContext = tableContext ?? throw new ArgumentNullException(nameof(tableContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<BoardDto?> CreateBoardAsync(BoardDto request)
        {
            var board = _mapper.Map<Board>(request);
            _tableContext.Boards.Add(board);

            if (await _tableContext.SaveChangesAsync() > 0)
            {
                request.Id = board.Id;
                return request;
            }

            throw new DbUpdateException($"Could not save changes to database at: {nameof(CreateBoardAsync)}");
        }

        public async Task<bool> DeleteBoardAsync(int id)
        {
            var board = await GetByIdAsync(id);

            if (board is null)
                return false;

            if (!board.IsActive)
            {
                return true;
            }

            board.IsActive = false;

            if ((await _tableContext.SaveChangesAsync()) > 0)
            {
                return true;
            }

            throw new DbUpdateException($"Could not save changes to database at: {nameof(DeleteBoardAsync)}");
        }

        public async Task<BoardDto?> GetBoardByIdAsync(int id)
        {
            var board = await GetByIdAsync(id);

            if (board is null)
                return null;

            return _mapper.Map<BoardDto>(board);
        }

        public async Task<PageResult<BoardDto>?> GetBoardsAsync(FilterBoardDto? filter)
        {
            var baseQuery = _tableContext
                .Boards
                .Where(x => x.IsActive == true)
                .AsQueryable();

            if (!baseQuery.Any())
            {
                return null;
            }

            if (filter is not null)
            {
                baseQuery = baseQuery
                    .FilterBy(filter);
            }
            var totalItemCount = baseQuery.Count();

            var boards = baseQuery
                .Skip(filter!.PageSize * (filter.PageNumber - 1))
                .Take(filter.PageSize);

            var items = await boards.Select(x => new BoardDto(x)).ToListAsync();

            return new PageResult<BoardDto>(items, totalItemCount, filter.PageSize, filter.PageNumber);
        }

        public async Task<Board?> GetByIdAsync(int id)
        {
            return await _tableContext.Boards.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> UpdateBoardAsync(UpdateBoardDto request, int id)
        {
            var board = await GetByIdAsync(id);

            if (board is null)
                return false;

            _mapper.Map(request, board);

            if ((await _tableContext.SaveChangesAsync()) > 0)
            {
                return true;
            }

            throw new DbUpdateException($"Could not save changes to database at: {nameof(UpdateBoardAsync)}");
        }

        public async Task<bool> UpdateBoardLightAsync(PartialBoardDto request, int id)
        {
            var board = await GetByIdAsync(id);

            if (board is null)
                return false;

            _mapper.Map(request, board);

            if ((await _tableContext.SaveChangesAsync()) > 0)
            {
                return true;
            }

            throw new DbUpdateException($"Could not save changes to database at: {nameof(UpdateBoardLightAsync)}");
        }
    }
}