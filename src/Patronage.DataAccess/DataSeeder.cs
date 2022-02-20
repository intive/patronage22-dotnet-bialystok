﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Patronage.Models;

namespace Patronage.DataAccess
{
    public class DataSeeder
    {
        private readonly TableContext _dbContext;
        private readonly ILogger<DataSeeder> _logger;

        public DataSeeder(TableContext dbContext, ILogger<DataSeeder> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public void Seed()
        {
            _logger.LogInformation(_dbContext.Database.GetConnectionString());
            if (_dbContext.Database.CanConnect())
            {
                _logger.LogInformation("Connected to Database");
                var pendingMigrations = _dbContext.Database.GetPendingMigrations();
                if (pendingMigrations != null && pendingMigrations.Any())
                {
                    _logger.LogInformation("Applying Migration");
                    _dbContext.Database.Migrate();
                }

                /*----------------------------------------------------*/
                /*--------------- Seeding test projects --------------*/
                /*----------------------------------------------------*/
                if (!_dbContext.Projects.Any())
                {
                    _dbContext.Projects.AddRange(produceTestProjects());
                    _dbContext.SaveChanges();
                }


                /*----------------------------------------------------*/
                /*--------------- Seeding test boards ----------------*/
                /*----------------------------------------------------*/
                if (!_dbContext.Boards.Any())
                {
                    _dbContext.Boards.AddRange(produceTestBoards());
                    _dbContext.SaveChanges();
                }


                /*----------------------------------------------------*/
                /*--------------- Seeding test issues ----------------*/
                /*----------------------------------------------------*/
                if (!_dbContext.Issues.Any())
                {
                    _dbContext.Issues.AddRange(produceTestIssues());
                    _dbContext.SaveChanges();
                }

                /*----------------------------------------------------*/
                /*-------------- Seeding test statuses ---------------*/
                /*----------------------------------------------------*/
                //if (!_dbContext.Statuses.Any())
                //{
                //    _dbContext.Boards.AddRange(produceTestStatuses());
                //    _dbContext.SaveChanges();
                //}


                /*----------------------------------------------------*/
                /*----------- Seeding test BoardStatuses -------------*/
                /*----------------------------------------------------*/
                //if (!_dbContext.BoardStatuses.Any())
                //{
                //    _dbContext.Issues.AddRange(produceTestBoardStatuses());
                //    _dbContext.SaveChanges();
                //}


                /*----------------------------------------------------*/
                /*------------- Seeding test Admin User --------------*/
                /*----------------------------------------------------*/
                //if (!_dbContext.Users.Any())
                //{
                //    _dbContext.Users.AddRange(produceTestUsers());
                //    _dbContext.SaveChanges();
                //}
            }
            else
            {
                _logger.LogInformation("Unable to connect to database");
                throw new Exception("Unable to connect to database");
            }
        }



        private IEnumerable<Project> produceTestProjects()
        {
            var projects = new List<Project>()
            {
                new Project()
                {
                    Name = "First project",
                    Alias = "1st",
                    Description = "This is a description of first test project",
                    IsActive = true
                },

                new Project()
                {
                    Name = "Second test project",
                    Alias = "2nd",
                    Description = "This is a description of 2nd test project",
                    IsActive = false
                },

                new Project()
                {
                    Name = "Third test project",
                    Alias = "3rd",
                    Description = null,
                    IsActive = false
                },
            };

            return projects;
        }


        private IEnumerable<Board> produceTestBoards()
        {
            var boards = new List<Board>()
            {
                new Board()
                {
                    Name = "First test Board",
                    Alias = "1st board",
                    Description = "This is a description of first test board",
                    ProjectId = 1,
                    IsActive = true
                },

                new Board()
                {
                    Name = "Second test Board",
                    Alias = "2nd board",
                    Description = "This is a description of second test board",
                    ProjectId = 1,
                    IsActive = false
                },
            };

            return boards;
        }



        private IEnumerable<Issue> produceTestIssues()
        {
            var issues = new List<Issue>()
            {
                new Issue
                {
                    Name = "First test Issue",
                    Alias = "1st issue",
                    Description = "This is a description of first test issue. This Issue is connected to a Board",
                    ProjectId = 1,
                    StatusId = 1,
                    BoardId = 1,
                    IsActive = true
                },

                new Issue
                {
                    Name = "Second test Issue",
                    Alias = "2nd issue",
                    Description = "This is a description of second test issue. This Issue isn't connected to a Board",
                    ProjectId = 1,
                    StatusId = 1,
                    BoardId = null,
                    IsActive = true
                },
            };

            return issues;
        }

/*
        private IEnumerable<Status> produceTestStatuses()
        {
            var statuses = new List<Status>()
            {
                new Status
                {

                },

                new Status
                {

                }
            };

            return statuses;
        }



        private IEnumerable<BoardStatus> produceTestBoardStatuses()
        {
            var boardStatuses = new List<BoardStatus>()
            {
                new BoardStatus
                {

                },

                new BoardStatus
                {

                }
            };

            return boardStatuses;
        }


        private IEnumerable<User> produceTestUsers()
        {
            var users = new List<User>()
            {
                new User
                {

                },

                new User
                {

                }
            };

            return users;
        }
*/

    }
}
