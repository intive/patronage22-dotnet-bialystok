using MediatR;
using Patronage.Contracts.ModelDtos.User;

namespace Patronage.Api.MediatR.User.Commands.SignIn
{
    public record SignInCommand(SignInDto dto) : IRequest<string?>;
}
