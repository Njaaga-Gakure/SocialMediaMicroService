using AuthService.Data;
using AuthService.Models;
using AuthService.Service;
using AuthService.Service.IService;
using AuthService.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// setup sql connection string
builder.Services.AddDbContext<SocialContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("SQLServerConnection")));

// identity framework config
builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<SocialContext>();

// configure Auto Mapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// register user service for dependency injection
builder.Services.AddScoped<IUser, UserService>();
builder.Services.AddScoped<IJWT, JWTService>();

// register jwt options class as a service
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtOptions"));


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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
