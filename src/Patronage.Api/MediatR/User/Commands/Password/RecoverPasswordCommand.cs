using MediatR;
using Patronage.Contracts.ModelDtos.User;

namespace Patronage.Api.MediatR.User.Commands.Password
{
    public class RecoverPasswordCommand : IRequest<bool>
    {
        public NewUserPasswordDto NewUserPassword { get; set; } = null!;
    }
}