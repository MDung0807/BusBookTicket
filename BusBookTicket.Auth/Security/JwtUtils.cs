using Microsoft.AspNetCore.DataProtection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BusBookTicket.Auth.Security
{
    public class JwtUtils
    {
        private static readonly string SECRET= "BachelorOfEngineeringThesisByMinhDung";
        private static readonly long EXPIRE = 4200000;

        public static string GernerateToken (string username, string role)
        {
            SHA256 sha256 = SHA256.Create(); 
            var secretBytes = Encoding.UTF8.GetBytes (SECRET);
            var symmetricKey = sha256.ComputeHash (secretBytes);
            var tokenHandler = new JwtSecurityTokenHandler();

            var now = DateTime.UtcNow;
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("username", username),
                    new Claim("role", role)
                }),

                Expires = now.AddMinutes(Convert.ToInt32(EXPIRE)),

                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(symmetricKey),
                    SecurityAlgorithms.HmacSha256Signature),
                TokenType = "Bearer"
            };

            var stoken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(stoken);

            return token;
        }

        public static ClaimsPrincipal GetPrincipal(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

                if (jwtToken == null)
                    return null;
                SHA256 sha256 = SHA256.Create();
                var secretBytes = Encoding.UTF8.GetBytes(SECRET);
                var symmetricKey = sha256.ComputeHash(secretBytes);

                var validationParameters = new TokenValidationParameters()
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(symmetricKey)
                };

                SecurityToken securityToken;
                var principal = tokenHandler.ValidateToken(token, validationParameters, out securityToken);

                return principal;
            }
            catch (Exception)
            {
                //should write log
                return null;
            }
        }
    }
}
