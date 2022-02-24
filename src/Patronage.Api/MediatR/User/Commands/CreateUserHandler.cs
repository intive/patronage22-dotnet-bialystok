using MediatR;
using Patronage.Contracts.ModelDtos.User;

namespace Patronage.Api.MediatR.User.Commands
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, UserDto>
    {
        public Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
