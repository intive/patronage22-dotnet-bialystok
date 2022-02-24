using NLog;
using NLog.Web;
using Patronage.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MediatR;
using Patronage.Contracts.Interfaces;
using Patronage.DataAccess.Services;
using Patronage.DataAccess;
using FluentValidation;
using Patronage.Api;
using Patronage.Api.Middleware;
using Npgsql;
using Patronage.Api.Validators;
using Patronage.Api.MediatR.Issues.Queries.GetIssues;


try
{
    var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
    logger.Info("Starting");

    var builder = WebApplication.CreateBuilder(args);
    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
    {
        c.EnableAnnotations();
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Patronage 2022 API", Version = "v1" });
        var filePath = Path.Combine(System.AppContext.BaseDirectory, "Patronage.Api.xml");
        c.IncludeXmlComments(filePath);
    });

    //TODO: move this to data seeder ~MZ
    //This determines which connection string are we going to use
    //If environmental variable "DATABASE_URL" is set we will build connection string to connect to remove database
    //Won't work on local! Else uses our "Default connection string.
    //Why do we have to build connection string dynamically? Heroku periodically changes credentials, so we have to keep up with that.
    logger.Info(builder.Configuration.GetConnectionString("Default"));
    if (Environment.GetEnvironmentVariable("DATABASE_URL") != null || builder.Configuration.GetValue("provider", "mysql").Equals("postgre", StringComparison.InvariantCultureIgnoreCase))
    {
        string connection_string = "";
        var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
        if (databaseUrl != null){
            logger.Info("Using remote database");
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

            connection_string = string_builder.ToString();
        }
        else
        {
            logger.Info("Using local PostgreSQL database");
            connection_string = builder.Configuration.GetConnectionString("DefaultPostgre");
        }

        builder.Services.AddDbContext<TableContext>((DbContextOptionsBuilder options) =>
        {
            options.UseNpgsql(
                connection_string,
                x => x.MigrationsAssembly("Patronage.MigrationsPostgre"));
        });
    }
    else
    {
        logger.Info("Using local MsSQL database");
        builder.Services.AddDbContext<TableContext>((DbContextOptionsBuilder options) =>
        {
            options.UseSqlServer(
                builder.Configuration.GetConnectionString("Default"),
                x => x.MigrationsAssembly("Patronage.Migrations"));
        });
    }

    builder.Services.AddScoped<IIssueService, IssueService>();
    builder.Services.AddScoped<IProjectService, ProjectService>();
    builder.Services.AddScoped<IBoardService, BoardService>();
    builder.Services.AddScoped<IBoardStatusService, BoardStatusService>();

    builder.Services.AddScoped<ErrorHandlingMiddleware>();

    builder.Services.AddTransient<DataSeeder>();

    builder.Services.AddMediatR(typeof(Program));

    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

    builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

    builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

    var app = builder.Build();

    ApplyMigrations();

    void ApplyMigrations()
    {
        logger.Info("Trying to apply migrations");
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        try
        {
            var db = services.GetRequiredService<TableContext>();
            if (!db.Database.CanConnect())
            {
                logger.Error("No database connection! Migrations not applied");
                return;
            }
            var pendingMigrations = db.Database.GetPendingMigrations();
            if (pendingMigrations.Any())
            {
                db.Database.Migrate();
                logger.Info($"{pendingMigrations.Count()} pending migrations applied");
            }
            else
            {
                logger.Info("No migrations need to be applied");
            }
            var lastAppliedMigration = (db.Database.GetAppliedMigrations()).Last();
            logger.Info($"You are on schema version: {lastAppliedMigration}");
        }
        catch (Exception ex)
        {
            logger.Error(ex);
        }
    }

    SendData(app);

    void SendData(IHost app)
    {
        var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

        if(scopedFactory == null)
        {
            return;
        }

        using var scope = scopedFactory.CreateScope();
        var service = scope.ServiceProvider.GetService<DataSeeder>();
        if(service != null)
        {
            service.Seed();
        }
    }


    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment() || Environment.GetEnvironmentVariable("USE_SWAGGER") == "true") //TODO try replacing this with staging
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Patronage 2022 API v1");
        });
    }

    app.UseMiddleware<ErrorHandlingMiddleware>();

    app.UseHttpsRedirection();

    app.UseAuthorization();
    // ErrorHandlingMiddleware does not work if UseDeveloperExceptionPage is enabled so I commented it
    //app.UseDeveloperExceptionPage();

    app.MapControllers();

    logger.Info("Initializing complete!");
    string? port = Environment.GetEnvironmentVariable("PORT") ?? "80";
    logger.Info("App listening on port:" + port);

    app.Run();

}
catch (Exception exception)
{
    string type = exception.GetType().Name;
    if (type.Equals("StopTheHostException", StringComparison.Ordinal))
    {
        throw;
    }
    // NLog: catch setup errors
        throw;
}
finally
{
    // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
    LogManager.Shutdown();
}
