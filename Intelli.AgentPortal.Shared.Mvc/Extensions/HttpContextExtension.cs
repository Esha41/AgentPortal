using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace Intelli.AgentPortal.Shared.Mvc
{
    /// <summary>
    /// The http context extension methods.
    /// </summary>
    public static class HttpContextExtension
    {
        private static readonly JwtSecurityTokenHandler _tokenHandler = new();

        /// <summary>
        /// Gets the claim value from jwt token by extracting it from current request.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="claimType">The claim type.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>A string.</returns>
        public static T GetClaimValue<T>(HttpContext context, string claimType, T defaultValue ,bool isJson = false)
        {
            var token = context.Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", string.Empty);
            if (string.IsNullOrEmpty(token)) return defaultValue;

            var jwtToken = _tokenHandler.ReadJwtToken(token);
            var claim = jwtToken.Claims.FirstOrDefault(x => x.Type == claimType);
            if (isJson)
            {
                return claim != null ? (T)Convert.ChangeType(Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(claim.Value), typeof(T)) : defaultValue;
            }
            return claim != null ? (T)Convert.ChangeType(claim.Value, typeof(T)) : defaultValue;
        }

        /// <summary>
        /// Gets the user id from jwt token by extracting it from current request.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>A int.</returns>
        public static int GetUserId(this HttpContext context)
        {
            return GetClaimValue(context, "Id", 0);
        }

        /// <summary>
        /// Gets the company id from jwt token by extracting it from current request.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>A int.</returns>
        public static int GetCompanyId(this HttpContext context)
        {
            return GetClaimValue(context, "CompanyId", 0);
        }

        /// <summary>
        /// Gets the user email from jwt token by extracting it from current request.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>A string.</returns>
        public static string GetUserEmail(this HttpContext context)
        {
            return GetClaimValue(context, JwtRegisteredClaimNames.Email, "System");
        }

        /// <summary>
        /// Gets the JwtRegisteredClaimName, jti.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>A string.</returns>
        public static string GetJti(this HttpContext context)
        {
            return GetClaimValue(context, JwtRegisteredClaimNames.Jti, string.Empty);
        }

        public static List<int> GetCompanyIds(this HttpContext context)
        {

            return GetClaimValue(context, "CompanyIds", new List<int>() ,true);
        }
    }
}
