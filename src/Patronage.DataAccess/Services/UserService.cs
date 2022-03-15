using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using NETCore.MailKit.Core;
using Patronage.Contracts.Interfaces;
using Patronage.Contracts.ModelDtos.User;
using Patronage.Models;
using System.Web;

namespace Patronage.DataAccess.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly TableContext _tableContext;
        private readonly IEmailService _emailService;
        private readonly ILoggerFactory _logger;

        public UserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            TableContext tableContext, IEmailService emailService, ILoggerFactory logger)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _tableContext = tableContext ?? throw new ArgumentNullException(nameof(tableContext));
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> ResendEmailConfirmationAsync(string email, string link)
        {
            var user = await _userManager.FindByIdAsync(id);

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
                userManager.FindByEmailAsync(recoverPasswordDto.Email!.Data) : 
                userManager.FindByNameAsync(recoverPasswordDto.Username!.Data));

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
    }
}