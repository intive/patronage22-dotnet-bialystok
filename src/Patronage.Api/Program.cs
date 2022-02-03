using Patronage.Models;
using Microsoft.EntityFrameworkCore;


using Microsoft.OpenApi.Models;
using MediatR;
using Patronage.Contracts.Interfaces;
using Patronage.Models.Services;
using Patronage.Common.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Patronage 2022 API", Version = "v1" });
});

builder.Services.AddDbContext<Patronage.Models.TableContext>((DbContextOptionsBuilder options) =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("Default"),
        x => x.MigrationsAssembly("Patronage.Migrations"));
});


builder.Services.AddScoped<IIssueService, IssueService>();



builder.Services.AddScoped<ErrorHandlingMiddleware>();

builder.Services.AddMediatR(typeof(Program));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Patronage 2022 API v1");
    });
}

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
