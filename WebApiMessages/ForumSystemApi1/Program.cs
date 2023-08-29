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
builder.Services.AddCors(options =>
{
    options.AddPolicy("MessagesCORSPolicy",
        builder =>
        {
            builder.WithOrigins("http://localhost:8001")
                                    .AllowAnyHeader();
        });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
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
                new Message {Id="1",Content="Scence",CreatedOn=DateTime.UtcNow},
                new Message {Id="2",Content="Sport",CreatedOn=DateTime.UtcNow},
                new Message {Id="3",Content="Technical",CreatedOn=DateTime.UtcNow},

        });

        dbContext.SaveChanges();
    }
}

app.UseCors("MessagesCORSPolicy");

app.UseHttpsRedirection();
   
app.UseAuthorization();

app.MapControllers();

app.Run();
