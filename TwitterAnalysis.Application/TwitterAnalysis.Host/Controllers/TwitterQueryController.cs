using Microsoft.AspNetCore.Mvc;
using TwitterAnalysis.Application.Messages.Request;
using TwitterAnalysis.Application.Messages.Response;
using TwitterAnalysis.Application.Services.Interfaces;
using System.Net;

namespace TwitterAnalysis.Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class TwitterQueryController : Controller
    {
        private readonly ITwitterSearchProcessor _twitterSearchProcessor;

        public TwitterQueryController(ITwitterSearchProcessor twitterSearchProcessor)
        {
            _twitterSearchProcessor = twitterSearchProcessor;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Just Testing");
        }


        [HttpPost()]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<TweetResponse>> PostQuery([FromBody] QueryRequest request)
        {
            await Task.Delay(50);

            var result = await _twitterSearchProcessor.ProcessSearchByQuery(request.Query);

            return Ok(result);
        }
    }
}
