using AutoMapper;
using Patronage.Contracts.Interfaces;
using Patronage.Contracts.ModelDtos.Projects;
using Patronage.Contracts.ModelDtos;
using Patronage.Models;
using Microsoft.EntityFrameworkCore;

namespace Patronage.DataAccess.Services
{
    public class ProjectService : IProjectService
    {
        private readonly TableContext _dbContext;


        public ProjectService(TableContext context)
        {
            _dbContext = context;
        }




        public async Task<int> Create(CreateProjectDto projectDto)
        {
            Project newProject = new Project();

            newProject.Name = projectDto.Name;
            newProject.Alias = projectDto.Alias;
            newProject.Description = projectDto.Description;
            newProject.IsActive = projectDto.IsActive;

            await _dbContext.Projects.AddAsync(newProject);
            await _dbContext.SaveChangesAsync();

            return newProject.Id;
        }



        public async Task<IEnumerable<ProjectDto>> GetAll(string searchedPhrase)
        {
            var  projects = _dbContext
                .Projects
                .Where(p => searchedPhrase == null || (p.Name.Contains(searchedPhrase)) ||
                                                       p.Alias.Contains(searchedPhrase) ||
                                                       p.Description.Contains(searchedPhrase))
                .ToList();


            var projectsDto = new List<ProjectDto>();

            foreach (var project in projects)
            {
                projectsDto.Add(new ProjectDto
                {
                    Id = project.Id,
                    Name = project.Name,
                    Alias = project.Alias,
                    Description = project.Description,
                    IsActive = project.IsActive,
                    CreatedOn = project.CreatedOn,
                    ModifiedOn = project.ModifiedOn
                });
            }

            return projectsDto;
        }



        public async Task<ProjectDto> GetById(int id)
        {
            var project = _dbContext
                .Projects
                .FirstOrDefault(p => p.Id == id);

            if (project is null) return null;


            ProjectDto projectDto = new ProjectDto
            {
                Id = project.Id,
                Name = project.Name,
                Alias = project.Alias,
                Description = project.Description,
                IsActive = project.IsActive,
                CreatedOn = project.CreatedOn,
                ModifiedOn = project.ModifiedOn
            };

            return projectDto;
        }



        public async Task<bool> Update(int id, UpdateProjectDto projectDto)
        {
            var project = _dbContext
                .Projects
                .FirstOrDefault(p => p.Id == id);

            if (project is null) return false;

            project.Name = projectDto.Name;
            project.Alias = projectDto.Alias;
            project.Description = projectDto.Description;
            project.IsActive = projectDto.IsActive;

            await _dbContext.SaveChangesAsync();

            return true;
        }



        public async Task<bool> LightUpdate(int id, PartialProjectDto projectDto)
        {
            var project = _dbContext
                .Projects
                .FirstOrDefault(p => p.Id == id);

            if (project is null) return false;


            if (projectDto.Description == null) project.Description = project.Description;
            else if (projectDto.Description?.Data == null && projectDto.Description != null) project.Description = null;
            else project.Description = projectDto.Description.Data;

            if (projectDto.Name == null) project.Name = project.Name;
            else if (projectDto.Name?.Data == null && projectDto.Name != null) return false;
            else if (_dbContext.Projects.Any(p => p.Name == projectDto.Name.Data)) return false;
            else project.Name = projectDto.Name.Data;

            if (projectDto.Alias == null) project.Alias = project.Alias;
            else if (projectDto.Alias?.Data == null && projectDto.Alias != null) return false;
            else if (_dbContext.Projects.Any(p => p.Alias == projectDto.Alias.Data)) return false;
            else project.Alias = projectDto.Alias.Data;

            if (projectDto.IsActive == null) project.IsActive = project.IsActive;
            else if (projectDto.IsActive?.Data == null && projectDto.IsActive != null) return false;
            else project.IsActive = projectDto.IsActive.Data;


            await _dbContext.SaveChangesAsync();

            return true;
        }



        public async Task<bool> Delete(int id)
        {
            var project = _dbContext
                .Projects
                .FirstOrDefault(p => p.Id == id);

            if (project is null) return false;

            project.IsActive = false;

            await _dbContext.SaveChangesAsync();

            return true;
        }

    }
}
