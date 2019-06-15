using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace web1
{
    public class AuthorizationMiddleware
    {
        private readonly RequestDelegate next;
        private readonly string policyName;

        public AuthorizationMiddleware(RequestDelegate next, string policyName)
        {
            this.next = next;
            this.policyName = policyName;
        }

        public async Task Invoke(HttpContext httpContext, IAuthorizationService authorizationService)
        {
            AuthorizationResult authorizationResult = await authorizationService.AuthorizeAsync(httpContext.User, policyName);

            if (authorizationResult.Succeeded)
            {
                await next(httpContext);
            }
            else
            {
                await httpContext.ChallengeAsync();
            }
        }
    }
}
