using AutoMapper;
using Patronage.Common.Entities;
using Patronage.Contracts.ModelDtos;
using Patronage.Contracts.Interfaces;
using Patronage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patronage.DataAccess.Services
{
    public class ProjectService : IProjectService
    {
        private readonly TableContext _dbContext;
        private readonly IMapper _mapper;

        // I had to add project reference Patronage.DataAccess -> Patronage.Models
        // to can inject dependency to database context. Don't know if it was good solution.
        public ProjectService(TableContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }




        public int Create(ProjectDto projectDto)
        {
            var newProject = _mapper.Map<Project>(projectDto);
            newProject.IsActive = true;
            newProject.CreatedOn = DateTime.UtcNow;

            _dbContext.Projects.Add(newProject);
            _dbContext.SaveChanges();

            return newProject.Id;
        }




        public IEnumerable<ProjectDto> GetAll()
        {
            var projects = _dbContext
                .Projects
                .ToList();

            var projectsDto = _mapper.Map<List<ProjectDto>>(projects);

            return projectsDto;
        }



        public ProjectDto GetById(int id)
        {
            var project = _dbContext
                .Projects
                .FirstOrDefault(p => p.Id == id);

            if (project is null) return null;

            var projectDto = _mapper.Map<ProjectDto>(project);

            return projectDto;
        }



        public bool Update(int id, ProjectDto projectDto)
        {
            var project = _dbContext
                .Projects
                .FirstOrDefault(p => p.Id == id);

            if (project is null) return false;

            project.Alias = projectDto.Alias;
            project.Name = projectDto.Name;
            project.Description = projectDto.Description;
            project.ModifiedOn = DateTime.UtcNow;

            _dbContext.SaveChanges();

            return true;
        }



        public bool Delete(int id)
        {
            var project = _dbContext
                .Projects
                .FirstOrDefault(p => p.Id == id);

            if (project is null) return false;

            project.IsActive = false;

            _dbContext.SaveChanges();

            return true;
        }

    }
}
