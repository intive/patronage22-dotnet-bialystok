using MediatR;
using Microsoft.AspNetCore.Mvc;
using Patronage.Api.MediatR.Comment.Commands;
using Patronage.Api.MediatR.Comment.Queries;
using Patronage.Contracts.Helpers;
using Patronage.Contracts.ModelDtos.Comments;
using Patronage.DataAccess;

namespace Patronage.Api.Controllers
{
    [Route("api/issue/comments")]
    [ApiController]
    public class IssuesCommentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public IssuesCommentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Returns all Comments by Issue Id.
        /// You need to add the PageSize and PageNumber.
        /// </summary>
        /// <response code="200">Searched issues.</response>
        /// <response code="404">Issues not found.</response>
        [HttpGet]
        public async Task<ActionResult<PageResult<CommentDto>>> GetAllCommentFromIssue([FromQuery] FilterCommentDto filter)
        {
            var result = await _mediator.Send(new GetCommentsListQuery(filter));
            if (result is null)
            {
                return NotFound(new BaseResponse<PageResult<CommentDto>>
                {
                    ResponseCode = StatusCodes.Status404NotFound,
                    Message = $"There's no comments for issue id: {filter.IssueId}"
                });
            }

            return Ok(new BaseResponse<PageResult<CommentDto>>
            {
                ResponseCode = StatusCodes.Status200OK,
                Data = result
            });
        }

        /// <summary>
        /// Creates Comment for Issue.
        /// </summary>
        /// <response code="201">Comment correctly created.</response>
        /// <response code="400">Please insert correct JSON object with parameters.</response>
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateCommentCommand command)
        {
            var result = await _mediator.Send(command);
            if (result is null)
            {
                return BadRequest(new BaseResponse<CommentDto>
                {
                    ResponseCode = StatusCodes.Status400BadRequest,
                });
            }

            return Ok(new BaseResponse<CommentDto>
            {
                Message = $"Comment was created successfully for issue id: {command.Data.IssueId}",
                ResponseCode = StatusCodes.Status201Created,
                Data = result
            });
        }

        /// <summary>
        /// Updates comment - only content.
        /// </summary>
        /// <response code="200">Comment correctly updated.</response>
        /// <response code="400">Please insert correct JSON object with parameters.</response>
        /// <response code="404">Comment not found.</response>
        [HttpPatch("{commentId}")]
        public async Task<ActionResult> UpdateLight([FromBody] PartialCommentDto dto, [FromRoute] int commentId)
        {
            var result = await _mediator.Send(new UpdateLightCommentCommand(commentId, dto));
            if (!result)
            {
                return NotFound(new BaseResponse<bool>
                {
                    ResponseCode = StatusCodes.Status404NotFound,
                    Message = $"There's no comment with Id: {commentId}"
                });
            }

            return Ok(new BaseResponse<bool>
            {
                Message = "Comment was updated successfully",
                ResponseCode = StatusCodes.Status200OK,
                Data = result
            });
        }

        /// <summary>
        /// Deletes Comment.
        /// </summary>
        /// <response code="200">Comment correctly deleted.</response>
        /// <response code="404">Comment not found.</response>
        [HttpDelete("{commentId}")]
        public async Task<ActionResult> Delete([FromRoute] int commentId)
        {
            var result = await _mediator.Send(new DeleteCommentCommand(commentId));
            if (!result)
            {
                return NotFound(new BaseResponse<bool>
                {
                    ResponseCode = StatusCodes.Status404NotFound,
                    Message = $"There's no comment with Id: {commentId}"
                });
            }

            return Ok(new BaseResponse<bool>
            {
                Message = "Comment was deleted successfully",
                ResponseCode = StatusCodes.Status200OK,
                Data = result
            });
        }
    }
}
