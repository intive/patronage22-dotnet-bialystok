using MediatR;
using Patronage.Contracts.ModelDtos.User;
using Patronage.Contracts.ResponseModels;

namespace Patronage.Api.MediatR.User.Commands.SignIn
{
    public record SignInCommand(SignInDto dto) : IRequest<RefreshTokenResponse?>;
}