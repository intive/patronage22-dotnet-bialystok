﻿using MediatR;
using Patronage.Contracts.Interfaces;

namespace Patronage.Api.MediatR.Comment.Commands
{
    public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand, bool>
    {
        private readonly ICommentService _commentService;

        public DeleteCommentCommandHandler(ICommentService commentService)
        {
            _commentService = commentService;
        }

        public async Task<bool> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
        {
            return await _commentService.DeleteAsync(request.Id);
        }
    }
}
