using MediatR;
using Patronage.Contracts.Interfaces;

namespace Patronage.Api.MediatR.Issues.Commands
{
    public class UpdateLightCommentCommandHandler : IRequestHandler<UpdateLightCommentCommand, bool>
    {
        private readonly ICommentService _commentService;

        public UpdateLightCommentCommandHandler(ICommentService commentService)
        {
            _commentService = commentService;
        }

        public async Task<bool> Handle(UpdateLightCommentCommand request, CancellationToken cancellationToken)
        {
            return await _commentService.UpdateLightAsync(request.Id, request.Dto);
        }
    }
}