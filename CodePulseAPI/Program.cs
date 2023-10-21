using CodePulseAPI.Data;
using CodePulseAPI.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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

builder.Services.AddDbContext<AuthDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("CodePulseConnString"));
});

builder.Services.AddScoped<ICategoryRepository, CategoryRepoImplement>();
builder.Services.AddScoped<IBlogRepository, BlogRepoImplement>();
builder.Services.AddScoped<ITokenRepository, TokenRepoImplement>();

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

builder.Services.AddIdentityCore<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("CodePulse")
    .AddEntityFrameworkStores<AuthDbContext>()
    .AddDefaultTokenProviders();

//defining rules for Password.
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = false;
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters
    {
        AuthenticationType = "Jwt",
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
