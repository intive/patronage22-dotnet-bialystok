using MediatR;

namespace Patronage.Api.MediatR.User.Commands.ConfirmationEmail
{
    public class ConfirmEmailCommand : IRequest<bool>
    {
        public string Id { get; set; } = null!;
        public string Token { get; set; } = null!;
    }
}
