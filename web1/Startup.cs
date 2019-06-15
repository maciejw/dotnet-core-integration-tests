using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace web1
{

    public class MyHttpClientOptions
    {
        public class Configuration : IConfigureOptions<MyHttpClientOptions>
        {
            private readonly IConfiguration configuration;
            public Configuration(IConfiguration configuration)
            {
                this.configuration = configuration.GetSection(nameof(MyHttpClientOptions));
            }

            public void Configure(MyHttpClientOptions options)
            {
                configuration.Bind(options);
            }
        }

        public Uri BaseAddress { get; set; }
    }

    public class Startup
    {
        private const string RequireAuthenticatedUserPolicy = nameof(RequireAuthenticatedUserPolicy);


        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureOptions<MyHttpClientOptions.Configuration>();

            using (var setupProvider = services.BuildServiceProvider())
            {
                var options = setupProvider.GetService<IOptionsSnapshot<MyHttpClientOptions>>();

                var logger = setupProvider.GetService<ILogger<Startup>>();


                logger.LogInformation("MyHttpClientOptions '{options}'", JsonConvert.SerializeObject(options.Value));
            }

            services.AddHttpClient("IdentityServerDemo")
                .AddTypedClient<IMyHttpClient, MyHttpClient>();

            services.AddMvc(options =>
            {
                options.Filters.Add(new AuthorizeFilter(RequireAuthenticatedUserPolicy));

            }).SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_1);

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddCookie()
                .AddJwtBearer(options =>
                {

                });


            services.AddAuthorization(options =>
            {
                options.AddPolicy(RequireAuthenticatedUserPolicy, builder =>
                {
                    builder.RequireAuthenticatedUser();
                });
            }).AddAuthorizationPolicyEvaluator();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();

            app.UseMvc();

            app.UseAuthorization(RequireAuthenticatedUserPolicy);

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!\n");
                foreach (var claim in context.User.Claims)
                {
                    await context.Response.WriteAsync($"Claim: {claim}\n");
                }
            });


        }
    }
}
