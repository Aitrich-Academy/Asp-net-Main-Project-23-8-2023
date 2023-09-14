using DAL.Models;
using ECOMMERSE.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace ECOMMERSE.Utils
{
    public class TokenManager
    {
        private static string Secret = "ERMN05OPLoDvbTTa/QkqLNMI3cPLguaRyHzyg7n5qNBVjQmtBhz4SzYh4NBVCXi3KJHlSXKP+oi2+bXr6CUYTR==";

        public static string GenerateToken(USER UsersBO)
        {
            byte[] key = Convert.FromBase64String(Secret);
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key);
            int TokenExpiry = 720;
            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {
                Issuer = "MyProjectName",
                Subject = new ClaimsIdentity(
                    new[] {
                        new Claim("USER_ID", UsersBO.USER_ID.ToString()),
                        new Claim("USER_ROLE", UsersBO.USER_ROLE.ToString())
                    }),
                Expires = DateTime.UtcNow.AddMinutes(TokenExpiry),
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            };
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = handler.CreateJwtSecurityToken(descriptor);
            return handler.WriteToken(token);
        }

        public static Ent_User ValidateToken(string token)
        {
            ClaimsPrincipal principal = GetPrincipal(token);
            return GetUserFromPrincipal(principal);
        }

        public static ClaimsPrincipal GetPrincipal(string token)
        {
            try
            {
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                //var tokens = new System.IdentityModel.Tokens.Jwt.JwtSecurityToken(token);

                JwtSecurityToken jwtToken = (JwtSecurityToken)tokenHandler.ReadToken(token);
                if (jwtToken == null) return null;
                byte[] key = Convert.FromBase64String(Secret);
                TokenValidationParameters parameters = new TokenValidationParameters()
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ClockSkew = TimeSpan.Zero //validity not checking
                };
                SecurityToken securityToken;
                ClaimsPrincipal principal = tokenHandler.ValidateToken(token, parameters, out securityToken);

                var jwtSecurityToken = securityToken as JwtSecurityToken;
                if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                    throw new SecurityTokenException("Invalid token");
                return principal;
            }
            catch(Exception ex) 
            {
                return null;
            }
        }

        private static Ent_User GetUserFromPrincipal(ClaimsPrincipal principal)
        {
            if (principal == null) return null;
            ClaimsIdentity identity = null;
            try
            {
                identity = (ClaimsIdentity)principal.Identity;
            }
            catch (NullReferenceException)
            {
                return null;
            }
            Claim userpidClaim = identity.FindFirst("user_id");
            Claim usertypeClaim = identity.FindFirst("USER_ROLE");
            Ent_User usersDTO = new Ent_User
            {
                Id = int.Parse(userpidClaim.Value),
                role = usertypeClaim.Value
            };
            return usersDTO;
        }
    }
}