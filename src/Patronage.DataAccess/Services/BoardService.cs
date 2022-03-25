using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Patronage.Contracts.Interfaces;
using Patronage.Contracts.ModelDtos.Board;
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

        public async Task<IEnumerable<BoardDto>?> GetBoardsAsync(FilterBoardDto? filter = null)
        {
            var query = _tableContext.Boards.AsQueryable();
            if (filter is null)
            {
                if (!query.Any())
                    return null;

                return _mapper
                    .Map<IEnumerable<BoardDto>>(await query.ToArrayAsync());
            }
            var boards = await query
                .Where(x =>
                    x.Alias.Equals(filter.Alias ?? x.Alias) &&
                    x.Name.Equals(filter.Name ?? x.Name) &&
                    x.Description != null && x.Description.Equals(filter.Description ?? x.Description))
                .ToArrayAsync();

            return _mapper.Map<IEnumerable<BoardDto>>(boards);
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