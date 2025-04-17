using Application.Attachments.Queries;
using Application.Attendances.Queries;
using Application.Common.Mappings;
using Application.Divisions.Queries;
using Application.Leaves.Queries;
using Application.LeaveTransactions.Queries;
using Application.Users.Queries;
using Application.Users.Commands;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Application.Validators.Users;
using Application.Validators.Attendances;
using Application.Validators.Divisions;
using Application.Validators.Leaves;
using Application.Validators.LeaveTransactions;
using FluentValidation;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Konfigurasi koneksi database
var connectionString = builder.Configuration.GetConnectionString("PostgreSQLConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(connectionString);
});

// Konfigurasi MediatR untuk setiap handler
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblyContaining<GetUserList.Handler>();
    cfg.RegisterServicesFromAssemblyContaining<GetAttachmentList.Handler>();
    cfg.RegisterServicesFromAssemblyContaining<GetAttendancesList.Handler>();
    cfg.RegisterServicesFromAssemblyContaining<GetLeaveList.Handler>();
    cfg.RegisterServicesFromAssemblyContaining<GetLeaveTransactionList.Handler>();
    cfg.RegisterServicesFromAssemblyContaining<GetDivisionList.Handler>();
});

// Konfigurasi AutoMapper
builder.Services.AddAutoMapper(typeof(UserProfile), typeof(DivisionProfile), typeof(LeaveProfile),
    typeof(LeaveTransactionProfile), typeof(AttachmentProfile), typeof(AttendanceProfile));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<UserClaimsHelper>();

// Konfigurasi Swagger + JWT Auth
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Absensis API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Masukkan token JWT seperti ini: Bearer {your token}"
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });

    c.AddServer(new Microsoft.OpenApi.Models.OpenApiServer
    {
        Url = "https://ordinary-kendra-absensi-7acdcd87.koyeb.app"
    });
});

// Konfigurasi Authentication dengan JWT
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

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<CreateAttendanceDtoValidator>();
// Konfigurasi CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Register Cloudinary as a singleton service
var account = new Account(
    builder.Configuration["Cloudinary:CloudName"],     
    builder.Configuration["Cloudinary:ApiKey"],        
    builder.Configuration["Cloudinary:ApiSecret"]      
);
var cloudinary = new Cloudinary(account);
builder.Services.AddSingleton(cloudinary);

builder.WebHost.UseUrls("http://+:8080");

var app = builder.Build();

// Middleware setup
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
