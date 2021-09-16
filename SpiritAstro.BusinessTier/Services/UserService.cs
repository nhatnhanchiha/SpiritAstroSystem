using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SpiritAstro.BusinessTier.Entities;
using SpiritAstro.BusinessTier.Generations.Repositories;
using SpiritAstro.BusinessTier.Requests.User;
using SpiritAstro.BusinessTier.Responses;
using SpiritAstro.BusinessTier.Responses.User;
using SpiritAstro.DataTier.BaseConnect;
using SpiritAstro.DataTier.Models;

namespace SpiritAstro.BusinessTier.Generations.Services
{
    public partial interface IUserService
    {
        User GetById(int id);
        LoginResponse LoginByPhoneForCustomer(LoginRequest loginRequest);
    }
    
    public partial class UserService
    {
        private readonly IConfiguration _configuration;
        public UserService(IUnitOfWork unitOfWork,IUserRepository repository, IConfiguration configuration):base(unitOfWork,repository)
        {
            _configuration = configuration;
        }
        public User GetById(int id)
        {
            return this.Get(id);
        }

        public LoginResponse LoginByPhoneForCustomer(LoginRequest loginRequest)
        {
            var user = Get().Include(u => u.UserRoles)
                .FirstOrDefault(u => u.PhoneNumber == loginRequest.PhoneNumber && u.Password == loginRequest.Password && u.DeletedAt == null && u.UserRoles.Any(ur => ur.RoleId == "888"));
            if (user == null)
            {
                throw new ErrorResponse((int)HttpStatusCode.NotFound, "Invalid phone number or password");
            }

            var loginResponse = GenerateJwtToken(user);

            return loginResponse;
        }
        
        private LoginResponse GenerateJwtToken(User user)
        {
            var customClaims = new CustomClaims
            {
                UserId = user.Id,
                PhoneNumber = user.PhoneNumber,
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
                Name = user.Name,
                PhoneNumber = user.PhoneNumber,
                Gender = user.Gender,
                Roles = customClaims.Roles,
                DayOfBirth = user.DayOfBirth,
                Token = tokenString,
                BufferTime = customClaims.BufferTime * 1000,
                ExpiresAt = (long)TimeSpan.FromTicks(customClaims.Exp).TotalMilliseconds,
            };
        }
    }
}