using CodePulseAPI.Data;
using CodePulseAPI.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//before build, inject
builder.Services.AddDbContext<CodePulseDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("CodePulseConnString"));
});

builder.Services.AddScoped<ICategoryRepository, CategoryRepoImplement>();
builder.Services.AddScoped<IBlogRepository, BlogRepoImplement>();

builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddCors((options) =>
{
    options.AddPolicy("AngularApp", (policy) =>
    {
        policy.WithOrigins("http://localhost:4200") //accepting requests running in this host.- a slash shudnt be given at the end.
        .AllowAnyHeader()
        .WithMethods("POST", "GET", "PUT", "DELETE") //accepting these 4 types of requests.
        .WithHeaders("*");
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AngularApp"); //using the above defined CORS

app.UseAuthorization();

app.MapControllers();

app.Run();
