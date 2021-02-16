using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LibraryAppAPI.Models.VM
{
    public class TokenManager
    {
        private static string Secret = "SecretMyJWT";
        public static string GenerateToken(string _username)
        {
            byte[] _key = Convert.FromBase64String(Secret);
            SymmetricSecurityKey _securityKey = new SymmetricSecurityKey(_key);
            SecurityTokenDescriptor _descriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, _username)
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(_securityKey,
                SecurityAlgorithms.HmacSha256Signature)
            };
            JwtSecurityTokenHandler _handler = new JwtSecurityTokenHandler();
            JwtSecurityToken _token = _handler.CreateJwtSecurityToken(_descriptor);
            return _handler.WriteToken(_token);
        }

        public static ClaimsPrincipal GetPrincipal(string _token)
        {
            try
            {
                JwtSecurityTokenHandler _handler = new JwtSecurityTokenHandler();
                JwtSecurityToken _jwtToken = (JwtSecurityToken)_handler.ReadToken(_token);
                if (_jwtToken == null)
                    return null;
                byte[] _key = Convert.FromBase64String(Secret);
                TokenValidationParameters _parameters = new TokenValidationParameters()
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(_key)
                };
                SecurityToken _securityToken;
                ClaimsPrincipal _principal = _handler.ValidateToken(_token, _parameters, out _securityToken);
                return _principal;
            }catch
            {
                return null;
            }
        }

        public static string ValidateToken(string _token)
        {
            string _username = null;
            ClaimsPrincipal _principal = GetPrincipal(_token);
            if (_principal == null)
                return null;
            ClaimsIdentity _identity = null;
            try
            {
                _identity = (ClaimsIdentity)_principal.Identity;
            }catch(NullReferenceException)
            {
                return null;
            }
            Claim _usernameClaim = _identity.FindFirst(ClaimTypes.Name);
            _username = _usernameClaim.Value;
            return _username;
        }
    }
}
