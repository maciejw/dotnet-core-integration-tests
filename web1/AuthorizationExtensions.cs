using Microsoft.AspNetCore.Builder;
using System;
using System.Linq;

namespace web1
{
    public static class AuthorizationExtensions
    {
        public static IApplicationBuilder UseAuthorization(this IApplicationBuilder app, string policyName)
        {
            return app.UseMiddleware<AuthorizationMiddleware>(policyName);
        }
    }
}
