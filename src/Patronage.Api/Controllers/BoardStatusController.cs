using MediatR;
using Microsoft.AspNetCore.Mvc;
using Patronage.Api.MediatR.BoardStatus.Commands;
using Patronage.Api.MediatR.BoardStatus.Queries;
using Patronage.Contracts.Interfaces;
using Patronage.Contracts.ModelDtos;
using Swashbuckle.AspNetCore.Annotations;

namespace Patronage.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoardStatusController : ControllerBase
    {
        private readonly IBoardStatusService _boardStatusService;
        private readonly IMediator _mediator;

        public BoardStatusController(IBoardStatusService boardStatusService, IMediator mediator )
        {
            _boardStatusService = boardStatusService;
            _mediator = mediator;
        }


        [SwaggerOperation(Summary = "get all StatusBoards")]
        [HttpGet]
        public async Task<IEnumerable<BoardStatusDto>> GetAll()
        {
            return await _mediator.Send(new GetAllBoardStatusQuery());
        }


        [SwaggerOperation(Summary = "get StatusBoard by boardId, statusId")]
        [HttpGet("board")]
        public ActionResult<IEnumerable<BoardStatusDto>> GetById([FromQuery] int boardId,[FromQuery] int statusId)
        {
            var boards = _boardStatusService.GetById(boardId, statusId);
            return Ok(boards);

        }

        //[SwaggerOperation(Summary = "get StatusBoards for boardid")]
        //[HttpGet]


        [SwaggerOperation(Summary = "create StatusBoard based on statusboardDto passed as parameter in request body")]
        [HttpPost]
        public async Task<BoardStatusDto> Create([FromBody] BoardStatusDto dto)
        {
            //int id = _boardStatusService.Create(dto);
            //return Created($"{id}", null);
            return await _mediator.Send(new CreateBoardStatusCommand(dto));
        }


        [SwaggerOperation(Summary = "delete statusboard by id")]
        [HttpDelete]
        public void Delete([FromQuery] int boardId, [FromQuery] int statusId)
        {
            _boardStatusService.Delete(boardId, statusId);

        }
    }
}
