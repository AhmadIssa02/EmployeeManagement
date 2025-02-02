using EmployeeManagement.Models;
using EmployeeManagement.Models.Domain;
using EmployeeManagement.Models.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EmployeeManagement.Services
{
    public class AuthManager : IAuthManager
    {
        private readonly UserManager<APIUser> _userManager;
        private readonly IConfiguration _configuration;
        private APIUser _user;
        public AuthManager(UserManager<APIUser> userManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<string> CreateToken()
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims();
            var token = GenerateTokenOptions(signingCredentials, claims);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var jwtSettings = _configuration.GetSection("Jwt");
            var expiration = DateTime.Now.AddMinutes(Convert.ToDouble(
                jwtSettings.GetSection("lifetime").Value));

            var token = new JwtSecurityToken(
                issuer: jwtSettings.GetSection("Issuer").Value,
                claims: claims,
                expires: expiration,
                signingCredentials: signingCredentials
                );  

            return token;
        }
        private async Task<List<Claim>> GetClaims()
        {
            var claims = new List<Claim>

       {
           new Claim(ClaimTypes.Name, _user.UserName)
       };

            var roles = await _userManager.GetRolesAsync(_user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }
        private SigningCredentials GetSigningCredentials()
        {
            var key = Environment.GetEnvironmentVariable("KEY");
            var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        public async Task<bool> ValidateUser(LoginUserDto userDTO)
        {
            _user = await _userManager.FindByNameAsync(userDTO.Email);
            var validPassword = await _userManager.CheckPasswordAsync(_user, userDTO.Password);
            return (_user != null && validPassword);
        }
        public async Task<string> CreateRefreshToken()
        {
            await _userManager.RemoveAuthenticationTokenAsync(_user, "EmployeeManagement", "RefreshToken");
            var newRefreshToken = await _userManager.GenerateUserTokenAsync(_user, "EmployeeManagement", "RefreshToken");
            var result = await _userManager.SetAuthenticationTokenAsync(_user, "EmployeeManagement", "RefreshToken", newRefreshToken);
            return newRefreshToken;
        }
        public async Task<TokenRequest> VerifyRefreshToken(TokenRequest request)
        {
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var tokenContent = jwtSecurityTokenHandler.ReadJwtToken(request.Token);
            var username = tokenContent.Claims.ToList().FirstOrDefault(q => q.Type == ClaimTypes.Name)?.Value;
            _user = await _userManager.FindByNameAsync(username);
            try
            {
                var isValid = await _userManager.VerifyUserTokenAsync(_user, "EmployeeManagement", "RefreshToken", request.RefreshToken);
                if (isValid)
                {
                    return new TokenRequest { Token = await CreateToken(), RefreshToken = await CreateRefreshToken() };
                }
                await _userManager.UpdateSecurityStampAsync(_user);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return null;
        }
    }
}
