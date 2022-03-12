using FluentValidation;
using Patronage.Contracts.ModelDtos.Comments;
using Patronage.Models;

namespace Patronage.Api.Validators.Comments
{
    public class CommentDtoValidator : AbstractValidator<BaseCommentDto>
    {
        public CommentDtoValidator(TableContext dbContext)
        {
            RuleFor(x => x.Content)
                .NotNull()
                .NotEmpty()
                .MaximumLength(500).WithMessage("Can not exceed 500 characters.");


            RuleFor(x => x.IssueId)
                .NotNull()
                .NotEmpty()
                .GreaterThanOrEqualTo(1);


            RuleFor(x => x.ApplicationUserId)
                .NotNull()
                .NotEmpty();
        }
    }
}
