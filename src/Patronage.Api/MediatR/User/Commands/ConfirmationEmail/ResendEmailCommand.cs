using MediatR;

namespace Patronage.Api.MediatR.User.Commands.ConfirmationEmail
{
    public class ResendEmailCommand : IRequest<bool>
    {
        public string Id { get; set; } = null!;
        public string Link { get; set; } = null!;
    }
}
