using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Moq;
using Patronage.Api.Controllers;
using Patronage.Api.MediatR.Projects.Queries;
using Patronage.Contracts.Interfaces;
using Patronage.Contracts.ModelDtos;
using Patronage.Contracts.ModelDtos.Projects;
using Patronage.DataAccess;
using Patronage.DataAccess.Services;
using Patronage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Patronage.Tests
{
    public class ProjectServiceUnitTest : IClassFixture<BaseTestFixture>
    {
        private readonly TableContext _context;
        private readonly ProjectService _projectService;

        public ProjectServiceUnitTest(BaseTestFixture fixture)
        {
            _context = fixture._context;
            _projectService = new ProjectService(_context);
        }

        [Fact]
        public async Task CanCreateProject()
        {
            //Arrange
            CreateProjectDto project = new()
            {
                Alias = "created",
                Name = "created name",
                Description = "created description"
            };
            
            //Act
            var response = await _projectService.Create(project);
            var projectExists = await _projectService.GetById(response);

            //Assert
            Assert.NotNull(projectExists);
            Assert.True(response > 0);
            Assert.Equal(project.Name, projectExists!.Name);
        }

        [Fact]
        public async Task CanGetProjects()
        {
            //Arrange
            CreateProjectDto project = new()
            {
                Alias = "Project one",
                Name = "Project name",
                Description = "description"
            };
            _projectService.Create(project).Wait();

            //Act
            var response = await _projectService.GetAll(null);
            
            //Assert
            Assert.NotNull(response);
            Assert.True(response!.Any(), "Could not load projects from project controller.");
            Assert.True(response.Any(x => x.Name == "Project name"), "Could not find project with name 'Project name'.");
        }

        [Fact]
        public async Task CanSearchProjectsByName()
        {
            //Arrange
            CreateProjectDto project = new()
            {
                Alias = "No alias",
                Name = "This name is unique",
                Description = "Unique"
            };
            _projectService.Create(project).Wait();

            //Act
            var response = await _projectService.GetAll("This name is unique");
            var response2 = await _projectService.GetAll("this name does not exist");            

            //Assert
            Assert.NotNull(response);
            Assert.True(response!.Any(), "Could not load projects from project service.");
            Assert.Equal("This name is unique", response!.First().Name);
            
            Assert.NotNull(response2);
            Assert.False(response2!.Any(), "Projects returned when they should not have been.");
        }

        [Fact]
        public async Task CanSearchProjectsByAlias()
        {
            //Arrange
            CreateProjectDto project = new()
            {
                Alias = "This alias is unique",
                Name = "No name",
                Description = "No description"
            };
            _projectService.Create(project).Wait();

            //Act
            var response = await _projectService.GetAll("This alias is unique");
            var response2 = await _projectService.GetAll("this name does not exist");

            //Assert
            Assert.NotNull(response);
            Assert.True(response!.Any(), "Could not load projects from project service.");
            Assert.Equal("This alias is unique", response!.First().Alias);

            Assert.NotNull(response2);
            Assert.False(response2!.Any(), "Projects returned when they should not have been.");
        }

        [Fact]
        public async Task CanSearchProjectsByDescription()
        {
            //Arrange
            CreateProjectDto project = new()
            {
                Alias = "No alias",
                Name = "No description",
                Description = "This description is unique"
            };
            _projectService.Create(project).Wait();

            //Act
            var response = await _projectService.GetAll("This description is unique");
            var response2 = await _projectService.GetAll("this description does not exist");

            //Assert
            Assert.NotNull(response);
            Assert.True(response!.Any(), "Could not load projects from project service.");
            Assert.Equal("This description is unique", response!.First().Description);

            Assert.NotNull(response2);
            Assert.False(response2!.Any(), "Projects returned when they should not have been.");
        }
        
        [Fact]
        public async Task CanUpdateProject()
        {
            //Arrange
            CreateProjectDto project = new()
            {
                Alias = "Not updated alias",
                Name = "Not updated name",
                Description = "Not updated description"
            };
            var projectId = await _projectService.Create(project);
            UpdateProjectDto updatedProject = new()
            {
                Alias = "Updated alias",
                Name = "Updated name",
                Description = "Updated description"
            };

            //Act
            var response = await _projectService.Update(projectId, updatedProject);
            var projectResponse= await _projectService.GetById(projectId);

            //Assert
            Assert.True(response, "Could not update project.");
            Assert.NotNull(projectResponse);
            Assert.True(projectResponse!.Name == "Updated name", "Project name was not updated.");
            Assert.True(projectResponse!.Description == "Updated description", "Project description was not updated.");
            Assert.True(projectResponse!.Alias == "Updated alias", "Project alias was not updated.");
        }

        //unit test for light update project
        [Fact]
        public async Task CanLightUpdateProject()
        {
            //Arrange
            CreateProjectDto project = new()
            {
                Alias = "Base alias",
                Name = "Base name",
                Description = "Base description"
            };
            var projectId = await _projectService.Create(project);
            PartialProjectDto updatedProject = new()
            {
                Alias = new PropInfo<string> { Data = "Updated alias" },
                Name = null,
                Description = new PropInfo<string> { Data = null }
            };

            //Act
            var response = await _projectService.LightUpdate(projectId, updatedProject);
            var projectResponse = await _projectService.GetById(projectId);
            
            //Assert
            Assert.True(response, "Could not light update project.");
            Assert.NotNull(projectResponse);
            Assert.True(projectResponse!.Name == "Base name", "Project name was updated, when it should not have been.");
            Assert.True(projectResponse!.Description == null, "Project description was not updated.");
            Assert.True(projectResponse!.Alias == "Updated alias", "Project alias was updated.");
            Assert.True(projectResponse!.ModifiedOn != null, "Project modified on was not updated.");
        }

        [Fact]
        public async Task CanDeleteProject()
        {
            //Arrange
            CreateProjectDto project = new()
            {
                Alias = "Not deleted alias",
                Name = "Not deleted name",
                Description = "Not deleted description"
            };
            var projectId = await _projectService.Create(project);

            //Act
            var response = await _projectService.Delete(projectId);
            var projectResponse = await _projectService.GetById(projectId);

            //Assert
            Assert.True(response, "Could not delete project.");
            Assert.NotNull(projectResponse);
            Assert.True(projectResponse!.IsActive == false, "Project was not deleted.");
        }
    }
}
