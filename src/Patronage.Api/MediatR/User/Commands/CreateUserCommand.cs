using MediatR;
using Patronage.Contracts.ModelDtos.User;

namespace Patronage.Api.MediatR.User.Commands
{
    public class CreateUserCommand : IRequest<UserDto>
    {
        public CreateUserDto CreateUserDto { get; set; }
    }
}
