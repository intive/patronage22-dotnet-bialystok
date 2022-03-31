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
        public void CheckMigrationIntegrity()
        {
            var proj = _context.Projects.FirstOrDefault();
            Assert.NotNull(proj);
            Assert.Equal("1st", proj!.Alias);
            Assert.Equal("First project", proj.Name);
            Assert.True(proj.IsActive);

            var proj2 = _context.Projects.FirstOrDefault(x => x.Id == 2);
            Assert.NotNull(proj2);
            Assert.Equal("2nd", proj2!.Alias);
            Assert.Equal("Second test project", proj2.Name);
            Assert.False(proj2.IsActive);

            var proj3 = _context.Projects.FirstOrDefault(x => x.Id == 10);
            Assert.Null(proj3);

            var board = _context.Boards.FirstOrDefault();
            Assert.NotNull(board);
            Assert.Equal("First test Board", board!.Name);
            Assert.True(board.IsActive);
            Assert.Equal(board.ProjectId, proj.Id);

            var issue = _context.Issues.FirstOrDefault(x => x.Id == 1);
            Assert.NotNull(issue);
            Assert.NotEqual("Second test Issue", issue!.Name);
            Assert.True(issue.IsActive);
            Assert.Equal(issue.ProjectId, proj.Id);
            Assert.Equal(issue.BoardId, board.Id);

            var status = _context.Statuses.FirstOrDefault();
            Assert.NotNull(status);
            Assert.Equal("TO DO", status!.Code);

            var boardStatus = _context.BoardsStatus.FirstOrDefault();
            Assert.NotNull(boardStatus);
            Assert.Equal(proj.Id, boardStatus!.BoardId);
            Assert.Equal(status.Id, boardStatus.StatusId);

            var user = _context.Users.FirstOrDefault();
            Assert.NotNull(user);
        }
    }
}
