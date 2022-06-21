using SimpleInjector;
using TwitterAnalysis.App.Services;
using TwitterAnalysis.App.Services.Interfaces;
using TwitterAnalysis.Application.Services;
using TwitterAnalysis.Application.Services.Interfaces;


namespace TwitterAnalysis.Extension
{
    public class ApplicationExtension
    {
        public static Container RegisterDependecies() 
        {
            var container = new Container();

            container.Register<ITwitterSearchProcessor, TwitterSearchProcessor>();
            container.Register<ITwitterSearchQuery, TwitterSearchService>();

            return container;
        }
    }
}
