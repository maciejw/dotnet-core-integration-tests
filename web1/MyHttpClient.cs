using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Threading.Tasks;

namespace web1
{
    public interface IMyHttpClient
    {
        Task<T> Get<T>(string url);
    }
    public class MyHttpClient : IMyHttpClient
    {
        private readonly HttpClient client;
        public MyHttpClient(HttpClient client, IOptions<MyHttpClientOptions> options)
        {
            this.client = client;

            client.BaseAddress = options.Value.BaseAddress;
        }

        public async Task<T> Get<T>(string url)
        {
            var httpResponseMessage = await client.GetAsync(url);

            return await httpResponseMessage.EnsureSuccessStatusCode().Content.ReadAsAsync<T>();
        }
    }
}
