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
            Project project = new()
            {
                Alias = "Project one",
                Name = "Project name",
                Description = "description",
            };
            _context.Projects.Add(project);
            _context.SaveChanges();

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
            Project project = new()
            {
                Alias = "No alias",
                Name = "This name is unique",
                Description = "Unique"
            };
            _context.Projects.Add(project);
            _context.SaveChanges();

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
            Project project = new()
            {
                Alias = "This alias is unique",
                Name = "No name",
                Description = "No description"
            };
            _context.Projects.Add(project);
            _context.SaveChanges();

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
            Project project = new()
            {
                Alias = "No alias",
                Name = "No description",
                Description = "This description is unique"
            };
            _context.Projects.Add(project);
            _context.SaveChanges();

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
            Project project = new()
            {
                Id = 10002,
                Alias = "Not updated alias",
                Name = "Not updated name",
                Description = "Not updated description"
            };
            _context.Projects.Add(project);
            _context.SaveChanges();
            UpdateProjectDto updatedProject = new()
            {
                Alias = "Updated alias",
                Name = "Updated name",
                Description = "Updated description"
            };

            //Act
            var response = await _projectService.Update(10002, updatedProject);
            var projectResponse= await _projectService.GetById(10002);

            //Assert
            Assert.True(response, "Could not update project.");
            Assert.NotNull(projectResponse);
            Assert.True(projectResponse!.Name == "Updated name", "Project name was not updated.");
            Assert.True(projectResponse!.Description == "Updated description", "Project description was not updated.");
            Assert.True(projectResponse!.Alias == "Updated alias", "Project alias was not updated.");
        }

        [Fact]
        public async Task CanLightUpdateProject()
        {
            //Arrange
            Project project = new()
            {
                Id = 10000,
                Alias = "Base alias",
                Name = "Base name",
                Description = "Base description"
            };
            _context.Projects.Add(project);
            _context.SaveChanges();
            PartialProjectDto updatedProject = new()
            {
                Alias = new PropInfo<string> { Data = "Updated alias" },
                Name = null,
                Description = new PropInfo<string> { Data = null }
            };

            //Act
            var response = await _projectService.LightUpdate(10000, updatedProject);
            var projectResponse = await _projectService.GetById(10000);
            
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
            Project project = new()
            {
                Id = 10001,
                Alias = "Not deleted alias",
                Name = "Not deleted name",
                Description = "Not deleted description"
            };
            _context.Projects.Add(project);
            _context.SaveChanges();

            //Act
            var response = await _projectService.Delete(10001);
            var responseAll = await _projectService.GetAll(null);
            var projectResponse = await _projectService.GetById(10001);

            //Assert
            Assert.True(response, "Could not delete project.");
            Assert.NotNull(projectResponse);
            Assert.True(projectResponse!.IsActive == false, "Project was not deleted.");
            Assert.True(responseAll!.Any(x => x.Id == 10001) || responseAll!.Any(x => x.IsActive == false), "Get all returned deleted project.");
        }
    }
}
