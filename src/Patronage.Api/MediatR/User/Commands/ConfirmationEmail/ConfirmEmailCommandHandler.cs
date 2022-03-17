using MediatR;
using Patronage.Contracts.Interfaces;

namespace Patronage.Api.MediatR.User.Commands.ConfirmationEmail
{
    public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, bool>
    {
        public IUserService userService { get; set; }

        public ConfirmEmailCommandHandler(IUserService userService)
        {
            this.userService = userService;
        }

        public Task<bool> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            return userService.ConfirmEmail(request.Id, request.Token);
        }
    }
}