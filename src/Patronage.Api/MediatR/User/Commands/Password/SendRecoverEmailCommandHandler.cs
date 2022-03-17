using MediatR;
using Patronage.Contracts.Interfaces;

namespace Patronage.Api.MediatR.User.Commands.Password
{
    public class SendRecoverEmailCommandHandler : IRequestHandler<SendRecoverEmailPasswordCommand, bool>
    {
        private readonly IUserService userService;

        public SendRecoverEmailCommandHandler(IUserService userService)
        {
            this.userService = userService;
        }

        public async Task<bool> Handle(SendRecoverEmailPasswordCommand request, CancellationToken cancellationToken)
        {
            return await userService.SendRecoveryPasswordEmailAsync(request.recoverPasswordDto, request.Link);
        }
    }
}