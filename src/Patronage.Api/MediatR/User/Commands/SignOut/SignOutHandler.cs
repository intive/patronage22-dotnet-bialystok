using MediatR;
using Patronage.Contracts.Interfaces;

namespace Patronage.Api.MediatR.User.Commands.SignOut
{
    public class SignOutHandler : IRequestHandler<SignOutCommand, bool>
    {
        private readonly IUserService _userService;

        public SignOutHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<bool> Handle(SignOutCommand request, CancellationToken cancellationToken)
        {
            var isTokenActive = await _userService.LogOutUserAsync(request.accessToken);
            return isTokenActive;
        }
    }
}
