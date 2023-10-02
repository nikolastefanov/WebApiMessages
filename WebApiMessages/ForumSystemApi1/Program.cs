
using ForumSystemApi1.Data;
using ForumSystemApi1.Jwt;
using ForumSystemApi1.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


var jwtSettingsSection=builder.Configuration.GetSection("JwtSettings");
builder.Services.Configure<JwtSettings>(jwtSettingsSection);


var jwtSettings = jwtSettingsSection.Get<JwtSettings>();
var key = Encoding.ASCII.GetBytes(jwtSettings.Secret);

var signingKey = new SymmetricSecurityKey(Encoding.UTF8
    .GetBytes(builder.Configuration["JwtSettings:Secret"]));

builder.Services.Configure<TokenProviderOptions>(options=>
{
    options.Audience = builder.Configuration["JwtSettings:Audience"];
    options.Issuer = builder.Configuration["JwtSettings:Issuer"];
    options.Path = "api/users/login";
    options.Expiration = TimeSpan.FromDays(15);
    options.SignigCredentials = new SigningCredentials(
        signingKey, SecurityAlgorithms.HmacSha256);
});



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

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

    

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
                new Message {User=new User{Username="Pesho" },Content="Scence",CreatedOn=DateTime.UtcNow},
                new Message {User=new User{Username="Ivan" },Content="Sport",CreatedOn=DateTime.UtcNow},
                new Message {User=new User{Username="Peter" }, Content="Technical",CreatedOn=DateTime.UtcNow},

        });

        dbContext.SaveChanges();
    }
}



app.UseHttpsRedirection();

app.UseCors();

app.UseAuthentication();

app.UseAuthorization();



app.MapControllers();

app.Run();
