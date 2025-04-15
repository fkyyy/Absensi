using Application.Common.Dtos.Users;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Persistence;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Users.Commands
{
    public class LoginUser
    {
        public class Query : IRequest<string>
        {
            public required string Email { get; set; }
            public required string Password { get; set; }
        }

        public class Handler(AppDbContext context, IConfiguration config) : IRequestHandler<Query, string>
        {
            public async Task<string> Handle(Query request, CancellationToken cancellationToken)
            {
                var user = await context.Users
                    .FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken);

                if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                    throw new UnauthorizedAccessException("Invalid email or password");

                var claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.IdUser.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Role, user.Role),
                    new Claim("IdDivision", user.IdDivision.ToString())

                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.UtcNow.AddHours(3),
                    signingCredentials: creds
                );

                return new JwtSecurityTokenHandler().WriteToken(token);
            }  
        }
    }
    public class UserClaimsHelper
        {
            private readonly IHttpContextAccessor _httpContextAccessor;

            public UserClaimsHelper(IHttpContextAccessor httpContextAccessor)
            {
                _httpContextAccessor = httpContextAccessor;
            }

            public Guid GetUserId() =>
                Guid.Parse(_httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new UnauthorizedAccessException());

            public string GetUserRole() =>
                _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Role)?.Value ?? string.Empty;

            public Guid GetUserDivision() =>
                Guid.Parse(_httpContextAccessor.HttpContext?.User.FindFirst("IdDivision")?.Value ?? throw new UnauthorizedAccessException());
        }
}