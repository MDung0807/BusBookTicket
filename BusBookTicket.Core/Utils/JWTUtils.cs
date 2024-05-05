using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace BusBookTicket.Core.Utils;

public class JWTUtils
{
    private static readonly string SECRET = "BachelorOfEngineeringThesisByMinhDung";

     public static ClaimsPrincipal GetPrincipal(string token)
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

        /// <summary>
        /// Get username Client from header['Authorization']
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static string GetUserName(string token)
        {
            var principal = GetPrincipal(token);
            string id = principal.Claims.ElementAt(1).Value;
            return id;  
        }
        
}