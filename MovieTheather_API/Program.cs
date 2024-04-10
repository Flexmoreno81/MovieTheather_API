using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.EntityFrameworkCore;
using Movie_Theater_Model;
using Movie_Theater_Model.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connectionString = builder.Configuration.GetConnectionString("movie_connection");


builder.Services.AddDbContext<MovieTheatherContext>(options => options.UseSqlServer(connectionString));

builder.Services.Configure<RouteOptions>(options =>
{
    options.ConstraintMap.Add("TimeOnly", typeof(TimeOnlyRouteConstraint));
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
