using Patronage.Contracts.Interfaces;
using Patronage.Contracts.ModelDtos.Issues;
using Patronage.DataAccess.Services;
using Patronage.Models;
using System.Threading.Tasks;
using Xunit;

namespace Patronage.Tests
{
    public class IssueControllerTests
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

            // act
            var result = await _issueService.GetAllIssuesAsync(filter);

            // assert
            Assert.NotNull(result);
        }

        [Fact]
        public void GetById_Issue_ReturnIssue()
        {
            // arrange

            // act

            // assert

        }

        [Fact]
        public async Task Create_Issue_ReturnNewIssue()
        {
            // arrange
            BaseIssueDto issue = new()
            {
                Alias = "Issue alias",
                Name = "Name",
                Description = "Description of new issue",
                ProjectId = 1,
                StatusId = 2
            };

            // act
            var result = await _issueService.CreateAsync(issue);

            // assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Update_Issue_ReturnTrue()
        {
            // arrange

            // act

            // assert

        }

        [Fact]
        public void UpdateLight_Issue_ReturnTrue()
        {
            // arrange

            // act

            // assert

        }

        [Fact]
        public void Delete_Issue_ReturnTrue()
        {
            // arrange

            // act

            // assert

        }

        [Fact]
        public void AssignUser_Issue_ReturnTrue()
        {
            // arrange

            // act

            // assert

        }
    }
}
