using Patronage.Models;
using Microsoft.EntityFrameworkCore;


using Microsoft.OpenApi.Models;
using MediatR;
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

builder.Services.AddDbContext<TableContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DockerString"),
        x => x.MigrationsAssembly("Patronage.Migrations"));
});

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
