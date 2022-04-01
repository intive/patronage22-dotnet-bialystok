using MediatR;
using Patronage.Contracts.ModelDtos.User;

namespace Patronage.Api.MediatR.User.Queries
{
    public record GetAllUsersQuery(string? SearchedPhrase) : IRequest<IEnumerable<UserDto>>;
}
