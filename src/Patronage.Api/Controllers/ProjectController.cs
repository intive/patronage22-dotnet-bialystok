using Microsoft.AspNetCore.Mvc;
using Patronage.Contracts;
using Patronage.Contracts.Interfaces;
using Patronage.DataAccess;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public ActionResult<IEnumerable<ProjectDto>> GetAll()
        {
            var projects = _projectService.GetAll();

            return Ok(projects);
        }



        [SwaggerOperation(Summary = "Returns Project by id")]
        [HttpGet("{id}")]
        public ActionResult<ProjectDto> GetById([FromRoute]int id)
        {
            var project = _projectService.GetById(id);

            if (project is null) return NotFound();

            return Ok(project);
        }



        [SwaggerOperation(Summary = "Creates Project")]
        [HttpPost]
        public ActionResult CreateProject([FromBody] ProjectDto projectDto)
        {
            var id = _projectService.Create(projectDto);

            return Created($"/api/project/{id}", null);
        }




        [SwaggerOperation(Summary = "Updates project - it's all properties")]
        [HttpPut("{id}")]
        public ActionResult UpdateProject([FromRoute] int id, [FromBody]ProjectDto projectDto)
        {
            var isUpdated = _projectService.Update(id, projectDto);

            if (!isUpdated) return NotFound();

            return Ok();
        }




        [SwaggerOperation(Summary = "Updates project - only selected properties")]
        [HttpPatch("{id}")]
        public ActionResult LightUpdateProject([FromRoute] int id, [FromBody] ProjectDto projectDto)
        {
            /*---------------*/
            /*---- TO DO ----*/
            /*---------------*/
            return Ok();
        }




        [SwaggerOperation(Summary = "Deletes Project. (Changes flag \"IsActive\" to false")]
        [HttpDelete("{id}")]
        public ActionResult<ProjectDto> DeleteProject([FromRoute] int id)
        {
            var isDeleted = _projectService.Delete(id);

            if (isDeleted) return NoContent();

            return NotFound();
        }
    }
}
