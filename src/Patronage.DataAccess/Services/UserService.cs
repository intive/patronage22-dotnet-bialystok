using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using NETCore.MailKit.Core;
using Patronage.Contracts.Interfaces;
using Patronage.Contracts.ModelDtos.User;
using Patronage.Contracts.ResponseModels;
using Patronage.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Patronage.DataAccess.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly TableContext tableContext;
        private readonly IEmailService emailService;
        private readonly ILoggerFactory logger;
        private readonly ITokenService _tokenService;

        public UserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            TableContext tableContext, IEmailService emailService, ILoggerFactory logger, ITokenService tokenService)
        {
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            this.signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            this.tableContext = tableContext ?? throw new ArgumentNullException(nameof(tableContext));
            this.emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _tokenService = tokenService;
        }






        public async Task<bool> ResendEmailConfirmationAsync(string id, string link)
        {
            var user = await userManager.FindByIdAsync(id);

            if (user == null)
            {
                return false;
            }

            var token = await userManager.GenerateEmailConfirmationTokenAsync(user);

            var uriBuilder = new UriBuilder(link);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["id"] = user.Id;
            query["token"] = token;
            uriBuilder.Query = query.ToString();

            link = uriBuilder.ToString();

            await emailService.SendAsync(user.Email, "Confirm your email", link);

            return true;
        }

        public async Task<bool> ConfirmEmail(string id, string token)
        {
            var user = await userManager.FindByIdAsync(id);

            if (user == null)
                return false;

            var result = await userManager.ConfirmEmailAsync(user, token);

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

            using (var transaction = tableContext.Database.BeginTransaction())
            {
                var result = await userManager.CreateAsync(user, createUser.Password);

                if (!result.Succeeded)
                {
                    if (result.Errors.Count() > 1)
                    {
                        throw new AggregateException("Multiple errors occured while creating user.",
                            result.Errors.Select(x => new Exception(x.Description)).ToList());
                    }
                    throw new Exception("Error occured while creating user: " + result.Errors.First().Description);
                }

                var token = await userManager.GenerateEmailConfirmationTokenAsync(user);

                var uriBuilder = new UriBuilder(link);
                var query = HttpUtility.ParseQueryString(uriBuilder.Query);
                query["id"] = user.Id;
                query["token"] = token;
                uriBuilder.Query = query.ToString();

                link = uriBuilder.ToString();

                await emailService.SendAsync(user.Email, "Confirm your email", link);

                await transaction.CommitAsync();

                return new UserDto
                {
                    Id = user.Id,
                    Email = user.Email,
                    UserName = user.UserName
                };
            }
        }

        public async Task<bool> SendRecoveryPasswordEmailAsync(string id, string link)
        {
            var user = await userManager.FindByIdAsync(id);

            if (user == null)
            {
                return false;
            }

            var token = await userManager.GeneratePasswordResetTokenAsync(user);

            var uriBuilder = new UriBuilder(link);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["id"] = user.Id;
            query["token"] = token;
            uriBuilder.Query = query.ToString();

            link = uriBuilder.ToString();

            await emailService.SendAsync(user.Email, "Recover your password", link);

            return true;
        }

        public async Task<bool> RecoverPasswordAsync(NewUserPasswordDto userPasswordDto)
        {
            var user = await userManager.FindByIdAsync(userPasswordDto.Id);

            if (user == null)
            {
                return false;
            }

            var result = await userManager.ResetPasswordAsync(user, userPasswordDto.Token, userPasswordDto.Password);

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

        public async Task<bool> RegisterUserTest(CreateUserDto createUser)
        {
            var user = new ApplicationUser
            {
                UserName = createUser.UserName,
                Email = createUser.Email,
            };

            var result = await userManager.CreateAsync(user, createUser.Password);

            if (result.Succeeded)
            {
                return true;
            }

            return false;
        }

        public async Task<RefreshTokenResponse?> LoginUserAsync(SignInDto signInDto)
        {
            var user = await userManager.FindByNameAsync(signInDto.Username);

            if (user is not null)
            {
                var signInResult = await signInManager.PasswordSignInAsync(user, signInDto.Password, false, false);
              
                if (signInResult.Succeeded)
                {
                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                        new Claim(ClaimTypes.Name, user.UserName)
                    };

                    var accessToken = _tokenService.GenerateAccessToken(claims);
                    var newRefreshToken = _tokenService.GenerateRefreshToken();
                    var userRefreshTokenRecord = tableContext.UserTokens.FirstOrDefault(u => u.UserId == user.Id);
                    if (userRefreshTokenRecord is null)
                    {
                        tableContext.UserTokens.Add(new TokenUser
                        {
                            UserId = user.Id,
                            LoginProvider = "111",
                            Name = "RefreshToken",
                            Value = newRefreshToken
                        });
                    }
                    else
                    {
                        userRefreshTokenRecord.Value = newRefreshToken;
                    }
                    await tableContext.SaveChangesAsync();
                    var response = new RefreshTokenResponse
                    {
                        RefreshToken = newRefreshToken,
                        AccessToken = accessToken
                    };

                    return response;
                }
                return null;
            }
            return null;
        }

        public async Task<RefreshTokenResponse> RefreshTokenAsync(
            string refreshToken,
            string accessToken)
        {
            var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);

            var userRefreshTokenRecord = tableContext.UserTokens.FirstOrDefault(u => u.Value == refreshToken);

            if (userRefreshTokenRecord == null)
            {
                // TODO: change throw to return? 
                Console.WriteLine("usertoken is null");
                throw new Exception();
            }
            var user = tableContext.Users.FirstOrDefault(u => u.Id == userRefreshTokenRecord.UserId);
            if (user == null || userRefreshTokenRecord.Value != refreshToken)
            {
                // TODO: change throw to return? 
                Console.WriteLine("usertoken is null");
                throw new Exception();
            }
            var newAccessToken = _tokenService.GenerateAccessToken(principal.Claims);
            var newRefreshToken = _tokenService.GenerateRefreshToken();

            userRefreshTokenRecord.Value = newRefreshToken;
            tableContext.SaveChanges();
            var response = new RefreshTokenResponse
            {
                RefreshToken = newRefreshToken,
                AccessToken = accessToken
            };

            return response;
        }

        public async Task LogOutUserAsync()
        {
            await signInManager.SignOutAsync();
        }

    }
}




