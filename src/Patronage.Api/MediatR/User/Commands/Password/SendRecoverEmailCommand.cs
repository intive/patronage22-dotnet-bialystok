using MediatR;

namespace Patronage.Api.MediatR.User.Commands.Password
{
    public class SendRecoverEmailCommand : IRequest<bool>
    {
        public string Id { get; set; } = null!;
        public string Link { get; set; } = null!;
    }
}
