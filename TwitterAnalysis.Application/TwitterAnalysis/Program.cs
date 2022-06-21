using TwitterAnalysis.Extension;
using SimpleInjector;
using TwitterAnalysis.Initializer;

namespace TwitterAnalysis
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var instance = ConfigureDependencies();
            Start(instance);
        }

        static Container ConfigureDependencies()
        {
            return ApplicationExtension.RegisterDependecies();
        }

        static void Start(Container container)
        {
            new AppManager(container).Execute();
        }
    }
}
