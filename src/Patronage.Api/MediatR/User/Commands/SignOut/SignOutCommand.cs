using MediatR;
using Patronage.Contracts.ModelDtos.User;

namespace Patronage.Api.MediatR.User.Commands.SignOut
{
    public record SignOutCommand(string accessToken) : IRequest;
}