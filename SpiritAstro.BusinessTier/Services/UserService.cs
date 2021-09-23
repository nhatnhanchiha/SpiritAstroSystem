using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
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
using IConfigurationProvider = AutoMapper.IConfigurationProvider;

namespace SpiritAstro.BusinessTier.Generations.Services
{
    public partial interface IUserService
    {
        User GetById(long id);
        LoginResponse LoginByPhone(LoginRequest loginRequest);
        Task<long> RegisterCustomer(RegisterCustomerRequest registerCustomerRequest);
    }
    
    public partial class UserService
    {
        private readonly IConfiguration _configuration;
        private readonly IConfigurationProvider _mapper;
        public UserService(IUnitOfWork unitOfWork,IUserRepository repository, IConfiguration configuration, IMapper mapper):base(unitOfWork,repository)
        {
            _configuration = configuration;
            _mapper = mapper.ConfigurationProvider;
        }
        public User GetById(long id)
        {
            return this.Get(id);
        }

        public async Task<long> RegisterCustomer(RegisterCustomerRequest registerCustomerRequest)
        {
            var mapper = _mapper.CreateMapper();
            var user = mapper.Map<User>(registerCustomerRequest);
            await CreateAsyn(user);
            return user.Id;
        } 

        public LoginResponse LoginByPhone(LoginRequest loginRequest)
        {
            var user = Get().Include(u => u.UserRoles)
                .FirstOrDefault(u => u.PhoneNumber == loginRequest.PhoneNumber && u.Password == loginRequest.Password && u.DeletedAt == null);
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
                TimeOfBirth = user.TimeOfBirth,
                Token = tokenString,
                BufferTime = customClaims.BufferTime * 1000,
                ExpiresAt = ((long)new DateTime(customClaims.Exp).ToUniversalTime().Subtract(
                    new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                ).TotalSeconds) * 1000,
            };
        }
    }
}