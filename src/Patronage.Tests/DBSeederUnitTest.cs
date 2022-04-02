using Patronage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Patronage.Tests
{
    public class DBSeederUnitTest : IClassFixture<BaseTestFixture>
    {
        private readonly BaseTestFixture fixture;
        private readonly TableContext _context;
        public DBSeederUnitTest(BaseTestFixture fixture)
        {
            this.fixture = fixture;
            _context = fixture._context;
        }

        [Fact]
        public void HasSeededProjects()
        {
            //Arrange
            var proj = _context.Projects.FirstOrDefault();
            var proj2 = _context.Projects.FirstOrDefault(x => x.Id == 2);
            var proj3 = _context.Projects.FirstOrDefault(x => x.Id == 10);
            //Act

            //Assert
            Assert.NotNull(proj);
            Assert.Equal("1st", proj!.Alias);
            Assert.Equal("First project", proj.Name);
            Assert.True(proj.IsActive);

            Assert.NotNull(proj2);
            Assert.Equal("2nd", proj2!.Alias);
            Assert.Equal("Second test project", proj2.Name);
            Assert.False(proj2.IsActive);

            Assert.Null(proj3);
        }

        [Fact]
        public void HasSeededBoards()
        {
            //Arrange
            int id = 1;
            var board = _context.Boards.FirstOrDefault();

            //Act

            //Assert
            Assert.NotNull(board);
            Assert.Equal("First test Board", board!.Name);
            Assert.True(board.IsActive);
            Assert.Equal(board.ProjectId, id);
        }

        [Fact]
        public void HasSeededIssues()
        {
            //Arrange
            var issue = _context.Issues.FirstOrDefault(x => x.Id == 1);
            var projId = 1;
            var boardId = 1;

            //Act

            //Assert
            Assert.NotNull(issue);
            Assert.NotEqual("Second test Issue", issue!.Name);
            Assert.True(issue.IsActive);
            Assert.Equal(issue.ProjectId, projId);
            Assert.Equal(issue.BoardId, boardId);
        }


        [Fact]
        public void HasSeededStatuses()
        {
            //Arrange
            var status = _context.Statuses.FirstOrDefault();

            //Act

            //Assert
            Assert.NotNull(status);
            Assert.Equal("TO DO", status!.Code);
        }

        [Fact]
        public void HasSeededBoardStatuses()
        {
            //Arrange
            var boardStatus = _context.BoardsStatus.FirstOrDefault();
            var projId = 1;
            var statusId = 1;

            //Act

            //Assert
            Assert.NotNull(boardStatus);
            Assert.Equal(projId, boardStatus!.BoardId);
            Assert.Equal(statusId, boardStatus.StatusId);
        }

        [Fact]
        public void HasSeededUsers()
        {
            //Arrange
            var user = _context.Users.FirstOrDefault();

            //Act

            //Assert
            Assert.NotNull(user);
        }
    }
}
