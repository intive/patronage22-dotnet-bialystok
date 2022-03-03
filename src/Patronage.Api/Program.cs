using NLog;
using NLog.Web;
using Patronage.Models;
using Microsoft.OpenApi.Models;
using MediatR;
using Patronage.Contracts.Interfaces;
using Patronage.DataAccess.Services;
using FluentValidation;
using Patronage.Api;
using Patronage.Api.Middleware;
using Microsoft.AspNetCore.Identity;
using Patronage.Api.Controllers;

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Info("Starting");

try
{
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
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    DatabaseController databaseController = new DatabaseController((ILogger<DatabaseController>)logger, builder, builder.Configuration.GetValue("provider", "mysql"));

    builder.Services.AddScoped<IIssueService, IssueService>();
    builder.Services.AddScoped<IProjectService, ProjectService>();
    builder.Services.AddScoped<IBoardService, BoardService>();
    builder.Services.AddScoped<IBoardStatusService, BoardStatusService>();

    builder.Services.AddScoped<ErrorHandlingMiddleware>();

    builder.Services.AddMediatR(typeof(Program));

    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

    builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

    builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

    builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
        .AddEntityFrameworkStores<TableContext>()
        .AddDefaultTokenProviders();

    var app = builder.Build();

    databaseController.ApplyMigrations(app);

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment() || Environment.GetEnvironmentVariable("USE_SWAGGER") == "true")
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Patronage 2022 API v1");
        });
    }

    app.UseMiddleware<ErrorHandlingMiddleware>();

    app.UseHttpsRedirection();

    app.UseAuthentication();

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
    logger.Error(exception, "Stopped program because of exception");
    throw;
}
finally
{
    // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
    LogManager.Shutdown();
}
