using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TestWebApiClient.Controllers
{
    [ApiController]
    [Route("")]
    public class TesteController : ControllerBase
    {
        private IHttpClientFactory _factory;
        public TesteController(IHttpClientFactory factory)
        {
            _factory = factory;
        }

        [HttpGet]
        public async Task<object> Get()
        {
            try
            {
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls13;
                var client = _factory.CreateClient("HttpClientWithSSLUntrusted");
                var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:5051/");
                var response = client.Send(request);
                return Ok(await response.Content.ReadAsStringAsync());
            }
            catch (System.Exception ex)
            {
                return Ok(ex.ToString());
            }
        }
    }
}