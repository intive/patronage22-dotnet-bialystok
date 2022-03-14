using MediatR;
using Microsoft.AspNetCore.Mvc;
using Patronage.Api.MediatR.User.Commands.ConfirmationEmail;
using Patronage.Api.MediatR.User.Commands.Create;
using Patronage.Api.MediatR.User.Commands.Password;
using Patronage.Api.MediatR.User.Commands.SignIn;
using Patronage.Api.MediatR.User.Commands.SignOut;
using Patronage.Contracts.Interfaces;
using Patronage.Contracts.ModelDtos.User;
using Patronage.DataAccess;

namespace Patronage.Api.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IMediator mediator;
        private readonly IUserService _userService;

        public UserController(IMediator mediator, IUserService userService)
        {
            this.mediator = mediator;
            _userService = userService;
        }

        /// <summary>
        /// Create new User and send confirmation email to User's email.
        /// </summary>
        /// <param name="createUser">JSON object with properties defining a user to create</param>
        /// <response code="201"></response>
        /// <response code="500"></response>
        [HttpPost("create")]
        public async Task<ActionResult<UserDto>> RegisterUserAsync([FromBody] CreateUserDto createUser)
        {
            var link = Url.Action(nameof(VerifyEmail), "User", null, Request.Scheme, Request.Host.ToString());

            if (link == null)
            {
                return BadRequest(new BaseResponse<UserDto>
                {
                    ResponseCode = StatusCodes.Status500InternalServerError,
                    Message = "Could not create confirmation link."
                });
            }

            var result = await mediator
                .Send(new CreateUserCommand
                {
                    CreateUserDto = createUser,
                    Link = link
                });

            if (result == null)
            {
                return BadRequest(new BaseResponse<UserDto>
                {
                    ResponseCode = StatusCodes.Status500InternalServerError,
                    Message = "User could not be created."
                });
            }

            return Ok(new BaseResponse<UserDto>
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
        /// <response code="200"></response>
        /// <response code="404"></response>
        [HttpGet("confirm")]
        public async Task<ActionResult<bool>> VerifyEmail([FromQuery] string id, string token)
        {
            var result =  await mediator
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
        /// <param name="id">User's id</param>
        /// <response code="200"></response>
        /// <response code="404"></response>
        /// <response code="500"></response>
        [HttpPost("resend/{id}")]
        public async Task<ActionResult<bool>> ResendConfirmationEmailAsync(string id)
        {
            var link = Url.Action(nameof(VerifyEmail), "User", null, Request.Scheme, Request.Host.ToString());

            if (link == null)
                return BadRequest(new BaseResponse<UserDto>
                {
                    ResponseCode = StatusCodes.Status500InternalServerError,
                    Message = "Could not create confirmation link."
                });

            var result = await mediator
                .Send(new ResendEmailCommand
                {
                    Id = id,
                    Link = link
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
                Message = "Email was resent successfully."
            });
        }

        /// <summary>
        /// Send email with the link to generate new password for specifed user.
        /// </summary>
        /// <param name="id">User's id</param>
        /// <response code="200"></response>
        /// <response code="404"></response>
        /// <response code="500"></response>
        [HttpPost("recover/{id}")]
        public async Task<ActionResult<bool>> SendRecoveryPasswordEmailAsync(string id)
        {
            var link = Url.Action(nameof(ResetPasswordCredentials), "User", null, Request.Scheme, Request.Host.ToString());

            if (link == null)
            {
                return BadRequest(new BaseResponse<bool>
                {
                    ResponseCode = StatusCodes.Status500InternalServerError,
                    Message = "Could not create confirmation link."
                });
            }

            var result = await mediator.Send(new SendRecoverEmailCommand
            {
                Id = id,
                Link = link
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
                Message = "Email was send successfully."
            });
        }

        /// <summary>
        /// Used to create endpoint to pass UserId and token to HttpPost method that handle password reset.
        /// </summary>
        /// <param name="id">User's id</param>
        /// <param name="token">User's token</param>
        /// <response code="200"></response>
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
            });
        }

        /// <summary>
        /// Change password for specifed user.
        /// </summary>
        /// <param name="newUserPassword">JSON object containing user id, token and new password</param>
        /// <response code="200"></response>
        /// <response code="404"></response>
        [HttpPost("reset")]
        public async Task<ActionResult<bool>> ResetPassword([FromBody] NewUserPasswordDto newUserPassword)
        {
            var result = await mediator.Send(new RecoverPasswordCommand
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
        /// <response code="500">Sorry. Try it later</response>
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody]SignInDto dto)
        {
            var response = await mediator.Send(new SignInCommand(dto));

            if (response is not null) 
            {
                return Ok(new BaseResponse<object>
                {
                    ResponseCode = StatusCodes.Status200OK,
                    Data = response,
                    Message = "You have been signed in successfully"
                });
            }

            return BadRequest(new BaseResponse<object>
            {
                ResponseCode = StatusCodes.Status400BadRequest,
                Data = null,
                Message = "Username or password is not valid"
            });
        }

        /// <summary>
        /// Action to sign out the user
        /// </summary>
        /// <response code="200">Successfully signed in</response>
        /// <response code="500">Sorry. Try it later</response>
        [HttpPost("logoff")]
        public async Task<ActionResult> Logoff([FromBody] string accessToken)
        {
            var isSucceded = await mediator.Send(new SignOutCommand(accessToken));
            
            if (!isSucceded)
            {
                return Unauthorized(new BaseResponse<bool>
                {
                    ResponseCode = StatusCodes.Status401Unauthorized,
                    Message = "Your access token is inactive"
                });
            }

            return Ok(new BaseResponse<bool>
            {
                ResponseCode = StatusCodes.Status200OK,
                Message = "You have been signed out successfully"
            });
        }

        [HttpPost("registerTest")]
        public async Task<ActionResult> RegisterTest([FromBody] CreateUserDto createUser)
        {
            var isSucceded = await _userService.RegisterUserTest(createUser);

            if (isSucceded)
            {
                return Ok(new BaseResponse<object>
                {
                    ResponseCode = StatusCodes.Status200OK,
                    Message = "You have been registered successfully"
                });
            }

            return BadRequest(new BaseResponse<object>
            {
                ResponseCode = StatusCodes.Status400BadRequest,
                Data = null,
                Message = "You have not been registered"
            });
        }

        [HttpPost("refreshtoken")]
        public async Task<ActionResult> RefreshToken([FromHeader(Name ="RefreshToken")] string refreshToken, [FromHeader(Name = "Bearer")] string accessToken)
        {
            
            var response = await _userService.RefreshTokenAsync(refreshToken, accessToken);          
            return Ok(response);
        }
    }
}