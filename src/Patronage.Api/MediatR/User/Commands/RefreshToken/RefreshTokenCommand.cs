using MediatR;
using Patronage.Contracts.ResponseModels;

namespace Patronage.Api.MediatR.User.Commands.RefreshToken
{
    public record RefreshTokenCommand(string refreshToken, string accessToken) : IRequest<RefreshTokenResponse?>;
}
