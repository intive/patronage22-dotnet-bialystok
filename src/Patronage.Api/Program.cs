using NLog;
using NLog.Web;
using Patronage.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MediatR;
using Patronage.Contracts.Interfaces;
using Patronage.DataAccess.Services;
using System.Reflection;
using FluentValidation;
using Patronage.Contracts.ModelDtos.Projects;
using FluentValidation.AspNetCore;
using Patronage.DataAccess.Validators;
using Patronage.Common.Middleware;
using Patronage.DataAccess;




var builder = WebApplication.CreateBuilder(args);

try
{
    var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
    logger.Debug("Starting initializing");

    // Add services to the container.
    builder.Services.AddControllers().AddFluentValidation();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
    {
        c.EnableAnnotations();
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Patronage 2022 API", Version = "v1" });
    });

    builder.Services.AddDbContext<TableContext>((DbContextOptionsBuilder options) =>
    {
        options.UseSqlServer(
            builder.Configuration.GetConnectionString("Default"),
            x => x.MigrationsAssembly("Patronage.Migrations"));
    });


    builder.Services.AddScoped<IIssueService, IssueService>();
    builder.Services.AddScoped<IProjectService, ProjectService>();
    builder.Services.AddScoped<IValidator<CreateOrUpdateProjectDto>, CreateOrUpdateProjectDtoValidator>();

    builder.Services.AddMediatR(typeof(Program));

    builder.Services.AddScoped<ErrorHandlingMiddleware>();

    builder.Services.AddTransient<DataSeeder>();

    builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());

    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

    var app = builder.Build();

    SendData(app);

    void SendData(IHost app)
    {
        var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

        using (var scope = scopedFactory.CreateScope())
        {
            var service = scope.ServiceProvider.GetService<DataSeeder>();
            service.Seed();
        }
    }


    // Configure the HTTP request pipeline.
    /*if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Patronage 2022 API v1");
        });
    }*/
    //Temporarily let's run swagger on realease too
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Patronage 2022 API v1");
    });

    app.UseMiddleware<ErrorHandlingMiddleware>();

    app.UseHttpsRedirection();

    app.UseAuthorization();
    app.UseDeveloperExceptionPage();

    app.MapControllers();

    logger.Debug("Initializing complete!");
    string? port = Environment.GetEnvironmentVariable("PORT");
    if(port == null)
    {
        port = "80";
    }
    logger.Debug("App listening on port:" + port);

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
