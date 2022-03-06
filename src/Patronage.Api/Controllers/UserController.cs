using MediatR;
using Microsoft.AspNetCore.Mvc;
using Patronage.Api.MediatR.User.Commands.ConfirmationEmail;
using Patronage.Api.MediatR.User.Commands.Create;
using Patronage.Api.MediatR.User.Commands.Password;
using Patronage.Api.MediatR.User.Commands.SignIn;
using Patronage.Contracts.ModelDtos.User;
using Patronage.DataAccess;
using Swashbuckle.AspNetCore.Annotations;

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

        [SwaggerOperation(Summary = "Create new User and send confirmation email to User's email.")]
        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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

        [SwaggerOperation(Summary = "Handle confirmation email that was sent to user.")]
        [HttpGet("confirm")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public  ActionResult<bool> VerifyEmail([FromQuery] string id, string token)
        {
            var result =  mediator
                .Send(new ConfirmEmailCommand
                {
                    Id = id,
                    Token = token
                });

            if (!result.Result)
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
                Data = result.Result,
                Message = "Email was confirmed."
            });
        }

        [SwaggerOperation(Summary = "Resend confirmation email.")]
        [HttpPost("resend/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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

        [SwaggerOperation(Summary = "Send email with the link to generate new password.")]
        [HttpPost("recover/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> SendRecoveryPasswordEmailAsync(string id)
        {
            var link = Url.Action(nameof(PassResetPasswordCredentials), "User", null, Request.Scheme, Request.Host.ToString());

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

        [SwaggerOperation(Summary = "Used to pass UserId and token to HttpPost method that handle password reset.")]
        [HttpGet("reset")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<bool> PassResetPasswordCredentials([FromQuery] string id, string token)
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

        [SwaggerOperation(Summary = "Change user password.")]
        [HttpPost("reset")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        /// Returns project by id
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
    }
}