using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TwitterAnalysis.Application.Auth.Interfaces;
using TwitterAnalysis.Application.Messages.Request.Auth;
using TwitterAnalysis.Application.Services.Interfaces;

namespace TwitterAnalysis.Host.Controllers.Authentication
{
    [ApiController]
    [Route("v1/[controller]")]
    [Produces("application/json")]
    public class AuthenticatorController : Controller
    {
        private readonly ILoginAuthProcessor _loginAuthProcessor;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        public AuthenticatorController(ILoginAuthProcessor loginAuthProcessor, 
                                       IJwtTokenGenerator jwtTokenGenerator)
        {
            _loginAuthProcessor = loginAuthProcessor;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<dynamic> AuthenticateAsync([FromBody] LoginAuthRequest request)
        {
            var userExist = await _loginAuthProcessor.IsRegistered(request.Name, request.Secret);

            if (!userExist)
                return NotFound(request);

            return new {
                token = _jwtTokenGenerator.GenerateToken(request),
                UserRegistered = true
            };
        
        }
    }
}
