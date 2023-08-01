using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Net;

namespace TwitterAnalysis.Host.Controllers.HealthCheck
{
    [ApiController]
    [Route("v1/[controller]")]
    [Produces("application/json")]
    public class HealthController : Controller
    {
        private readonly HealthCheckService _healthCheck;

        public HealthController(HealthCheckService healthCheck)
        {
            _healthCheck = healthCheck;
        }

        [HttpGet()]
        public async Task<IActionResult> Get()
        {
            var report = await _healthCheck.CheckHealthAsync();

            return report.Status == HealthStatus.Healthy ? Ok(report) : StatusCode((int)HttpStatusCode.ServiceUnavailable, report);
        }
    }
}
