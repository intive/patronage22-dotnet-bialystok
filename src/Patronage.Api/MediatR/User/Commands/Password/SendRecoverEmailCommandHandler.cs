using MediatR;
using Patronage.Contracts.Interfaces;

namespace Patronage.Api.MediatR.User.Commands.Password
{
    public class SendRecoverEmailCommandHandler : IRequestHandler<SendRecoverEmailCommand, bool>
    {
        private readonly IUserService userService;

        public SendRecoverEmailCommandHandler(IUserService userService)
        {
            this.userService = userService;
        }

        public async Task<bool> Handle(SendRecoverEmailCommand request, CancellationToken cancellationToken)
        {
            return await userService.SendRecoveryPasswordEmailAsync(request.Id, request.Link);
        }
    }
}
