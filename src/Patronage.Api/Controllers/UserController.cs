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
        /// <response code="400">"User could not be created."</response>
        /// <response code="500">Link could not be created.</response>
        [HttpPost("create")]
        public async Task<ActionResult<UserDto>> RegisterUserAsync([FromBody] CreateUserDto createUser)
        {
            var link = Url.Action(nameof(VerifyEmailAsync), "User", null, Request.Scheme, Request.Host.ToString());

            if (link == null)
            {
                throw new Exception("Link could not be created.");
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
        [HttpGet("confirm")]
        public async Task<ActionResult<bool>> VerifyEmailAsync([FromQuery] string id, string token)
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
        /// <param name="email">User's email</param>
        /// <response code="200">Email was resent successfully.</response>
        /// <response code="404">There's no user with this Id.</response>
        /// <response code="500">Link could not be created.</response>
        [HttpPost("resend/{email}")]
        public async Task<ActionResult<bool>> ResendConfirmationEmailAsync(string email)
        {
            var link = Url.Action(nameof(VerifyEmailAsync), "User", null, Request.Scheme, Request.Host.ToString());

            if (link == null)
            {
                throw new Exception("Link could not be created.");

            }

            var result = await mediator
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
        /// Send email with the link to generate new password for specifed user.
        /// </summary>
        /// <param name="sendRecoverEmail"></param>
        /// <response code="200">Email was send successfully.</response>
        /// <response code="404">There's no user with specified email or username.</response>
        /// <response code="500">Link could not be created.</response>
        [HttpPost("recover")]
        public async Task<ActionResult<bool>> SendRecoveryPasswordEmailAsync([FromBody] RecoverPasswordDto sendRecoverEmail)
        {
            var link = Url.Action(nameof(ResetPasswordCredentials), "User", null, Request.Scheme, Request.Host.ToString());

            if (link == null)
            {
                throw new Exception("Link could not be created.");
            }

            var result = await mediator.Send(new SendRecoverEmailPasswordCommand
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
        [HttpPost("reset")]
        public async Task<ActionResult<bool>> ResetPasswordAsync([FromBody] NewUserPasswordDto newUserPassword)
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