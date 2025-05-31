using Intelli.AgentPortal.Api.Helpers;
using Intelli.AgentPortal.Api.Services.JWT.Impl;
using Intelli.AgentPortal.Domain.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Intelli.AgentPortal.Api.Services
{
    /// <summary>
    /// The Json Web token service.
    /// </summary>
    public class JWTService : IJWTService
    {
        private readonly byte[] _secretKeyBytes;
        private readonly string _issuer;
        private readonly string _audience;
        private readonly int _tokenExpiryInHours;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthService"/> class.
        /// </summary>
        /// <param name="config">The config object to be Injected via IOC</param>
        public JWTService(IConfiguration config)
        {
            var secretKey = config[IAuthConstants.JwtSecretKey];
            _secretKeyBytes = Encoding.UTF8.GetBytes(secretKey);

            _issuer = config[IAuthConstants.JwtIssuer];
            _audience = config[IAuthConstants.JwtAudience];

            var temp = config[IAuthConstants.TokenExpiryInHours];
            if (!int.TryParse(temp, out _tokenExpiryInHours)) _tokenExpiryInHours = 3;
        }

        /// <summary>
        /// Generates the JWT Token.
        /// </summary>
        /// <param name="userInfo">The user info.</param>
        /// <returns>A string.</returns>
        public async Task<TokenResponseDto> GenerateJWTToken(SystemUser userInfo, List<int> CompanyIds)
        {
            return await Task.Run(() =>
            {
                // JWT private key from the config
                var securityKey = new SymmetricSecurityKey(_secretKeyBytes);

                // creating the credentials from the secret key
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                // Create jti using guid
                var jti = Guid.NewGuid().ToString();
             
                // creating the claims array for the JWT token
                var claims = new[]
                {
                    new Claim(nameof(userInfo.Id), userInfo.Id.ToString()),
                    new Claim(nameof(CompanyIds),  Newtonsoft.Json.JsonConvert.SerializeObject(CompanyIds).ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, userInfo.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, jti)
                };

                // Generating the token object
                var token = new JwtSecurityToken(
                    issuer: _issuer,
                    audience: _audience,
                    claims: claims,
                    expires: DateTime.Now.AddHours(_tokenExpiryInHours),
                    signingCredentials: credentials
                );

                // Writing the Token to a String
                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                // Create token response
                var dto = new TokenResponseDto
                {
                    Token = tokenString,
                    Jti = jti
                };
                
                // Return response
                return dto;
            });
        }
    }
}
