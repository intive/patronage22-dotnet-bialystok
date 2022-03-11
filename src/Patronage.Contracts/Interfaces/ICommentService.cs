using Patronage.Contracts.Helpers;
using Patronage.Contracts.ModelDtos.Comments;
using Patronage.Contracts.ModelDtos.IssuesComments;
using Patronage.Models;

namespace Patronage.Contracts.Interfaces
{
    public interface ICommentService : IEntityService<Comment>
    {
        Task<PageResult<CommentDto>?> GetAllCommentFromIssue(FilterCommentDto filter);
        Task<CommentDto?> CreateAsync(BaseCommentDto dto);
        Task<bool> UpdateLightAsync(int commentId, PartialCommentDto dto);
        Task<bool> DeleteAsync(int commentId);
    }
}
