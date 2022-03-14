using MediatR;
using Patronage.Contracts.Interfaces;

namespace Patronage.Api.MediatR.User.Commands.ConfirmationEmail
{
    public class ResendEmailCommandHandler : IRequestHandler<ResendEmailCommand, bool>
    {
        public IUserService userService { get; set; }

        public ResendEmailCommandHandler(IUserService userService)
        {
            this.userService = userService;
        }

        public Task<bool> Handle(ResendEmailCommand request, CancellationToken cancellationToken)
        {
            return userService.ResendEmailConfirmationAsync(request.Email, request.Link);
        }
    }
}
