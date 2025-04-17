using Application.Attachments.Queries;
using Application.Attendances.Queries;
using Application.Common.Mappings;
using Application.Divisions.Queries;
using Application.Leaves.Queries;
using Application.LeaveTransactions.Queries;
using Application.Users.Queries;
using Application.Users.Commands;
using Application.Validators.Users;
using Application.Validators.Attendances;
using Application.Validators.Divisions;
using Application.Validators.Leaves;
using Application.Validators.LeaveTransactions;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Persistence;
using System.Text;
using CloudinaryDotNet;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Database configuration
var connectionString = builder.Configuration.GetConnectionString("PostgreSQLConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(connectionString);
});

// MediatR configuration
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblyContaining<GetUserList.Handler>();
    cfg.RegisterServicesFromAssemblyContaining<GetAttachmentList.Handler>();
    cfg.RegisterServicesFromAssemblyContaining<GetAttendancesList.Handler>();
    cfg.RegisterServicesFromAssemblyContaining<GetLeaveList.Handler>();
    cfg.RegisterServicesFromAssemblyContaining<GetLeaveTransactionList.Handler>();
    cfg.RegisterServicesFromAssemblyContaining<GetDivisionList.Handler>();
});

// AutoMapper
builder.Services.AddAutoMapper(
    typeof(UserProfile),
    typeof(DivisionProfile),
    typeof(LeaveProfile),
    typeof(LeaveTransactionProfile),
    typeof(AttachmentProfile),
    typeof(AttendanceProfile)
);

// FluentValidation + Controllers
builder.Services.AddControllers();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
// Claims Helper
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<UserClaimsHelper>();

// Swagger + JWT Auth
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Absensis API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Masukkan token JWT seperti ini: Bearer {your token}"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });

    c.AddServer(new OpenApiServer
    {
        Url = "https://ordinary-kendra-absensi-7acdcd87.koyeb.app"
    });
});

// JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    var config = builder.Configuration;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!))
    };
});

// CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Cloudinary
var account = new Account(
    builder.Configuration["Cloudinary:CloudName"],
    builder.Configuration["Cloudinary:ApiKey"],
    builder.Configuration["Cloudinary:ApiSecret"]
);
var cloudinary = new Cloudinary(account);
builder.Services.AddSingleton(cloudinary);

// Use specific port
builder.WebHost.UseUrls("http://+:8080");

var app = builder.Build();

// Middleware pipeline
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("AllowFrontend");
app.UseStaticFiles();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapGet("/", () => "Absensi API is running...");

app.Run();
