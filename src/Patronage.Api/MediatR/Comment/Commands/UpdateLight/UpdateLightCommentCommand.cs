using MediatR;
using Patronage.Contracts.ModelDtos.Comments;

namespace Patronage.Api.MediatR.Comment.Commands
{
    public class UpdateLightCommentCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public PartialCommentDto Dto { get; set; }

        public UpdateLightCommentCommand(int id, PartialCommentDto dto)
        {
            Id = id;
            Dto = dto;
        }
    }
}