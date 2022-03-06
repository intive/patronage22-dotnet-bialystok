﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
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
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly TableContext tableContext;
        private readonly IEmailService emailService;
        private readonly ILoggerFactory logger;

        public UserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            TableContext tableContext, IEmailService emailService, ILoggerFactory logger)
        {
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            this.signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            this.tableContext = tableContext ?? throw new ArgumentNullException(nameof(tableContext));
            this.emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> ResendEmailConfirmationAsync(string id, string link)
        {
            var user = await userManager.FindByIdAsync(id);

            if(user == null)
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

                return new UserDto { 
                    Id = user.Id,
                    Email = user.Email,
                    UserName = user.UserName
                };
            }
        }

        public async Task<bool> SendRecoveryPasswordEmailAsync(string id, string link)
        {
            var user = await userManager.FindByIdAsync(id);

            if(user == null)
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

            if(user == null)
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

        public async Task<string?> LoginUserAsync(SignInDto signInDto)
        {
            var user = await userManager.FindByNameAsync(signInDto.Username);

            if (user is null)
            {
                return null;
            }

            var result = await userManager.CheckPasswordAsync(user, signInDto.Password);

            if (!result)
            {
                return null;
            }

            //// alternative to above solution
            //var signInResult = await signInManager.PasswordSignInAsync(user, signInDto.Password, false, false);
            //if (!signInResult.Succeeded)
            //{
            //    return null;
            //}

            //var claims = new[]
            //{
            //    new Claim(ClaimTypes.Name, signInDto.Username),
            //    new Claim(ClaimTypes.NameIdentifier, user.Id),
            //};

            //var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AuthSettings:Key"]));

            //var token = new JwtSecurityToken(
            //    issuer: _configuration["AuthSettings:Issuer"],
            //    audience: _configuration["AuthSettings:Audience"],
            //    claims: claims,
            //    expires: DateTime.Now.AddDays(30),
            //    signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

            //string tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);

            return "tokenAsString";
        }

        public async Task LogOutUserAsync()
        {
            await signInManager.SignOutAsync();
        }
    }
}
