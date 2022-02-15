using MediatR;
using Microsoft.AspNetCore.Mvc;
using Patronage.Api.Functions.Commands.CreateProject;
using Patronage.Api.Functions.Commands.DeleteProject;
using Patronage.Api.Functions.Commands.LightUpdateProject;
using Patronage.Api.Functions.Commands.UpdateProject;
using Patronage.Api.Functions.Queries.GetAllProjects;
using Patronage.Api.Functions.Queries.GetSingleProject;
using Patronage.Contracts.Interfaces;
using Patronage.Contracts.ModelDtos.Projects;
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




        [SwaggerOperation(Summary = "Returns all Projects")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesDefaultResponseType]
        public ActionResult<IEnumerable<ProjectDto>> GetAll([FromQuery] string? searchedProject)
        {
            var projects = _mediator.Send(new GetAllProjectsQuery(searchedProject));

            return Ok(projects);
        }



        [SwaggerOperation(Summary = "Returns Project by id")]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ProjectDto> GetById([FromRoute] int id)
        {
            var project = _mediator.Send(new GetSingleProjectQuery(id));

            if (project.Result is null) return NotFound();

            return Ok(project);
        }



        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>
        [SwaggerOperation(Summary = "Creates Project")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult CreateProject([FromBody] CreateOrUpdateProjectDto projectDto)
        {
            var id = _mediator.Send(new CreateProjectCommand(projectDto));

            return Created($"/api/project/{id}", null);
        }




        [SwaggerOperation(Summary = "Updates project - it's all properties")]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult UpdateProject([FromRoute] int id, [FromBody] CreateOrUpdateProjectDto projectDto)
        {
            var isUpdated = _mediator.Send(new UpdateProjectCommand(id, projectDto));

            //if (!isUpdated) return NotFound();

            //return Ok();
            return NoContent();
        }




        [SwaggerOperation(Summary = "Updates project - only selected properties")]
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult LightUpdateProject([FromRoute] int id, [FromBody] PartialProjectDto projectDto)
        {
            var isUpdated = _mediator.Send(new LightUpdateProjectCommand(id, projectDto));

            //if (!isUpdated) return NotFound();

            //return Ok();
            return NoContent();
        }




        [SwaggerOperation(Summary = "Deletes Project. (Changes flag \"IsActive\" to false")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ProjectDto> DeleteProject([FromRoute] int id)
        {
            _mediator.Send(new DeleteProjectCommand(id));

            //if (isDeleted) return NoContent();

            //return NotFound();
            return NoContent();
        }
    }
}
