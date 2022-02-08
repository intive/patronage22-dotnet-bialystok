using NLog;
using NLog.Web;
using Patronage.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MediatR;
using Patronage.Contracts.Interfaces;
using Patronage.Models.Services;
using Patronage.DataAccess.Services;
using System.Reflection;
using Patronage.Common.Middleware;
using Patronage.DataAccess;

try
{
    var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
    logger.Debug("init main");

    var builder = WebApplication.CreateBuilder(args);
    builder.Services.AddControllers();
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
    if (app.Environment.IsDevelopment())
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
    app.UseDeveloperExceptionPage();

    app.MapControllers();

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
