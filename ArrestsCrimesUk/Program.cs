using System.Text;
using ArrestsCrimesUk.Options;
using ArrestsCrimesUk.Services;
using AuthDataLayer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RepositoryService;
using Swashbuckle.Swagger;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//DB
var authConnectionString = builder.Configuration.GetConnectionString("AuthConnection");
var defConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AuthDbContext>(options =>
    options.UseSqlServer(authConnectionString));

builder.Services.AddDefaultIdentity<IdentityUser>(options =>
    {
        options.Password.RequiredLength = 6;
        options.Password.RequireDigit = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireLowercase = false;
        options.Password.RequireNonAlphanumeric = false;
    })
    .AddEntityFrameworkStores<AuthDbContext>();

//UnitOfWork
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>(provider => new UnitOfWork(defConnectionString));

//Jwt Config
var jwtSettings = new JwtSettings();
builder.Configuration.Bind(nameof(jwtSettings), jwtSettings);
builder.Services.AddSingleton(jwtSettings);
builder.Services.AddScoped<IIdentityService, IdentityService>();

//Auth Config
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)),
        ValidateIssuer = false,
        ValidateAudience = false,
        RequireExpirationTime = false,
        ValidateLifetime = true
    };
});



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    x.SwaggerDoc("v1", new OpenApiInfo { Title = "ArrestsCrimesUK", Version = "v1" });
    x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    x.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,

            },
            new List<string>()
        }
    });
});


//Mapper
builder.Services.AddAutoMapper(typeof(Program));

//Cors Policy
builder.Services.AddCors(options =>
    options.AddPolicy("AllowCors",
        b =>
        {
            b.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
        }
    )
);

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger(x => x.SerializeAsV2 = true);
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ArrestsCrimesUK API V1");
    c.RoutePrefix = string.Empty;
});

//app.MapControllers();
app.UseRouting();

app.UseCors("AllowCors");
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
app.Run();
