using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NETCore.MailKit.Core;
using Patronage.Contracts.Interfaces;
using Patronage.Contracts.ModelDtos.User;
using Patronage.Contracts.ResponseModels;
using Patronage.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Web;

namespace Patronage.DataAccess.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly TableContext _tableContext;
        private readonly IEmailService _emailService;
        private readonly ILogger<UserService> _logger;
        private readonly ITokenService _tokenService;

        public UserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            TableContext tableContext, IEmailService emailService, ILogger<UserService> logger, ITokenService tokenService)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _tableContext = tableContext ?? throw new ArgumentNullException(nameof(tableContext));
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
            _logger = logger ?? throw new ArgumentNullException(nameof(_logger));
            _tokenService = tokenService;
        }

        public async Task<bool> ResendEmailConfirmationAsync(string email, string link)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return false;
            }

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            var uriBuilder = new UriBuilder(link);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["id"] = user.Id;
            query["token"] = token;
            uriBuilder.Query = query.ToString();

            link = uriBuilder.ToString();

            await _emailService.SendAsync(user.Email, "Confirm your email", link);

            return true;
        }

        public async Task<bool> ConfirmEmail(string id, string token)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return false;

            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (!result.Succeeded)
            {
                if (result.Errors.Count() > 1)
                {
                    throw new AggregateException("Multiple errors occured during confirming email.",
                        result.Errors.Select(x => new Exception(x.Description)).ToList());
                }
                throw new Exception("Error occured during Email confirmation: " + result.Errors.First().Description);
            }

            return true;
        }

        public async Task<UserDto> CreateUserAsync(CreateUserDto createUser, string link)
        {
            var user = new ApplicationUser
            {
                UserName = createUser.UserName,
                Email = createUser.Email,
            };

            using (var transaction = _tableContext.Database.BeginTransaction())
            {
                var result = await _userManager.CreateAsync(user, createUser.Password);

                if (!result.Succeeded)
                {
                    if (result.Errors.Count() > 1)
                    {
                        throw new AggregateException("Multiple errors occured while creating user.",
                            result.Errors.Select(x => new Exception(x.Description)).ToList());
                    }
                    throw new Exception("Error occured while creating user: " + result.Errors.First().Description);
                }

                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                var uriBuilder = new UriBuilder(link);
                var query = HttpUtility.ParseQueryString(uriBuilder.Query);
                query["id"] = user.Id;
                query["token"] = token;
                uriBuilder.Query = query.ToString();

                link = uriBuilder.ToString();

                await _emailService.SendAsync(user.Email, "Confirm your email", link);

                await transaction.CommitAsync();

                return new UserDto
                {
                    Id = user.Id,
                    Email = user.Email,
                    UserName = user.UserName
                };
            }
        }

        public async Task<bool> SendRecoveryPasswordEmailAsync(RecoverPasswordDto recoverPasswordDto, string link)
        {
            var user = await (recoverPasswordDto.Username == null ?
                _userManager.FindByEmailAsync(recoverPasswordDto.Email!.Data) :
                _userManager.FindByNameAsync(recoverPasswordDto.Username!.Data));

            if (user == null)
            {
                return false;
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var uriBuilder = new UriBuilder(link);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["id"] = user.Id;
            query["token"] = token;
            uriBuilder.Query = query.ToString();

            link = uriBuilder.ToString();

            await _emailService.SendAsync(user.Email, "Recover your password", link);

            return true;
        }

        public async Task<bool> RecoverPasswordAsync(NewUserPasswordDto userPasswordDto)
        {
            var user = await _userManager.FindByIdAsync(userPasswordDto.Id);

            if (user == null)
            {
                return false;
            }

            var result = await _userManager.ResetPasswordAsync(user, userPasswordDto.Token, userPasswordDto.Password);

            if (!result.Succeeded)
            {
                if (result.Errors.Count() > 1)
                {
                    throw new AggregateException("Multiple errors occured while reseting password.",
                        result.Errors.Select(x => new Exception(x.Description)).ToList());
                }
                throw new Exception("Error occured while reseting password." + result.Errors.First().Description);
            }

            return true;
        }

        public async Task<RefreshTokenResponse?> SignInUserAsync(SignInDto signInDto)
        {
            var user = await _userManager.FindByNameAsync(signInDto.Username);

            if (user is null)
            {
                _logger.LogDebug("Wrong username");
                return null;
            }

            var signInResult = await _signInManager.PasswordSignInAsync(user, signInDto.Password, false, false);

            if (!signInResult.Succeeded)
            {
                _logger.LogDebug("Wrong password");
                return null;
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var accessToken = _tokenService.GenerateAccessToken(claims);
            var newRefreshToken = _tokenService.GenerateRefreshToken();
            var userRefreshTokenRecord = _tableContext.UserTokens.FirstOrDefault(u => u.UserId == user.Id);
            if (userRefreshTokenRecord is null)
            {
                _tableContext.UserTokens.Add(new TokenUser
                {
                    UserId = user.Id,
                    LoginProvider = "Own",
                    Name = "RefreshToken",
                    Value = newRefreshToken.Token,
                    ValidUntil = newRefreshToken.ValidUntil,
                });
            }
            else
            {
                userRefreshTokenRecord.Value = newRefreshToken.Token;
                userRefreshTokenRecord.ValidUntil = newRefreshToken.ValidUntil;
            }
            await _tableContext.SaveChangesAsync();
            var response = new RefreshTokenResponse
            {
                RefreshToken = newRefreshToken,
                AccessToken = accessToken
            };

            return response;
        }

        public async Task SignOutUserAsync(string accessToken)
        {
            await _signInManager.SignOutAsync();

            var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);

            var userId = principal.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value;

            _logger.LogDebug("Clearing Refresh Token from DB");
            var userRefreshTokenRecord = _tableContext.UserTokens.Single(u => u.UserId == userId);

            userRefreshTokenRecord.Value = null;
            userRefreshTokenRecord.ValidUntil = DateTime.UtcNow;

            await _tableContext.SaveChangesAsync();
        }

        public async Task<RefreshTokenResponse?> RefreshTokenAsync(
            string refreshToken,
            string accessToken)
        {
            var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);

            var userRefreshTokenRecord = _tableContext.UserTokens.FirstOrDefault(u => u.Value == refreshToken);

            if (userRefreshTokenRecord == null)
            {
                _logger.LogDebug($"This Refresh token {refreshToken} does not exist");
                return null;
            }
            var user = _tableContext.Users.FirstOrDefault(u => u.Id == userRefreshTokenRecord.UserId);
            if (user == null)
            {
                _logger.LogDebug($"There is no user with this refresh token");
                return null;
            }
            if (!userRefreshTokenRecord.IsActive)
            {
                _logger.LogDebug("Refresh Token has expired");
                return null;
            }
            var newAccessToken = _tokenService.GenerateAccessToken(principal.Claims);
            var newRefreshToken = _tokenService.GenerateRefreshToken();

            userRefreshTokenRecord.Value = newRefreshToken.Token;
            userRefreshTokenRecord.ValidUntil = newRefreshToken.ValidUntil;

            await _tableContext.SaveChangesAsync();

            var response = new RefreshTokenResponse
            {
                RefreshToken = newRefreshToken,
                AccessToken = newAccessToken
            };

            return response;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync(string? searchedPhrase)
        {
            var usersQueryable = _tableContext
                .Users
                .AsQueryable();

            if (searchedPhrase is not null)
            {
                usersQueryable = usersQueryable.Where(u => (u.FirstName!.Contains(searchedPhrase)) ||
                                                           (u.SecondName!.Contains(searchedPhrase)) ||
                                                           u.UserName.Contains(searchedPhrase) ||
                                                           u.Email.Contains(searchedPhrase));
            }

            var users = await usersQueryable.Select(user => new UserDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
            })
                .ToListAsync();

            return users;
        }
    }
}