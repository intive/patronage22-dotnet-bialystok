using MediatR;

namespace Patronage.Api.MediatR.User.Commands.SignOut
{
    public record SignOutCommand(string accessToken) : IRequest<bool>;
}