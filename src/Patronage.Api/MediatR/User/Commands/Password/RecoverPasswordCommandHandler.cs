using MediatR;
using Patronage.Contracts.Interfaces;

namespace Patronage.Api.MediatR.User.Commands.Password
{
    public class RecoverPasswordCommandHandler : IRequestHandler<RecoverPasswordCommand, bool>
    {
        private readonly IUserService userService;

        public RecoverPasswordCommandHandler(IUserService userService)
        {
            this.userService = userService;
        }

        public async Task<bool> Handle(RecoverPasswordCommand request, CancellationToken cancellationToken)
        {
            return await userService.RecoverPasswordAsync(request.NewUserPassword);
        }
    }
}
