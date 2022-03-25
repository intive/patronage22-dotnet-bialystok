using Microsoft.EntityFrameworkCore;
using NLog;
using Npgsql;
using Patronage.Models;

namespace Patronage.Api.Controllers
{
    public class DatabaseManager
    {
        private readonly Logger _logger;
        private readonly WebApplicationBuilder _builder;

        public DatabaseManager(Logger logger, WebApplicationBuilder builder, string provider = "mssql")
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
                _logger.Info("Using PostgreSQL provider");
                ConnectToPostgre();
            }
            else
            {
                _logger.Info("Using MsSQL provider");
                ConnectToMsSQL();
            }
        }

        private void ConnectToPostgre()
        {
            string connection_string;
            if (Environment.GetEnvironmentVariable("DATABASE_URL") != null)
            {
                _logger.Info("Using remote database, generating connection string");
                connection_string = BuildPostrgreConnectionString(Environment.GetEnvironmentVariable("DATABASE_URL")!);
            }
            else
            {
                _logger.Info("No connection specified, defaulting to config's connection string");
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

        private void ConnectToMsSQL()
        {
            string connectionString = "";
            connectionString = _builder.Configuration.GetConnectionString("Default");
            _logger.Info("Using default connection string");
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
                _logger.Error("Trying to apply migrations without database connected");
                return;
            }

            _logger.Info("Applying migrations");
            var db = _dbContext.Database;
            try
            {
                var pendingMigrations = db.GetPendingMigrations();
                if (pendingMigrations.Any())
                {
                    db.Migrate();
                    _logger.Info($"{pendingMigrations.Count()} pending migrations applied");
                }
                else
                {
                    _logger.Info("No migrations need to be applied");
                }
                var lastAppliedMigration = (db.GetAppliedMigrations()).Last();
                _logger.Info($"You are on schema version: {lastAppliedMigration}");
            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());
            }
        }
    }
}