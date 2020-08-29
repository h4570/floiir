using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebApi.Misc.Http
{
    public static class HttpUtilities
    {

        /// <summary>
        /// Returns host from request headers.
        /// Examples: "localhost", "google.com"
        /// </summary>
        public static string GetHostFromRequestHeaders(IHeaderDictionary headers)
        {
            headers.TryGetValue("Origin", out StringValues originValues);
            string dirty;
            if (originValues.Count > 0)
                dirty = originValues[0];
            else throw new Exception("Origin was not found in request headers");
            var clean = new Uri(dirty).Host;
            return clean.ToString();
        }

        /// <summary>
        /// Compute's JWT token
        /// </summary>
        /// <param name="privateKey"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static string GenerateJWTToken(string privateKey, int userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(privateKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", userId.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
