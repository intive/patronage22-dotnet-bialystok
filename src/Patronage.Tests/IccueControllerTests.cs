using Patronage.Contracts.Interfaces;
using Patronage.Contracts.ModelDtos.Issues;
using Patronage.DataAccess.Services;
using Patronage.Models;
using System.Threading.Tasks;
using Xunit;

namespace Patronage.Tests
{
    public class IccueControllerTests
    {
        private readonly TableContext _context;
        private readonly IIssueService _issueService;

        public IccueControllerTests(BaseTestFixture fixture)
        {
            _context = fixture._context;
            _issueService = new IssueService(_context);
        }

        [Fact]
        public async Task Iccue_GetAll_ReturnIssues()
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
        public void Iccue_GetById_ReturnIssue()
        {
            // arrange

            // act

            // assert

        }

        [Fact]
        public async Task Iccue_Create_ReturnNewIssue()
        {
            // arrange
            BaseIssueDto issue = new()
            {
                Alias = "Issue alias",
                Name = "Name",
                Description = "Description of new issue",
                ProjectId = 1,
                StatusId = 1
            };

            // act
            var result = await _issueService.CreateAsync(issue);

            // assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Iccue_Update_ReturnMessageSuccessfully()
        {
            // arrange

            // act

            // assert

        }

        [Fact]
        public void Iccue_UpdateLight_ReturnTrue()
        {
            // arrange

            // act

            // assert

        }

        [Fact]
        public void Iccue_Delete_ReturnTrue()
        {
            // arrange

            // act

            // assert

        }

        [Fact]
        public void Iccue_Assign_ReturnReturnTrue()
        {
            // arrange

            // act

            // assert

        }
    }
}
