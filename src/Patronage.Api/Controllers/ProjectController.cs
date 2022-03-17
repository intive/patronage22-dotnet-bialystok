using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Patronage.Api.MediatR.Projects.Commands;
using Patronage.Api.MediatR.Projects.Queries;
using Patronage.Contracts.Interfaces;
using Patronage.Contracts.ModelDtos.Projects;
using Patronage.DataAccess;

namespace Patronage.Api.Controllers
{
    [Route("api/project")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProjectController(IMediator mediator, IProjectService projectService)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Returns all Projects. When you give "searchedPhrase" in Query you will receive only projects
        /// in which name, alias or description contains this phrase.
        /// </summary>
        /// <param name="searchedPhrase" example="string">The phrase that project's name/alias/description have to contain.</param>
        /// <response code="200">Searched projects</response>
        /// <response code="500">Sorry. Try it later</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectDto>>> GetAll([FromQuery] string? searchedPhrase)
        {
            var projects = await _mediator.Send(new GetAllProjectsQuery(searchedPhrase));

            return Ok(new BaseResponse<IEnumerable<ProjectDto>>
            {
                ResponseCode = StatusCodes.Status200OK,
                Data = projects
            });
        }

        /// <summary>
        /// Returns project by id
        /// </summary>
        /// <param name="id" example="10">The project's id</param>
        /// <response code="200">Searched project</response>
        /// <response code="404">Poroject not found</response>
        /// <response code="500">Sorry. Try it later</response>
        [Authorize]
        [HttpGet("{id}")]
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

        /// <summary>
        /// Creates Project
        /// </summary>
        /// <param name="projectDto">JSON object with properties defining a project</param>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/project
        ///     {
        ///        "Name": "Name of project",
        ///        "Alias": "Project's alias",
        ///        "Description": "Project's description",
        ///        "isActive": true
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Project correctly created</response>
        /// <response code="400">Pease insert correct JSON object with parameters</response>
        /// <response code="500">Sorry. Try it later</response>
        [HttpPost]
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

        /// <summary>
        /// Updates project - it's all properties
        /// </summary>
        /// <param name="id" example="10">The project's id</param>
        /// <param name="projectDto">JSON object with properties to update</param>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/project/{id}
        ///     {
        ///        "Name": "Name of project",
        ///        "Alias": "Project's alias",
        ///        "Description": "Project's description",
        ///        "isActive": true
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Project correctly updated</response>
        /// <response code="400">Pease insert correct JSON object with parameters</response>
        /// <response code="500">Sorry. Try it later</response>
        [HttpPut("{id}")]
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

        /// <summary>
        /// Updates project - only selected properties
        /// </summary>
        /// <param name="id" example="10">The project's id</param>
        /// <param name="projectDto"> The project's partial DTO object</param>
        /// <remarks>
        /// Sample request:
        ///
        ///     PATCH /api/project/{id}
        ///     {
        ///        "Name": { "data": "Name of project" },
        ///        "Description": { "data": null }
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Project correctly updated</response>
        /// <response code="400">Pease insert correct JSON object with parameters</response>
        /// <response code="500">Sorry. Try it later</response>
        [HttpPatch("{id}")]
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

        /// <summary>
        /// Deletes Project. (Changes flag "IsActive" to false)")
        /// </summary>
        /// <param name="id" example="10">The project's id</param>
        /// <response code="200">Project correctly deleted</response>
        /// <response code="404">Project with this id doesn't exist</response>
        /// <response code="500">Sorry. Try it later</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProjectDto>> DeleteProject([FromRoute] int id)
        {
            var isDeleted = await _mediator.Send(new DeleteProjectCommand(id));

            if (!isDeleted)
            {
                return NotFound(new BaseResponse<bool>
                {
                    ResponseCode = StatusCodes.Status404NotFound,
                    Message = "Project with this id doesn't exist"
                });
            }

            return Ok(new BaseResponse<bool>
            {
                ResponseCode = StatusCodes.Status200OK,
                Message = "This project has been successfully deleted"
            });
        }
    }
}