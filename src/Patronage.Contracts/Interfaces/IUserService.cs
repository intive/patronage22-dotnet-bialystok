﻿using Patronage.Contracts.ModelDtos.User;
using Patronage.Contracts.ResponseModels;

namespace Patronage.Contracts.Interfaces
{
    public interface IUserService
    {
        Task<bool> ResendEmailConfirmationAsync(string email, string link);

        Task<UserDto> CreateUserAsync(CreateUserDto createUser, string link);

        Task<bool> ConfirmEmail(string id, string token);

        Task<bool> SendRecoveryPasswordEmailAsync(RecoverPasswordDto recoverPasswordDto, string link);

        Task<bool> RecoverPasswordAsync(NewUserPasswordDto userPasswordDto);

        Task<RefreshTokenResponse?> SignInUserAsync(SignInDto signInDto);

        Task SignOutUserAsync(string accessToken);

        Task<RefreshTokenResponse?> RefreshTokenAsync(string refreshToken, string accessToken);

        Task<IEnumerable<UserDto>> GetAllUsersAsync(string? searchedPhrase);
    }
}