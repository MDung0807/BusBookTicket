﻿using BusBookTicket.Auth.DTOs.Responses;
using BusBookTicket.Auth.Exceptions;
using BusBookTicket.Auth.Services.AuthService;
using BusBookTicket.Auth.Utils;
using BusBookTicket.Common.Models.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;

namespace BusBookTicket.Auth.Security
{
    public class JwtUtils
    {
        #region -- Private properties --
        private static readonly string SECRET = "BachelorOfEngineeringThesisByMinhDung";
        private static readonly long EXPIRE = 4200000;
        #endregion -- Private properties --

        #region -- Public properties -- 
        #endregion -- Public properties --

        /// <summary>
        /// When login success. Reponse token
        /// </summary>
        /// <param name="username"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        public static string GernerateToken (AuthResponse response)
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
                    new Claim("UserID", response.userID.ToString()),
                    new Claim("username", response.username),
                    new Claim("role", response.roleName)
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

        /// <summary>
        /// Decode token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
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
            catch 
            {
                throw new AuthException(AuthConstants.UNAUTHORIZATION);
            }
        }

        /// <summary>
        /// Get token from header
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetToken (HttpContext context)
        {
            string authString = context.Request.Headers["Authorization"].FirstOrDefault();
            if (!authString.IsNullOrEmpty())
            {
                authString = authString.Substring(7);
            }
            return authString ?? string.Empty;
        }

        /// <summary>
        /// Get ID Client from header['Authorization']
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetUserID(HttpContext context)
        {
            string token = GetToken(context);
            var principal = GetPrincipal(token);
            return principal.Claims.ElementAt(0).Value;
        }
    }
}
