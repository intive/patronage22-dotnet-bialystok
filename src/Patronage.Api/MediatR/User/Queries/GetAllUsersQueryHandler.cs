using MediatR;
using Patronage.Contracts.Interfaces;
using Patronage.Contracts.ModelDtos.User;

namespace Patronage.Api.MediatR.User.Queries
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<UserDto>>
    {
        private readonly IUserService _userService;

        public GetAllUsersQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IEnumerable<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            return await _userService.GetAllUsersAsync(request.SearchedPhrase);
        }
    }
}
