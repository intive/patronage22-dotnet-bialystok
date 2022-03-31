using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Patronage.Api.MediatR.User.Commands.ConfirmationEmail;
using Patronage.Api.MediatR.User.Commands.Create;
using Patronage.Api.MediatR.User.Commands.Password;
using Patronage.Api.MediatR.User.Commands.RefreshToken;
using Patronage.Api.MediatR.User.Commands.SignIn;
using Patronage.Api.MediatR.User.Commands.SignOut;
using Patronage.Api.MediatR.User.Queries;
using Patronage.Contracts.Interfaces;
using Patronage.Contracts.ModelDtos.User;
using Patronage.Contracts.ResponseModels;
using Patronage.DataAccess;

namespace Patronage.Api.Controllers
{
    /// <summary>
    /// For information on how to use the various controllers, go to:
    /// https://github.com/intive/patronage22-dotnet-bialystok/wiki/Authentication
    /// </summary>
    [Route("api/user")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Create new User and send confirmation email to User's email.
        /// </summary>
        /// <param name="createUser">JSON object with properties defining a user to create</param>
        /// <response code="201">User was created successfully and confirmation email was sent.</response>
        /// <response code="500">Confirmation link could not be created.</response>
        [AllowAnonymous]
        [HttpPost("create")]
        public async Task<ActionResult<UserDto>> RegisterUserAsync([FromBody] CreateUserDto createUser)
        {
            var link = Url.Action(nameof(VerifyEmailAsync), "User", null, Request.Scheme, Request.Host.ToString());

            if (link is null)
            {
                throw new Exception("Link could not be created.");
            }

            var result = await _mediator
                .Send(new CreateUserCommand
                {
                    CreateUserDto = createUser,
                    Link = link
                });

            return CreatedAtAction(nameof(RegisterUserAsync), new BaseResponse<UserDto>
            {
                ResponseCode = StatusCodes.Status201Created,
                Data = result,
                Message = "User was created successfully and confirmation email was sent."
            });
        }

        /// <summary>
        /// Handle confirmation email that was sent to user.
        /// </summary>
        /// <param name="id">User's id</param>
        /// <param name="token">User's token</param>
        /// <response code="200">Email was confirmed.</response>
        /// <response code="404">There's no user with this Id.</response>
        [AllowAnonymous]
        [HttpGet("confirm")]
        public async Task<ActionResult<bool>> VerifyEmailAsync([FromQuery] string id, string token)
        {
            var result = await _mediator
                .Send(new ConfirmEmailCommand
                {
                    Id = id,
                    Token = token
                });

            if (!result)
            {
                return NotFound(new BaseResponse<bool>
                {
                    ResponseCode = StatusCodes.Status404NotFound,
                    Message = $"There's no user with Id: {id}"
                });
            }

            return Ok(new BaseResponse<bool>
            {
                ResponseCode = StatusCodes.Status200OK,
                Data = result,
                Message = "Email was confirmed."
            });
        }

        /// <summary>
        /// Resend confirmation email to specifed user.
        /// </summary>
        /// <param name="email">User's email</param>
        /// <response code="200">Email was resent successfully.</response>
        /// <response code="404">There's no user registered with this email address. Check spelling and try again.</response>
        /// <response code="500">Link could not be created.</response>
        [AllowAnonymous]
        [HttpPost("resend/{email}")]
        public async Task<ActionResult<bool>> ResendConfirmationEmailAsync(string email)
        {
            var link = Url.Action(nameof(VerifyEmailAsync), "User", null, Request.Scheme, Request.Host.ToString());

            if (link == null)
            {
                throw new Exception("Link could not be created.");
            }

            var result = await _mediator
                .Send(new ResendEmailCommand
                {
                    Email = email,
                    Link = link
                });

            if (!result)
            {
                return NotFound(new BaseResponse<bool>
                {
                    ResponseCode = StatusCodes.Status404NotFound,
                    Message = $"There's no user with Email: {email}"
                });
            }

            return Ok(new BaseResponse<bool>
            {
                ResponseCode = StatusCodes.Status200OK,
                Data = result,
                Message = "Email was resent successfully."
            });
        }

        /// <summary>
        /// Send email with the link to generate new password for specifed user. You have to provide Email or Username.
        /// </summary>
        /// <param name="sendRecoverEmail">User's email or username.</param>
        /// <response code="200">Email was sent successfully.</response>
        /// <response code="404">There's no user with this email or username.</response>
        /// <response code="500">Link could not be created.</response>
        [AllowAnonymous]
        [HttpPost("recover")]
        public async Task<ActionResult<bool>> SendRecoveryPasswordEmailAsync([FromBody] RecoverPasswordDto sendRecoverEmail)
        {
            var link = Url.Action(nameof(ResetPasswordCredentials), "User", null, Request.Scheme, Request.Host.ToString());

            if (link is null)
            {
                throw new Exception("Link could not be created.");
            }

            var result = await _mediator.Send(new SendRecoverEmailPasswordCommand
            {
                recoverPasswordDto = sendRecoverEmail,
                Link = link
            });

            if (!result)
            {
                return NotFound(new BaseResponse<bool>
                {
                    ResponseCode = StatusCodes.Status404NotFound,
                    Message = $"There's no user with specified email or username.",
                    Data = false
                });
            }

            return Ok(new BaseResponse<bool>
            {
                ResponseCode = StatusCodes.Status200OK,
                Data = result,
                Message = "Email was send successfully."
            });
        }

        /// <summary>
        /// Used to create endpoint to pass UserId and token to HttpPost method that handle password reset.
        /// </summary>
        /// <param name="id">User's id</param>
        /// <param name="token">User's token</param>
        /// <response code="200">User id and token was fetched successfully.</response>
        [AllowAnonymous]
        [HttpGet("reset")]
        public ActionResult<bool> ResetPasswordCredentials([FromQuery] string id, string token)
        {
            return Ok(new BaseResponse<Dictionary<string, string>>()
            {
                ResponseCode = StatusCodes.Status200OK,
                Data = new Dictionary<string, string>()
                {
                    { "id", id },
                    { "token", token }
                },
                Message = "User id and token was fetched successfully."
            });
        }

        /// <summary>
        /// Change password for specifed user.
        /// </summary>
        /// <param name="newUserPassword">JSON object containing user id, token and new password</param>
        /// <response code="200">Password was changed successfully.</response>
        /// <response code="404">There's no user with this Id.</response>
        [AllowAnonymous]
        [HttpPost("reset")]
        public async Task<ActionResult<bool>> ResetPasswordAsync([FromBody] NewUserPasswordDto newUserPassword)
        {
            var result = await _mediator.Send(new RecoverPasswordCommand
            {
                NewUserPassword = newUserPassword
            });

            if (!result)
            {
                return NotFound(new BaseResponse<bool>
                {
                    ResponseCode = StatusCodes.Status404NotFound,
                    Message = $"There's no user with Id: {newUserPassword.Id}"
                });
            }

            return Ok(new BaseResponse<bool>
            {
                ResponseCode = StatusCodes.Status200OK,
                Data = result,
                Message = "Password was changed successfully."
            });
        }

        /// <summary>
        /// Action to sign in a user by username and password
        /// </summary>
        /// <param name="dto">JSON object with username, password and confirmed password</param>
        /// <response code="200">Successfully signed in</response>
        /// <response code="400">Username or password is not valid</response>
        /// <response code="500">Internal server error</response>
        [AllowAnonymous]
        [HttpPost("signin")]
        public async Task<ActionResult> SignInUserAsync([FromBody] SignInDto dto)
        {
            var response = await _mediator.Send(new SignInCommand(dto));

            if (response is null)
            {
                return BadRequest(new BaseResponse<object>
                {
                    ResponseCode = StatusCodes.Status400BadRequest,
                    Data = null,
                    Message = "Username or password is not valid"
                });
            }

            return Ok(new BaseResponse<RefreshTokenResponse>
            {
                ResponseCode = StatusCodes.Status200OK,
                Data = response,
                Message = "You have been signed in successfully"
            });
        }

        /// <summary>
        /// Action to sign out the user. The user's refresh token is beeing deleted from database.
        /// </summary>
        /// <response code="200">Successfully signed out</response>
        /// <response code="500">Internal server error</response>
        [HttpPost("signout")]
        public async Task<ActionResult> SignOutUserAsync()
        {
            HttpContext.Request.Headers.TryGetValue("Authorization", out var accessToken);
            await _mediator.Send(new SignOutCommand(accessToken.ToString().Split(' ').Last()));

            return Ok(new BaseResponse<bool>
            {
                ResponseCode = StatusCodes.Status200OK,
                Message = "You have been signed out successfully"
            });
        }

        /// <summary>
        /// Generates a new acccess token (JWT). It is necessary to give an old access token in header 
        /// (the same way as when authorizing to protected resorces)
        /// and active refresh token in body
        /// </summary>
        /// <param name="refreshToken">A refresh token given afler signing in (and refreshing access token)</param>
        /// <response code="200">New access token and refresh token</response>
        /// <response code="401">Something wrong with given tokens propably</response>
        /// <response code="500">Internal server error</response>
        [HttpPost("refreshtoken")]
        public async Task<ActionResult> RefreshTokenAsync([FromBody] RefreshTokenDto refreshToken)
        {
            var isAccessTokenGiven = HttpContext.Request.Headers.TryGetValue("Authorization", out var accessToken);
            
            if (!isAccessTokenGiven)
            {
                return Unauthorized(new BaseResponse<object>
                {
                    ResponseCode = StatusCodes.Status401Unauthorized,
                    Data = null,
                    Message = "Failed to generate new token"
                });
            }

            var response = await _mediator.Send(new RefreshTokenCommand(refreshToken.RefreshToken, 
                                                                        accessToken.ToString().Split(' ')[1]));

            if (response is null)
            {
                return Unauthorized(new BaseResponse<object>
                {
                    ResponseCode = StatusCodes.Status401Unauthorized,
                    Data = null,
                    Message = "Failed to generate new token"
                });

            }

            return Ok(new BaseResponse<object>
            {
                ResponseCode = StatusCodes.Status401Unauthorized,
                Data = response,
                Message = "Here is your new access token and refresh token"
            });
        }

        /// <summary>
        /// Returns list of users. When you give "searchedPhrase" in Query you will receive only users
        /// in which username, email, first name or surename contains this phrase.
        /// </summary>
        /// <param name="searchedPhrase">The phrase that username/email/first name/surename has to contain.</param>
        /// <response code="200">Searched users</response>
        /// <response code="500">Sorry. Try it later</response>
        [HttpGet("list")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsersAsync([FromQuery] string? searchedPhrase)
        {
            var users = await _mediator.Send(new GetAllUsersQuery(searchedPhrase));

            return Ok(new BaseResponse<IEnumerable<UserDto>>
            {
                ResponseCode = StatusCodes.Status200OK,
                Message = "There are users, you have searched",
                Data = users
            });
        }
    }
}