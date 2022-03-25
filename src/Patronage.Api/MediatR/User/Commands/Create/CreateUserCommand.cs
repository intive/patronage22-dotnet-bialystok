using MediatR;
using Patronage.Contracts.ModelDtos.User;

namespace Patronage.Api.MediatR.User.Commands.Create
{
    public class CreateUserCommand : IRequest<UserDto>
    {
        public CreateUserDto CreateUserDto { get; set; } = null!;
        public string Link { get; set; } = null!;
    }
}