using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace test1
{
    public static class TestAuthenticationExtensions
    {
        public static void AddTestAuthentication(this IServiceCollection services, Action<TestAuthenticationOptions> configure = null)
        {
            services.AddAuthentication()
                .AddScheme<TestAuthenticationOptions, TestAuthenticationHandler>(
                    TestAuthenticationOptions.DefaultAuthenticationScheme,
                    TestAuthenticationOptions.DefaultAuthenticationScheme,
                    configure ?? DefaultConfigureOptions);

            services.PostConfigure<AuthenticationOptions>(options =>
            {
                options.DefaultAuthenticateScheme = TestAuthenticationOptions.DefaultAuthenticationScheme;
                options.DefaultChallengeScheme = TestAuthenticationOptions.DefaultAuthenticationScheme;
                options.DefaultForbidScheme = TestAuthenticationOptions.DefaultAuthenticationScheme;
                options.DefaultScheme = TestAuthenticationOptions.DefaultAuthenticationScheme;
                options.DefaultSignInScheme = TestAuthenticationOptions.DefaultAuthenticationScheme;
                options.DefaultSignOutScheme = TestAuthenticationOptions.DefaultAuthenticationScheme;
            });
        }

        private static void DefaultConfigureOptions(TestAuthenticationOptions options)
        {

        }
    }
}
