using ForumSystemApi1.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.Extensions.DependencyInjection;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/*
builder.Services.AddCors(options =>
{
    options.AddPolicy("MessagesCORSPolicy",
        builder =>
        {
            builder.WithOrigins("http://127.0.0.1:5500/")
                                    .AllowAnyHeader();
        });
});
*/

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddCors(options =>
    options.AddDefaultPolicy(polisy =>
    {
        polisy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();

    }));
}



var app = builder.Build();

// Configure the HTTP request pipeline.wq][qw
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var serviceScope = app.Services.CreateScope())
{
   
    var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.EnsureCreated();


    if (!dbContext.Messages.Any())
    {
        dbContext.Messages.AddRange(new[]
        {
                new Message {User="Pesho",Content="Scence",CreatedOn=DateTime.UtcNow},
                new Message {User="Ivan",Content="Sport",CreatedOn=DateTime.UtcNow},
                new Message {User="Peter",Content="Technical",CreatedOn=DateTime.UtcNow},

        });

        dbContext.SaveChanges();
    }
}



app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
