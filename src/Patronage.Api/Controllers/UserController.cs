using MediatR;
using Microsoft.AspNetCore.Mvc;
using Patronage.Api.MediatR.User.Commands.ConfirmationEmail;
using Patronage.Api.MediatR.User.Commands.Create;
using Patronage.Api.MediatR.User.Commands.Password;
using Patronage.Contracts.ModelDtos.User;
using Patronage.DataAccess;

namespace Patronage.Api.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IMediator mediator;

        public UserController(IMediator mediator)
        {
            this.mediator = mediator;
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
    }
}