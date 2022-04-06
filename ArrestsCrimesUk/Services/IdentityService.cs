using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ArrestsCrimesUk.Options;
using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace ArrestsCrimesUk.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtSettings _jwtSettings;

        public IdentityService(UserManager<IdentityUser> userManager, JwtSettings jwtSettings)
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings;
        }

        public async Task<AuthenticationResult> RegisterAsync(string requestEmail, string requestPassword)
        {
            var existingUser = await _userManager.FindByEmailAsync(requestEmail);

            if (existingUser != null)
            {
                return new AuthenticationResult
                {
                    ErrorMessages = new[] { "User with this email address already exists." }
                };
            }

            var newUser = new IdentityUser
            {
                Email = requestEmail,
                UserName = requestEmail,
            };

            var createdUser = await _userManager.CreateAsync(newUser, requestPassword);

            if (!createdUser.Succeeded)
            {
                return new AuthenticationResult
                {
                    ErrorMessages = createdUser.Errors.Select(e => e.Description)
                };
            }

            return GenerateAuthenticationResultForUser(newUser);
        }


        public async Task<AuthenticationResult> LoginAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return new AuthenticationResult
                {
                    ErrorMessages = new[] { "User with this email does not exists.You need to register." }
                };
            }

            var userHasValidPassword = await _userManager.CheckPasswordAsync(user, password);
            if (!userHasValidPassword)
            {
                return new AuthenticationResult
                {
                    ErrorMessages = new[] { "Invalid password." }
                };
            }

            return GenerateAuthenticationResultForUser(user);
        }

        private AuthenticationResult GenerateAuthenticationResultForUser(IdentityUser newUser)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, newUser.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, newUser.Email),
                    new Claim("id", newUser.Id)
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new AuthenticationResult
            {
                Success = true,
                Token = tokenHandler.WriteToken(token)
            };
        }

    }
}
