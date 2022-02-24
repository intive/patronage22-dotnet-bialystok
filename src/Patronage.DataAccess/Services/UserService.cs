using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Patronage.Contracts.Interfaces;
using Patronage.Contracts.ModelDtos.User;
using Patronage.Models;

namespace Patronage.DataAccess.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly LinkGenerator linkGenerator;
        private readonly IEmailSender emailSender;
        private readonly TableContext tableContext;
        private readonly HttpContext httpContext;

        public UserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, 
            LinkGenerator linkGenerator, IEmailSender emailSender, IHttpContextAccessor httpContextAccessor, TableContext tableContext)
        {
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            this.signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            this.linkGenerator = linkGenerator ?? throw new ArgumentNullException(nameof(linkGenerator));
            this.emailSender = emailSender ?? throw new ArgumentNullException(nameof(emailSender));
            this.tableContext = tableContext ?? throw new ArgumentNullException(nameof(tableContext));
            httpContext = httpContextAccessor.HttpContext ?? throw new ArgumentNullException(nameof(httpContextAccessor));
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
                throw new Exception("Error occured while creating user: " + result.Errors.First().Description);
            }

            return true;
        }

        // TODO After implementation of fluent validation.
        // Change throwing exception to 'return null'
        public async Task<UserDto> CreateUserAsync(CreateUserDto createUser)
        {
            var user = new ApplicationUser
            {
                UserName = createUser.UserName,
                Email = createUser.Email,
            };

            using (var transaction = tableContext.Database.BeginTransaction())
            {
                var result = await userManager.CreateAsync(user, createUser.Password);

                // Mostly return validations error. So it's probably redundant after implementations of validation
                if (!result.Succeeded)
                {
                    // log problems
                    if (result.Errors.Count() > 1)
                    {
                        throw new AggregateException("Multiple errors occured while creating user.",
                            result.Errors.Select(x => new Exception(x.Description)).ToList());
                    }
                    throw new Exception("Error occured while creating user: " + result.Errors.First().Description);
                }

                var token = await userManager.GenerateEmailConfirmationTokenAsync(user);

                // I dont know why but the only route that work is /api/user/. For example I cant generate /api/user/confirm/
                var link = linkGenerator.GetUriByPage(
                    httpContext,
                    page: "/api/user/",
                    handler : null,
                    values : new { id = user.Id, token = token });

                if(link == null)
                {
                    // log problem
                    throw new Exception("Couldn not generate confirmation link.");
                }

                emailSender.SendEmail(user.Email, "Confirm your email", link);

                await transaction.CommitAsync();

                return new UserDto { 
                    Id = user.Id,
                    Email = user.Email,
                    UserName = user.UserName
                };
            }
        }

        public Task<bool> GenerateRecoveryPassword(string id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RecoverPassword(string id, string token)
        {
            throw new NotImplementedException();
        }
    }
}
