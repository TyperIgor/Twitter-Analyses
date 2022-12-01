using Microsoft.AspNetCore.Mvc;
using TwitterAnalysis.Application.Messages.Request;
using TwitterAnalysis.Application.Messages.Response;
using TwitterAnalysis.Application.Services.Interfaces;
using System.Net;
using Microsoft.AspNetCore.Authorization;

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

        [HttpPost()]
        [Authorize]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<ActionResult<TweetResponse>> PostQuery([FromBody] QueryRequest request, [FromQuery] PaginationQuery page)
        {
            await Task.Delay(50);

            var result = await _twitterSearchProcessor.ProcessSearchByQuery(request.Query, page);

            return Ok(result);
        }
    }
}
