using MediatR;
using Patronage.Contracts.Interfaces;

namespace Patronage.Api.MediatR.User.Commands.SignIn
{
    public class SignInHandler : IRequestHandler<SignInCommand, string?>
    {
        private readonly IUserService _userService;

        public SignInHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<string?> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            return await _userService.LoginUserAsync(request.dto);
        }
    }
}
