using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Patronage.Contracts.Interfaces;
using Patronage.Contracts.ModelDtos.Board;
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

        public async Task<BoardDto?> CreateBoardAsync(BoardDto request)
        {
            var board = mapper.Map<Board>(request);
            tableContext.Boards.Add(board);

            if (await tableContext.SaveChangesAsync() > 0)
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

            if ((await tableContext.SaveChangesAsync()) > 0)
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

            return mapper.Map<BoardDto>(board);
        }

        public async Task<IEnumerable<BoardDto>?> GetBoardsAsync(FilterBoardDto? filter = null)
        {
            var query = tableContext.Boards.AsQueryable();
            if (filter is null)
            {
                if (!query.Any())
                    return null;

                return mapper
                    .Map<IEnumerable<BoardDto>>(await query.ToArrayAsync());
            }
            var boards = await query
                .Where(x =>
                    x.Alias.Equals(filter.Alias ?? x.Alias) &&
                    x.Name.Equals(filter.Name ?? x.Name) &&
                    x.Description != null && x.Description.Equals(filter.Description ?? x.Description))
                .ToArrayAsync();

            return mapper.Map<IEnumerable<BoardDto>>(boards);
        }

        public async Task<Board?> GetByIdAsync(int id)
        {
            return await tableContext.Boards.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> UpdateBoardAsync(UpdateBoardDto request, int id)
        {
            var board = await GetByIdAsync(id);

            if (board is null)
                return false;

            mapper.Map(request, board);

            if ((await tableContext.SaveChangesAsync()) > 0)
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

            mapper.Map(request, board);

            if ((await tableContext.SaveChangesAsync()) > 0)
            {
                return true;
            }

            throw new DbUpdateException($"Could not save changes to database at: {nameof(UpdateBoardLightAsync)}");
        }
    }
}