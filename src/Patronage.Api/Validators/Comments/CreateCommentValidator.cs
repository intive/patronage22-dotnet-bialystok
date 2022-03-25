using FluentValidation;
using Patronage.Api.MediatR.Comment.Commands;

namespace Patronage.Api.Validators.Comments
{
    public class CreateCommentValidator : AbstractValidator<CreateCommentCommand>
    {
        public CreateCommentValidator(CommentDtoValidator commentValidator)
        {
            RuleFor(x => x.Data).SetValidator(commentValidator);
        }
    }
}