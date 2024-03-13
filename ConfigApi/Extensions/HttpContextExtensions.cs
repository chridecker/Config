using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;

namespace ConfigApi.Extensions
{
    public static class HttpContextExtensions
    {
        public static Task<string> GetJWTTokenAsync(this HttpContext context) => context.GetTokenAsync(JwtBearerDefaults.AuthenticationScheme, "access_token");
    }
}
