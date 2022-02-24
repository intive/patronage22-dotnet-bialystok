using Microsoft.AspNetCore.Mvc;
using Patronage.Api.MediatR.User.Commands;
using Patronage.Contracts.Interfaces;
using Patronage.Contracts.ModelDtos.User;
using Patronage.DataAccess;
using Swashbuckle.AspNetCore.Annotations;

namespace Patronage.Api.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [SwaggerOperation(Summary = "Create new User and send confirmation email to User's email.")]
        [HttpPost]
        public async Task<ActionResult<UserDto>> RegisterUserAsync([FromBody] CreateUserCommand createUser)
        {
            //TODO MediatR implementation
            var result = await userService.CreateUserAsync(createUser.CreateUserDto);

            //TODO change CreateUserAsync to actually return null instead of throwing exception
            if (result == null)
            {
                return BadRequest();
            }

            return Ok(new BaseResponse<UserDto>
            {
                ResponseCode = StatusCodes.Status200OK,
                Data = result,
                Message = "User was created successfully and confirmation email was sent."
            });
        }

        [SwaggerOperation(Summary = "Handle confirmation email that was sent to user.")]
        [HttpGet]
        public async Task<ActionResult<UserDto>> ConfirmEmail([FromQuery] string id, string token)
        {
            //TODO MediatR implementation
            var result = await userService.ConfirmEmail(id, token);

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

        [HttpGet("recover")]
        public Task<ActionResult<UserDto>> RecoverPassword([FromQuery] string id, string token)
        {
            throw new NotImplementedException();
        }
    }
}
