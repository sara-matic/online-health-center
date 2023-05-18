using IdentityServer.Data;
using IdentityServer.DTOs;
using IdentityServer.Entities;
using IdentityServer.Repositories.Interfaces;
using IdentityServer.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace IdentityServer.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IIdentityRepository repository;
        private readonly IConfiguration configuration;

        public AuthenticationService(IIdentityRepository repository, IConfiguration configuration)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<User?> ValidateUser(UserCredentialsDTO userCredentials)
        {
            var user = await this.repository.GetUserByUsername(userCredentials.UserName);
            if (user == null || !await this.repository.CheckUserPassword(user, userCredentials.Password))
            {
                return null;
            }
            return user;
        }

        public async Task<AuthenticationModel> CreateAuthenticationModel(User user)
        {
            var accessToken = await CreateAccessToken(user);
            var refreshToken = await CreateRefreshToken();

            await this.repository.AddRefreshTokenToUser(user, refreshToken);

            return new AuthenticationModel { AccessToken = accessToken, RefreshToken = refreshToken.Token };
        }

        public async Task RemoveRefreshToken(User user, string refreshToken)
        {
            await this.repository.RemoveRefreshTokenFromUser(user, refreshToken);
            await this.repository.RemoveRefreshTokenFromDb(refreshToken);
        }

        private async Task<string> CreateAccessToken(User user)
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims(user);
            var token = GenerateToken(signingCredentials, claims);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(this.configuration.GetValue<string>("JwtSettings:secretKey"));
            var secret = new SymmetricSecurityKey(key);

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<IEnumerable<Claim>> GetClaims(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var roles = await this.repository.GetUserRoles(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }

        private JwtSecurityToken GenerateToken(SigningCredentials signingCredentials, IEnumerable<Claim> claims)
        {
            var jwtSettings = this.configuration.GetSection("JwtSettings");

            var token = new JwtSecurityToken
            (
                issuer: jwtSettings.GetSection("validIssuer").Value,
                audience: jwtSettings.GetSection("validAudience").Value,
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings.GetSection("expires").Value)),
                signingCredentials: signingCredentials
            );

            return token;
        }

        private async Task<RefreshToken> CreateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);

            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(randomNumber),
                ExpiryTime = DateTime.Now.AddDays(Convert.ToDouble(this.configuration.GetValue<string>("RefreshTokenExpires")))
            };

            await this.repository.AddRefreshTokenToDb(refreshToken);

            return refreshToken;
        }
    }
}
