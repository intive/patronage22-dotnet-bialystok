using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Npgsql;
using Patronage.DataAccess;
using Patronage.Models;

namespace Patronage.Api.Controllers
{
    public class DatabaseController
    {
        private readonly ILogger<DatabaseController> _logger;
        private readonly WebApplicationBuilder _builder;

        public DatabaseController(ILogger<DatabaseController> logger, WebApplicationBuilder builder, string provider = "mysql")
        {
            _logger = logger;
            _builder = builder;
            ConnectToDb(provider);
        }

        //This will indirectly call seeding, see TableContext.cs for the seeding function
        private void ConnectToDb(string provider)
        {
            if (provider.Equals("postgre", StringComparison.InvariantCultureIgnoreCase))
            {
                _logger.LogInformation("Using PostgreSQL provider");
                ConnectToPostgre();
            }
            else
            {
                _logger.LogInformation("Using MySQL provider");
                 ConnectToMysql();
            }
        }

        private void ConnectToPostgre()
        {
            string connection_string;
            _logger.LogInformation("Using PostgreSQL database");
            if (Environment.GetEnvironmentVariable("DATABASE_URL") != null)
            {
                _logger.LogInformation("Using remote database, generating connection string");
                connection_string = BuildPostrgreConnectionString(Environment.GetEnvironmentVariable("DATABASE_URL")!);
            }
            else
            {
                _logger.LogInformation("No connection specified, defaulting to config's connection string");
                connection_string = BuildPostrgreConnectionString(_builder.Configuration.GetConnectionString("DefaultPostgre"));
            }

            _builder.Services.AddDbContext<TableContext>((DbContextOptionsBuilder options) =>
            {
                options.UseNpgsql(
                    connection_string,
                    x => x.MigrationsAssembly("Patronage.MigrationsPostgre"));
            });
        }

        private static string BuildPostrgreConnectionString(string databaseUrl)
        {
            var databaseUri = new Uri(databaseUrl);
            var userInfo = databaseUri.UserInfo.Split(':');

            var string_builder = new NpgsqlConnectionStringBuilder
            {
                Host = databaseUri.Host,
                Port = databaseUri.Port,
                Username = userInfo[0],
                Password = userInfo[1],
                Database = databaseUri.LocalPath.TrimStart('/')
            };

            return string_builder.ToString();
        }

        private void ConnectToMysql()
        {
            string connectionString = "";
            _logger.LogInformation("Using MsSQL database");
            connectionString = _builder.Configuration.GetConnectionString("Default");
            _logger.LogInformation("Using default connection string");
            _builder.Services.AddDbContext<TableContext>((DbContextOptionsBuilder options) =>
            {
                options.UseSqlServer(
                    connectionString,
                    x => x.MigrationsAssembly("Patronage.Migrations"));
            });
        }

        public void ApplyMigrations(IHost app)
        {
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var _dbContext = services.GetRequiredService<TableContext>();
            if (!_dbContext.Database.CanConnect())
            {
                _logger.LogError("Trying to apply migrations without database connected");
                return;
            }

            _logger.LogInformation("Applying migrations");
            var db = _dbContext.Database;
            try
            {
                var pendingMigrations = db.GetPendingMigrations();
                if (pendingMigrations.Any())
                {
                    db.Migrate();
                    _logger.LogInformation($"{pendingMigrations.Count()} pending migrations applied");
                }
                else
                {
                    _logger.LogInformation("No migrations need to be applied");
                }
                var lastAppliedMigration = (db.GetAppliedMigrations()).Last();
                _logger.LogInformation($"You are on schema version: {lastAppliedMigration}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }
        }
    }
}