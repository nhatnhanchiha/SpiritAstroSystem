using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SpiritAstro.BusinessTier.Entities;
using SpiritAstro.BusinessTier.Generations.Repositories;
using SpiritAstro.BusinessTier.Responses;
using SpiritAstro.BusinessTier.Responses.User;
using SpiritAstro.DataTier.BaseConnect;
using SpiritAstro.DataTier.Models;

namespace SpiritAstro.BusinessTier.Generations.Services
{
    public partial interface IUserService
    {
        Task<LoginResponse> Login(string uid);
    }

    public partial class UserService
    {
        private readonly IConfiguration _configuration;

        public UserService(IUnitOfWork unitOfWork, IUserRepository repository, IConfiguration configuration) : base(
            unitOfWork, repository)
        {
            _configuration = configuration;
        }

        public async Task<LoginResponse> Login(string uid)
        {
            var user = await Get().Include(u => u.UserRoles)
                .Include(u => u.Astrologer)
                .Include(u => u.Customer)
                .FirstOrDefaultAsync(u => u.Uid == uid && u.DeletedAt == null);

            return GenerateJwtToken(user);
        }
        

        private LoginResponse GenerateJwtToken(User user)
        {
            var customClaims = new CustomClaims
            {
                UserId = user.Id,
                Roles = string.Join(",", user.UserRoles.Select(ur => ur.RoleId).ToArray()),
                BufferTime = long.Parse(_configuration["jwt:BufferTime"]),
                Exp = DateTime.UtcNow.AddSeconds(Convert.ToDouble(_configuration["jwt:ExpiresTime"])).Ticks,
                Iss = _configuration["Jwt:Issuer"],
                Nbf = DateTime.UtcNow.AddSeconds(-5).Ticks,
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:SigningKey"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(customClaims.ToEnumerableClaims()),
                Expires = new DateTime(customClaims.Exp),
                Issuer = customClaims.Iss,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            var tokenString = tokenHandler.WriteToken(token);
            return new LoginResponse
            {
                UserId = user.Id,
                Roles = customClaims.Roles,
                Token = tokenString,
                BufferTime = customClaims.BufferTime * 1000,
                ExpiresAt = ((long)new DateTime(customClaims.Exp).ToUniversalTime().Subtract(
                    new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                ).TotalSeconds) * 1000,
            };
        }
    }
}