using MediatR;

namespace Patronage.Api.MediatR.User.Commands.ConfirmationEmail
{
    public class ResendEmailCommand : IRequest<bool>
    {
        public string Email { get; set; } = null!;
        public string Link { get; set; } = null!;
    }
}
