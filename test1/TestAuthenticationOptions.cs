using Microsoft.AspNetCore.Authentication;
using System;
using System.Linq;
using System.Security.Claims;

namespace test1
{
    public class TestAuthenticationOptions : AuthenticationSchemeOptions
    {
        public const string DefaultAuthenticationScheme = "test";
        public virtual ClaimsIdentity Identity { get; private set; }

        public TestAuthenticationOptions()
        {

        }

        public void UseTestUser(params Claim[] claims)
        {
            Identity = new ClaimsIdentity(new Claim[] { }.Concat(claims), DefaultAuthenticationScheme);
        }

        public void UseAnonymousUser()
        {
            Identity = null;
        }
    }
}
