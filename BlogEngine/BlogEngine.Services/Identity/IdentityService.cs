namespace BlogEngine.Services.Identity
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    public class IdentityService : IIdentityService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;

        public IdentityService(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<Dictionary<string, string>> GenerateToken(ApplicationUser user)
        {
            //Generate token

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("AppSettings:SecretKey"));

            // Set expire time
            var expiredIn = TimeSpan.FromSeconds(_configuration.GetValue<int>("AppSettings:ApiExpireIn"));

            var claims = await _userManager.GetClaimsAsync(user);
            var claimsIdentity = new ClaimsIdentity(claims);

            //Add role to claim
            var roles = await _userManager.GetRolesAsync(user);
            foreach (string role in roles)
            {
                claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, role));
            }

            // Build Descriptor
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = DateTime.UtcNow.Add(expiredIn),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha512Signature)
            };

            // Complete and build token
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(securityToken);

            var response = new Dictionary<string, string>
            {
                {"AccessToken", tokenString},
                {"ExpiredIn", tokenDescriptor.Expires.GetValueOrDefault(DateTime.UtcNow).ToString("O")}
            };

            return response;
        }
    }
}
