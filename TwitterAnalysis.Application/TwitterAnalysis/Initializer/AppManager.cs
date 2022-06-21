using SimpleInjector;
using TwitterAnalysis.Application.Services.Interfaces;
using TwitterAnalysis.Interfaces;

namespace TwitterAnalysis.Initializer
{
    public class AppManager : IAppManager
    {
        private readonly ITwitterSearchProcessor _twitterProcessor;

        public AppManager(Container container)
        {
            _twitterProcessor = container.GetInstance<ITwitterSearchProcessor>();
        }

        public void Execute()
        {
            _twitterProcessor.ProcessSearch("");
        }
    }
}
