using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TwitterAnalysis.Application.Messages.Request;
using TwitterAnalysis.Application.Messages.Response;
using TwitterAnalysis.Application.Services.Interfaces;

namespace TwitterAnalysis.Application.Controllers
{
    [ApiController]
    [Route("api/v1/search")]
    [Produces("application/json")]
    public class TwitterQueryController : Controller
    {
        private readonly ITwitterSearchProcessor _twitterSearchProcessor;

        public TwitterQueryController(ITwitterSearchProcessor twitterSearchProcessor)
        {
            _twitterSearchProcessor = twitterSearchProcessor;
        }

        [HttpGet]
        public Task GetById()
        {
            return Task.FromResult(0);
        }

        [HttpPost()]
        public async Task<ActionResult<TweetResponse>> PostQuery([FromBody] QueryRequest request)
        {
            if (request == null) return BadRequest();

            var result = await _twitterSearchProcessor.ProcessSearch(request.Query);

            return Ok(result);
        }
    }
}
