using MediatR;
using Patronage.Contracts.Interfaces;
using Patronage.Contracts.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patronage.Api.MediatR.User.Commands.RefreshToken
{
    public class RefreshTokenHandler : IRequestHandler<RefreshTokenCommand, RefreshTokenResponse?>
    {
        private readonly IUserService _userService;

        public RefreshTokenHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<RefreshTokenResponse?> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            return await _userService.RefreshTokenAsync(request.refreshToken, request.accessToken);
        }
    }
}
