using MediatR;
using Microsoft.AspNetCore.Mvc;
using Patronage.Api.MediatR.Projects.Commands;
using Patronage.Api.MediatR.Projects.Queries;
using Patronage.Contracts.Interfaces;
using Patronage.Contracts.ModelDtos.Projects;
using Patronage.DataAccess;
using Swashbuckle.AspNetCore.Annotations;

namespace Patronage.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProjectController(IMediator mediator, IProjectService projectService)
        {
            _mediator = mediator;
        }





        [SwaggerOperation(Summary = "Returns all Projects. When you give \"searchedPhrase\" in Query you will receive only projects" +
            " in which name, alias or description contains this phrase")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ProjectDto>>> GetAll([FromQuery] string? searchedPhrase)
        {
            var projects = await _mediator.Send(new GetAllProjectsQuery(searchedPhrase));

            return Ok(new BaseResponse<IEnumerable<ProjectDto>>
            {
                ResponseCode = StatusCodes.Status200OK,
                Data = projects
            });
        }




        [SwaggerOperation(Summary = "Returns Project by id")]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProjectDto>> GetById([FromRoute] int id)
        {
            var project = await _mediator.Send(new GetSingleProjectQuery(id));

            if (project is null)
            {
                return NotFound(new BaseResponse<ProjectDto>
                {
                    ResponseCode = StatusCodes.Status404NotFound,
                    Message = "Project with this Id doesn't exist"
                });
            }


            return Ok(new BaseResponse<ProjectDto>
            {
                ResponseCode = StatusCodes.Status200OK,
                Data = project
            });
        }




        [SwaggerOperation(Summary = "Creates Project")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreateProject([FromBody] CreateProjectDto projectDto)
        {
            var id = await _mediator.Send(new CreateProjectCommand(projectDto));

            return Created($"/api/project/{id}", 
                new BaseResponse<int>
                {
                    ResponseCode = StatusCodes.Status201Created,
                    Data = id
                });
        }




        [SwaggerOperation(Summary = "Updates project - it's all properties")]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateProject([FromRoute] int id, [FromBody] UpdateProjectDto projectDto)
        {
            var isExistingRecord = await _mediator.Send(new UpdateProjectCommand(id, projectDto));

            if (!isExistingRecord)
            {
                return NotFound(new BaseResponse<ProjectDto>
                {
                    ResponseCode = StatusCodes.Status404NotFound,
                    Message = "Project with this id doesn't exist"
                });
            }
            
            
            return Ok(new BaseResponse<ProjectDto>
            {
                ResponseCode = StatusCodes.Status200OK,
                Message = "This project has been successfully updated"
            });
        }




        [SwaggerOperation(Summary = "Updates project - only selected properties")]
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> LightUpdateProject([FromRoute] int id, [FromBody] PartialProjectDto projectDto)
        {
            var isExistingRecord = await _mediator.Send(new LightUpdateProjectCommand(id, projectDto));

            if (!isExistingRecord)
            {
                return NotFound(new BaseResponse<ProjectDto>
                {
                    ResponseCode = StatusCodes.Status404NotFound,
                    Message = "Project with this id doesn't exist or you try to insert null value to property that can not be null or Name/Alias" +
                    "has been used to another project (these properties must be unique)"
                });
            }


            return Ok(new BaseResponse<ProjectDto>
            {
                ResponseCode = StatusCodes.Status200OK,
                Message = "This project has been successfully updated"
            });
        }




        [SwaggerOperation(Summary = "Deletes Project. (Changes flag \"IsActive\" to false)")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProjectDto>> DeleteProject([FromRoute] int id)
        {
            var isDeleted = await _mediator.Send(new DeleteProjectCommand(id));

            if (!isDeleted)
            {
                return NotFound(new BaseResponse<ProjectDto>
                {
                    ResponseCode = StatusCodes.Status404NotFound,
                    Message = "Project with this id doesn't exist"
                });
            }


            return Ok(new BaseResponse<ProjectDto>
            {
                ResponseCode = StatusCodes.Status200OK,
                Message = "This project has been successfully deleted"
            });
        }
    }
}
