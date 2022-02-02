using Microsoft.AspNetCore.Mvc;
using Patronage.Contracts.Interfaces;
using Patronage.Contracts.ModelDtos;
using Patronage.DataAccess;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patronage.Api.Controllers
{
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
            /*---------------*/
            /*---- TO DO ----*/
            /*---------------*/
            return Ok();
        }



        [SwaggerOperation(Summary = "Returns Project by id")]
        [HttpGet("{id}")]
        public ActionResult<ProjectDto> GetById([FromRoute]int id)
        {
            /*---------------*/
            /*---- TO DO ----*/
            /*---------------*/
            return Ok();
        }



        [SwaggerOperation(Summary = "Creates Project")]
        [HttpPost]
        public ActionResult<ProjectDto> CreateProject([FromBody] ProjectDto projectDto)
        {
            /*---------------*/
            /*---- TO DO ----*/
            /*---------------*/
            return Ok();
        }




        [SwaggerOperation(Summary = "Updates project - it's all properties")]
        [HttpPut("{id}")]
        public ActionResult<ProjectDto> UpdateProject([FromRoute] int id, [FromBody]ProjectDto projectDto)
        {
            /*---------------*/
            /*---- TO DO ----*/
            /*---------------*/
            return Ok();
        }




        [SwaggerOperation(Summary = "Updates project - only selected properties")]
        [HttpPatch("{id}")]
        public ActionResult<ProjectDto> LightUpdateProject([FromRoute] int id, [FromBody] ProjectDto projectDto)
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
            /*---------------*/
            /*---- TO DO ----*/
            /*---------------*/
            return Ok();
        }
    }
}
