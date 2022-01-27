using NLog;
using NLog.Web;
using Patronage.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MediatR;

var dblogger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
dblogger.Debug("init main dsfsdfs");

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
    });

    builder.Services.AddDbContext<TableContext>(options =>
    {
        options.UseSqlServer(
            builder.Configuration.GetConnectionString("DockerString"),
            x => x.MigrationsAssembly("Patronage.Migrations"));
    });

    builder.Services.AddMediatR(typeof(Program));

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    //Nlog
    builder.Logging.ClearProviders();
    builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
    builder.Host.UseNLog();
  


    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Patronage 2022 API v1");
        });
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();
    app.UseDeveloperExceptionPage();

    app.MapControllers();

    app.Run();
}
catch (Exception exception)
{
    // NLog: catch setup errors
    dblogger.Error(exception, "Stopped program because of exception");
    throw;
}
finally
{
    // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
    LogManager.Shutdown();
}
