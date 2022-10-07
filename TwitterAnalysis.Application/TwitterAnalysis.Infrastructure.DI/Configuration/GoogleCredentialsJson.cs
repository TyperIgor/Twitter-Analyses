using Microsoft.Extensions.Configuration;
using System;

namespace TwitterAnalysis.Infrastructure.DI.Configuration
{
    public class GoogleCredentialsJson
    {
        private readonly IConfiguration _configuration;

        public GoogleCredentialsJson(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetCredentialsOnJson()
        {
            return _configuration.GetSection("").Value;
        }
    }
}
