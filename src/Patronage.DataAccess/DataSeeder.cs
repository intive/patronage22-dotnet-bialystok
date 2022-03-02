using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
        }
    }
}
