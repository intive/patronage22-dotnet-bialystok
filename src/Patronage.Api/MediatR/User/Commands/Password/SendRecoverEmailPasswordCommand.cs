using MediatR;
using Patronage.Contracts.ModelDtos.User;

namespace Patronage.Api.MediatR.User.Commands.Password
{
    public class SendRecoverEmailPasswordCommand : IRequest<bool>
    {
        public RecoverPasswordDto recoverPasswordDto { get; set; } = null!;
        public string Link { get; set; } = null!;
    }
}