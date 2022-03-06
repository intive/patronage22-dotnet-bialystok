using Patronage.Contracts.ModelDtos.User;

namespace Patronage.Contracts.Interfaces
{
    public interface IUserService
    {
        Task<bool> ResendEmailConfirmationAsync(string id, string link);
        Task<UserDto> CreateUserAsync(CreateUserDto createUser, string link);
        Task<bool> ConfirmEmail(string id, string token);
        Task<bool> SendRecoveryPasswordEmailAsync(string id, string link);
        Task<bool> RecoverPasswordAsync(NewUserPasswordDto userPasswordDto);
        Task<string?> LoginUserAsync(SignInDto signInDto);
        Task LogOutUserAsync();
    }
}
