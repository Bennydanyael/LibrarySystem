using Library.App.API.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace LibraryAppAPI.Helpers
{
    public class TokenHelper
    {
        public const string Issuer = "http://192.168.0.3";
        public const string Audience = "http://192.168.0.3";
        public const string Secret = "OFRC1j9aaR2BvADxNWlG2pmuD392UfQBZZLM1fuzDEzDlEpSsn+btrpJKd3FfY855OMA9oK4Mc8y48eYUrVUSw==";
        public static string GenerateSecureSecret()
        {
            var _hmac = new HMACSHA256();
            return Convert.ToBase64String(_hmac.Key);
        }

        public static string GenerateToken(Customers _customers)
        {
            var _tokenHandler = new JwtSecurityTokenHandler();
            var _key = Convert.FromBase64String(Secret);
            var _claimIdentity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, _customers.CustomerId.ToString()),
                new Claim("IsBlocked", _customers.Blocked.ToString())
            });
            var _signingCredentials = new SigningCredentials(new SymmetricSecurityKey(_key), SecurityAlgorithms.HmacSha256Signature);
            var _tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = _claimIdentity,
                Issuer = Issuer,
                Audience = Audience,
                Expires = DateTime.Now.AddMinutes(15),
                SigningCredentials = _signingCredentials
            };
            var _token = _tokenHandler.CreateToken(_tokenDescriptor);
            return _tokenHandler.WriteToken(_token);
        }
    }
}
