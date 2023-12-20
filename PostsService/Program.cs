using Microsoft.EntityFrameworkCore;
using PostsService.Data;
using PostsService.Extensions;
using PostsService.Service;
using PostsService.Service.IService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


// setup sql connection string
builder.Services.AddDbContext<PostContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("SQLServerConnection")));

// configure AutoMapper 
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// tell httpClient about baseURL
builder.Services.AddHttpClient("Comments", c => c.BaseAddress = new Uri(builder.Configuration.GetValue<string>("ServiceURL:CommentsBaseURL")));

// register for DI
builder.Services.AddScoped<IPost, PostService>();
builder.Services.AddScoped<IComment, CommentService>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// add extensions
builder.AddSwaggenGenExtension();
builder.AddAuth();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
