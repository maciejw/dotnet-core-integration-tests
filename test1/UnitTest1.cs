using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using web1;
using Xunit;
using Xunit.Abstractions;

namespace test1
{
    public class UnitTest1 : IClassFixture<WebApplicationFactory<web1.Startup>>
    {
        private readonly WebApplicationFactory<web1.Startup> factory;
        private readonly ITestOutputHelper output;
        public UnitTest1(WebApplicationFactory<web1.Startup> factory, ITestOutputHelper output)
        {
            this.output = output;
            this.factory = factory;
        }
        [Fact]
        public async Task Test1()
        {
            var client = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddTestAuthentication(options =>
                    {
                        options.UseTestUser(new Claim(ClaimTypes.Name, "test"));

                    });
                });

            }).CreateClient();


            var response = await client.GetAsync("/some-url");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            output.WriteLine(await response.Content.ReadAsStringAsync());

        }

        [Fact]
        public async Task Test2()
        {
            var client = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddTestAuthentication(options =>
                    {
                        options.UseTestUser(new Claim(ClaimTypes.Name, "test"));
                    });
                });

            }).CreateClient();


            var response = await client.GetAsync("/api/values");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            output.WriteLine(await response.Content.ReadAsStringAsync());

        }


    }


}
