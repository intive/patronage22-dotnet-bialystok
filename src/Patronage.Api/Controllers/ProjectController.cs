using Microsoft.AspNetCore.Mvc;
using Patronage.Contracts.Interfaces;
using Patronage.Contracts.ModelDtos.Projects;
using Swashbuckle.AspNetCore.Annotations;

namespace Patronage.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }



        [SwaggerOperation(Summary = "Returns all Projects")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesDefaultResponseType]
        public ActionResult<IEnumerable<ProjectDto>> GetAll([FromQuery]string? searchedProject)
        {
            var projects = _projectService.GetAll(searchedProject);

            return Ok(projects);
        }



        [SwaggerOperation(Summary = "Returns Project by id")]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ProjectDto> GetById([FromRoute]int id)
        {
            var project = _projectService.GetById(id);

            if (project is null) return NotFound();

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
            var id = _projectService.Create(projectDto);

            return Created($"/api/project/{id}", null);
        }




        [SwaggerOperation(Summary = "Updates project - it's all properties")]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult UpdateProject([FromRoute] int id, [FromBody] CreateOrUpdateProjectDto projectDto)
        {
            var isUpdated = _projectService.Update(id, projectDto);

            if (!isUpdated) return NotFound();

            return Ok();
        }




        [SwaggerOperation(Summary = "Updates project - only selected properties")]
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult LightUpdateProject([FromRoute] int id, [FromBody] PartialProjectDto projectDto)
        {
            var isUpdated = _projectService.LightUpdate(id, projectDto);

            if (!isUpdated) return NotFound();

            return Ok();
        }




        [SwaggerOperation(Summary = "Deletes Project. Changes flag \"IsActive\" to false")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ProjectDto> DeleteProject([FromRoute] int id)
        {
            var isDeleted = _projectService.Delete(id);

            if (isDeleted) return NoContent();

            return NotFound();
        }
    }
}
