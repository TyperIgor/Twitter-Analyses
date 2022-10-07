using Microsoft.Extensions.Configuration;

namespace TwitterAnalysis.Infrastructure.DI.Configuration
{
    public class FlagConfig 
    {
        private readonly IConfiguration _configuration;

        public bool IsEnabled { get; set; }

        public FlagConfig(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private bool IsEnabledFileRoot()
        {
            return _configuration.GetValue<bool>(nameof(FlagConfig));
        }
    }
}