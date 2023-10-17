using TodoApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.Extensions.DependencyInjection;
using TodoApi.Data;
using System.Linq;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<TodoApiContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("TodoApiContext")));
/*
 builder.Services.AddDbContext<TodoApiContext>(
                options => options.UseSqlServer(builder.Configuration.GetConnectionString("TodoApiContext"))); ;
*/

/*
builder.Services.AddDbContext<TodoApiContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString(
        "TodoApiContext") ?? throw new InvalidOperationException("Connection string 'TodoApiContext' not found.")));
*/

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


using (var serviceScope = app.Services.CreateScope())
{
      
    var dbContext = serviceScope.ServiceProvider.GetRequiredService<TodoApiContext>();
    dbContext.Database.EnsureCreated();
}



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
