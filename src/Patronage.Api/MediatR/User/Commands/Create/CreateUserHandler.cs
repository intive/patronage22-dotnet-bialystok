using MediatR;
using Patronage.Contracts.Interfaces;
using Patronage.Contracts.ModelDtos.User;

namespace Patronage.Api.MediatR.User.Commands.Create
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, UserDto>
    {
        public IUserService userService { get; set; }

        public CreateUserHandler(IUserService userService)
        {
            this.userService = userService;
        }

        public Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            return userService.CreateUserAsync(request.CreateUserDto, request.Link);
        }
    }
}
