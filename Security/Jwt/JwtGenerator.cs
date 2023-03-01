using Application.Common;
using Application.Common.Helpers;
using Application.Entity;
using Application.Interface.Security;
using Application.Interface.Security.Jwt;
using JWT.Algorithms;
using JWT.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Security.Jwt.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Security.Jwt
{
    public class JwtGenerator : IJwtGenerator
    {
        //private readonly string _KeySecurity;
        private readonly TokenManagement _tokenSettings;

        public JwtGenerator(IConfiguration config, IOptions<TokenManagement> tokenSettings) 
        {
            //_KeySecurity = config["KeySecurity"];
            _tokenSettings = tokenSettings.Value;
        }

        public async Task<AuthDto> CreateToken(CredentialsEntity credentials)
        {
            var claims = new[] {
                new Claim(JWTClaimTypes.Username, credentials.Username),
                new Claim(JWTClaimTypes.Password, credentials.Password)
            };

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenSettings.Secret));
            var encryptingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenSettings.EncryptionSecret));
            var now = DateTime.UtcNow;
            var expiresAt = DateTime.UtcNow.AddMinutes(_tokenSettings.AccessExpiration);

            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            var encryptingCredentials = new EncryptingCredentials(encryptingKey, SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256);

            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.CreateJwtSecurityToken(
                _tokenSettings.Issuer,
                _tokenSettings.Audience,
                new ClaimsIdentity(claims),
                now,
                expiresAt,
                now,
                signingCredentials: signingCredentials,
                encryptingCredentials: encryptingCredentials
            );

            var token = await Task.Run(() => handler.WriteToken(jwtSecurityToken));
            var expirationTimestamp = DateTimeHelper.GetUnixTimeMilliseconds(expiresAt);

            return new AuthDto(token, expirationTimestamp, credentials.Username, credentials.Password);
        }

        public static class JWTClaimTypes
        {
            public const string Username = "https://primax/username";
            //public const string Name = "https://suizasoft/name";
            //public const string LastName = "https://suizasoft/lastname";
            //public const string MotherLastName = "https://suizasoft/mother-lastname";
            public const string Password = "https://primax/password";
        }
    }
}
