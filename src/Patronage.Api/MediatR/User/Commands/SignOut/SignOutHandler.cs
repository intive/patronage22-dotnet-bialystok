using MediatR;
using Patronage.Contracts.Interfaces;

namespace Patronage.Api.MediatR.User.Commands.SignOut
{
    public class SignOutHandler : IRequestHandler<SignOutCommand>
    {
        private readonly IUserService _userService;

        public SignOutHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<Unit> Handle(SignOutCommand request, CancellationToken cancellationToken)
        {
            await _userService.SignOutUserAsync(request.accessToken);
            return Unit.Value;
        }
    }
}