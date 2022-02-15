using AutoMapper;
using Microsoft.EntityFrameworkCore;
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

        public async Task<BoardDto?> CreateBoardAsync(BoardDto request)
        {
            if (request is null)
                return null;

            var result = tableContext.Boards.FirstOrDefaultAsync(x => x.Alias.Equals(request.Alias) || x.Name.Equals(request.Name));

            if (result is not null)
                return null;

            var board = mapper.Map<Board>(request);
            tableContext.Boards.Add(board);

            if (await tableContext.SaveChangesAsync() > 0)
            {
                request.Id = board.Id;
                return request;
            }
            return null;
        }

        public async Task<bool> DeleteBoardAsync(int id)
        {
            var board = await tableContext.Boards.FirstOrDefaultAsync(x => x.Id == id);

            if (board is null)
                return false;

            board.IsActive = false;

            if ((await tableContext.SaveChangesAsync()) > 0)
            {
                return true;
            }

            return false;
        }

        public async Task<BoardDto?> GetBoardByIdAsync(int id)
        {
            var board = await tableContext.Boards.FirstOrDefaultAsync(x => x.Id == id);

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
                    x.Description.Equals(filter.Description ?? x.Description))
                .ToArrayAsync();

            return mapper.Map<IEnumerable<BoardDto>>(boards);
        }

        public async Task<bool> UpdateBoardAsync(BoardDto request)
        {
            var result = tableContext.Boards
                .FirstOrDefaultAsync(x => x.Alias.Equals(request.Alias) || x.Name.Equals(request.Name));

            if (result is not null)
                return false;

            var board = await tableContext.Boards.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (board is null)
                return false;

            mapper.Map(request, board);

            if ((await tableContext.SaveChangesAsync()) > 0)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> UpdateBoardLightAsync(PartialBoardDto request)
        {
            if (request is null)
                return false;

            var result = tableContext.Boards
                .FirstOrDefaultAsync(x => x.Alias.Equals(request.Alias.Data) || x.Name.Equals(request.Name.Data));

            if (result is not null)
                return false;

            var board = await tableContext.Boards.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (board is null)
                return false;

            mapper.Map(request, board);

            if ((await tableContext.SaveChangesAsync()) > 0)
            {
                return true;
            }

            return false;
        }
    }
}
