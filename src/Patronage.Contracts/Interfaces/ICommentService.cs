using Patronage.Contracts.Helpers;
using Patronage.Contracts.ModelDtos.IssuesComments;

namespace Patronage.Contracts.Interfaces
{
    public interface ICommentService
    {
        Task<PageResult<BaseCommentDto>?> GetAllCommentFromIssue(int issueId);
        Task<CommentDto?> CreateAsync(BaseCommentDto issue);
        Task<bool> UpdateLightAsync(int issueId, int commentId, PartialCommentDto dto);
    }
}
