using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TwitterAnalysis.Application.Messages.Request;
using TwitterAnalysis.Application.Messages.Response;
using TwitterAnalysis.Application.Services.Interfaces;

namespace TwitterAnalysis.Host.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class DataTrainingController : Controller
    {
        private readonly ITrainingDataProcessor _trainingDataProcessor;
        public DataTrainingController(ITrainingDataProcessor trainingDataProcessor)
        {
            _trainingDataProcessor = trainingDataProcessor;
        }

        [HttpGet]
        [Authorize]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<ActionResult<DataTrainingResponse>> GetTrainingData()
        {
            var result = await _trainingDataProcessor.GetAlgorithmDataList();

            return Ok(result);
        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<ActionResult<DataTrainingResponse>> Post([FromBody] RacistPhraseRequest request)
        {
            var result = await _trainingDataProcessor.InsertNewDataForTraining(request);

            return Ok(result);
        }
    }
}
