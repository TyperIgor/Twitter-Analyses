using System.Threading.Tasks;

namespace TwitterAnalysis.Application.Services.Interfaces
{
    public interface ILoginAuthProcessor
    {
        Task<bool> IsRegistered(string name, string secret);
    }
}
