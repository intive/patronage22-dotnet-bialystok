using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Patronage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            }
            else
            {
                _logger.LogInformation("Unable to connect to database");
                throw new Exception("Unable to connect to database");
            }
        }
    }
}
