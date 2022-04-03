using Patronage.Api.MediatR.Issues.Commands;
using Patronage.Api.MediatR.Issues.Queries;
using Patronage.Contracts.Helpers;
using Patronage.Contracts.Interfaces;
using Patronage.Contracts.ModelDtos;
using Patronage.Contracts.ModelDtos.Issues;
using Patronage.DataAccess.Services;
using Patronage.Models;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Patronage.Tests
{
    public class IssueControllerTests : IClassFixture<BaseTestFixture>
    {
        private readonly TableContext _dbContext;
        private readonly IIssueService _issueService;

        public IssueControllerTests(BaseTestFixture fixture)
        {
            _dbContext = fixture._context;
            _issueService = new IssueService(_dbContext);
        }

        [Fact]
        public async Task GetAll_Issues_ReturnPageResult()
        {
            // arrange
            FilterIssueDto filter = new()
            {
                PageNumber = 1,
                PageSize = 10
            };

            GetIssuesListQuery query = new(filter);
            GetIssuesListQueryHandler handler = new(_issueService);

            // act
            var result = await handler.Handle(query, new CancellationToken());

            // assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<PageResult<IssueDto>>(result);
        }

        [Fact]
        public async Task GetById_Issue_ReturnIssue()
        {
            // arrange
            var id = 1;

            GetSingleIssueQuery query = new(id);
            GetSingleIssueQueryHandler handler = new(_issueService);

            // act
            var result = await handler.Handle(query, new CancellationToken());

            // assert
            Assert.NotNull(result);
            Assert.Equal(id, result!.Id);
        }

        [Fact]
        public async Task Create_Issue_ReturnNewIssue()
        {
            // arrange
            BaseIssueDto dto = new()
            {
                Alias = "Issue alias",
                Name = "Name",
                Description = "Description of new issue",
                ProjectId = 1,
                StatusId = 2
            };

            CreateIssueCommand command = new(dto);
            CreateIssueCommandHandler handler = new(_issueService);

            // act
            var result = await handler.Handle(command, new CancellationToken());

            // assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IssueDto>(result);
        }

        [Fact]
        public async Task Update_Issue_ReturnTrue()
        {
            // arrange
            var id = 1;
            BaseIssueDto dto = new()
            {
                Alias = "Update issue alias",
                Name = "Update name",
                Description = "Description of update issue",
                ProjectId = 1,
                StatusId = 2
            };

            UpdateIssueCommand command = new(id, dto);
            UpdateIssueCommandHandler handler = new(_issueService);

            // act
            var result = await handler.Handle(command, new CancellationToken());

            // assert
            Assert.True(result);
        }

        [Fact]
        public async Task UpdateLight_Issue_ReturnTrue()
        {
            // arrange
            var id = 1;
            var dto = new PartialIssueDto()
            {
                Description = new PropInfo<string>()
            };
            dto.Description.Data = "Description of light update issue";

            UpdateLightIssueCommand command = new(id, dto);
            UpdateLightIssueCommandHandler handler = new(_issueService);

            // act
            var result = await handler.Handle(command, new CancellationToken());

            // assert
            Assert.True(result);
        }

        [Fact]
        public async Task Delete_Issue_ReturnTrue()
        {
            // arrange
            var id = 1;

            DeleteIssueCommand command = new(id);
            DeleteIssueCommandHandler handler = new(_issueService);

            // act
            var result = await handler.Handle(command, new CancellationToken());

            // assert
            Assert.True(result);
        }

        [Fact]
        public async Task AssignUserWithNull_Issue_ReturnTrue()
        {
            // arrange
            var id = 1;
            string? userId = null;

            AssignIssueCommand command = new(id, userId);
            AssignIssueCommandHandler handler = new(_issueService);

            // act
            var result = await handler.Handle(command, new CancellationToken());

            // assert
            Assert.True(result);
        }

        [Fact]
        public async Task AssignUserWithId_Issue_ReturnTrue()
        {
            // arrange
            var id = 1;
            string userId = "679381f2-06a1-4e22-beda-179e8e9e3236";
        
            AssignIssueCommand command = new(id, userId);
            AssignIssueCommandHandler handler = new(_issueService);

            // act
            var result = await handler.Handle(command, new CancellationToken());

            // assert
            Assert.True(result);
        }
    }
}
