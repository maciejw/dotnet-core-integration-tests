using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace web1
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly IMyHttpClient client;
        public ValuesController(IMyHttpClient client)
        {
            this.client = client;
        }

        // GET: api/<controller>
        [HttpGet]
        public async Task<dynamic> Get()
        {
            var configuration = await client.Get<dynamic>("/.well-known/openid-configuration");

            var claims = HttpContext.User.Claims.Select(c => c.ToString());
            return new { claims, authorization_endpoint = configuration.authorization_endpoint };
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return $"value-{id}";
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
